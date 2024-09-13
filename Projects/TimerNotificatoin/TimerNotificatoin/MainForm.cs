using Microsoft.Extensions.Options;
using System.Diagnostics;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Helpers;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Core.Services;
using TimerNotificatoin.Core.Settings;
using TimerNotificatoin.Forms;

namespace TimerNotificatoin
{
    public partial class MainForm : Form, INotificatoinMessage
    {
        private readonly TimerServices timerServices;
        private bool Exiting = false;
        private AppSettings settings;
        private AlertsAutoSaveService alertsAutoSaveService;
        public MainForm()
        {
            InitializeComponent();
            settings = APPHOST.GetRequiredService<IOptions<AppSettings>>().Value;
            alertsAutoSaveService = APPHOST.GetRequiredService<AlertsAutoSaveService>();
            dgDataList.AutoGenerateColumns = false;
            timerServices = APPHOST.GetTimerServices(this, ReadConfigedAlerts());

            Initial();
        }

        private List<NotificationModel> ReadConfigedAlerts()
        {
            var txt = File.ReadAllText(settings.Notifications);
            var notifies = ConversionsHelper.NJ_DeserializeToJson<List<NotificationModel>>(txt);
            return notifies ?? new List<NotificationModel>();
        }

        private void Initial()
        {
            SwichWindowModel(tmiOpenOrHiden, WindowState);
            ReBoundDataGrid();
            nfyTimer.Text = $"Notification Timer - Initial";
        }

        #region INotificatoinMessage members
        public void ShowMessage(string message, EnMessageType messageType)
        {
            Invoke(() =>
            {
                if ((messageType & EnMessageType.MessageShow) > 0)
                {
                    nfyTimer.ShowBalloonTip(3000, message, message, ToolTipIcon.Info);
                }
                if ((messageType & EnMessageType.StatusShow) > 0)
                {
                    tslStatus.Text = message;
                }
                if ((messageType & EnMessageType.Stopped) > 0)
                {
                    btnStart.Enabled = true;
                    nfyTimer.Text = $"Notification Timer - {message}";
                }
                if ((messageType & EnMessageType.Started) > 0)
                {
                    btnStart.Enabled = false;
                    nfyTimer.Text = $"Notification Timer - {message}";
                }
            });
        }
        public void ShowMessage(IEnumerable<NotificationModel> messages, EnMessageType messageType)
        {
            Invoke(() =>
            {
                foreach (var message in messages)
                {
                    nfyTimer.ShowBalloonTip(3000, message.Title, message.Description, ToolTipIcon.Info);

                    var cf = ContentsForm.CreateForm("Helper", new Font(new FontFamily("Times New Roman"), 14f));
                    cf.ShowMessage(message, EnMessageType.NotificationShow);
                    cf.Show();
                }
                ReBoundDataGrid();
                SaveActiveAlerts(true);
            });
        }
        #endregion

        private void cmsIcon_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Name)
            {
                case nameof(tmiOpenOrHiden):
                    WindowState = WindowState == FormWindowState.Minimized ? FormWindowState.Normal : FormWindowState.Minimized;
                    SwichWindowModel(e.ClickedItem, WindowState);
                    break;
                case nameof(tmiExit):
                    Exiting = true;
                    Close();
                    break;
                case nameof(tmiStartOrStop):
                    if (timerServices.TimerIsRunning)
                    {
                        btnStop_Click(sender, e);
                        tmiStartOrStop.Text = "Start";
                    }
                    else
                    {
                        timerServices.Start();
                        nfyTimer.ShowBalloonTip(3000, "Star Timer", $"There are {timerServices.GetActiveNotification().Count} activity Notifications.", ToolTipIcon.Info);
                        tslStatus.Text = "Timer is in running...";
                        tmiStartOrStop.Text = "Stop";
                    }
                    break;
                default:
                    break;
            }
        }
        private void SwichWindowModel(ToolStripItem item, FormWindowState fwState)
        {
            btnStart.Enabled = !timerServices.TimerIsRunning;
            var isMin = fwState == FormWindowState.Minimized;
            item.Text = isMin ? "Open" : "Hidden";
            ShowInTaskbar = !isMin;
            if (isMin)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            SwichWindowModel(tmiOpenOrHiden, WindowState);
        }

        private void nfyTimer_DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Minimized ? FormWindowState.Normal : FormWindowState.Minimized;
            SwichWindowModel(tmiOpenOrHiden, WindowState);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            timerServices.Start();
            WindowState = WindowState == FormWindowState.Minimized ? FormWindowState.Normal : FormWindowState.Minimized;
            SwichWindowModel(tmiOpenOrHiden, WindowState);
            nfyTimer.ShowBalloonTip(3000, "Star Timer", $"There are {timerServices.GetActiveNotification().Count} activity Notifications.", ToolTipIcon.Info);
            tslStatus.Text = "Timer is in running...";
            btnStart.Enabled = false;
        }

        private void dgDataList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var idx = e.RowIndex;
            var guid = (Guid)dgDataList.Rows[idx].Cells["Id"].Value;
            var nty = timerServices.GetTotalNotification().FirstOrDefault(x => x.Id == guid) ?? new NotificationModel();
            CreateOrUpdateNotification(nty, sender, e);
        }

        private void CreateOrUpdateNotification(NotificationModel notification, object sender, EventArgs e)
        {
            var alpu = new AlertInput();
            alpu.SetNotification(notification);
            if (alpu.ShowDialog() == DialogResult.OK)
            {
                btnStop_Click(sender, e);
                timerServices.AppendOrReplaceAlerts(new List<NotificationModel> { alpu.GetNotification() });
                ReBoundDataGrid();
                SaveActiveAlerts();
                ShowMessage($"Update alerts list successfully - {alpu.GetNotification().Title}", EnMessageType.StatusShow);
            }
        }

        private void btnAddAlert_Click(object sender, EventArgs e)
        {
            CreateOrUpdateNotification(new NotificationModel(), sender, e);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgDataList.SelectedRows.Count > 0)
            {
                if (MessageBox.Show(this, "Sure to Remove?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question
                    , MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    List<Guid> ids = dgDataList.SelectedRows.OfType<DataGridViewRow>().Select(x => (Guid)x.Cells["Id"].Value).ToList();
                    btnStop_Click(sender, e);
                    timerServices.RemoveAlerts(ids);
                    ReBoundDataGrid();
                    SaveActiveAlerts();
                    ShowMessage($"Remove alerts successfully!", EnMessageType.StatusShow);
                }
            }
            else
            {
                MessageBox.Show(this, "Please select rows to remove.", "No rows selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReBoundDataGrid()
        {
            var ndt = DateTime.Now.Date;
            dgDataList.DataSource = timerServices.GetTotalNotification();
            for (var i = 0; i < dgDataList.Rows.Count; i++)
            {
                dgDataList.Rows[i].Cells["OrderIndex"].Value = $"{i + 1}";
                if (
                    bool.TryParse(dgDataList.Rows[i].Cells["IsAlerted"].Value?.ToString(), out bool rsl) && !rsl
                    && DateTime.TryParse(dgDataList.Rows[i].Cells["AlertDateTime"].Value?.ToString(), out DateTime dtRsl)
                    && dtRsl.Date <= ndt
                    )
                {
                    dgDataList.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else if (rsl)
                {
                    dgDataList.Rows[i].DefaultCellStyle.BackColor = Color.Azure;
                }
            }
            dgDataList.ClearSelection();
            dgDataList.Refresh();
        }

        private void DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tslStatus.Text = "";
            switch (e.ClickedItem.AccessibleName)
            {
                case "HelperFile":
                    var helpfile = APPHOST.GetService<OutputHelperService>();
                    var txt = helpfile?.ReadHelperFile();

                    var cf = ContentsForm.CreateForm("Helper", new Font(new FontFamily("Times New Roman"), 14f));
                    cf.ShowMessage(txt ?? "", EnMessageType.MessageShow);
                    cf.Show();
                    ShowMessage("Done!", EnMessageType.StatusShow);
                    break;
                case "Exit":
                    e.ClickedItem.GetCurrentParent().Hide();
                    Exiting = true;
                    Close();
                    break;
                case "ShowNotificationsFoler":
                    var path = Path.GetFullPath(settings.Notifications).TrimEnd((Path.GetFileName(settings.Notifications) ?? "").ToArray());
                    OpenFolderSelectFiles(path);
                    break;
                default:
                    break;
            }
        }

        private void OpenFolderSelectFiles(string pathFile)
        {
            var psi = new ProcessStartInfo("Explorer.exe")
            {
                Arguments = pathFile
            };
            Process.Start(psi);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Exiting)
            {
                timerServices.Stop();
                SaveActiveAlerts();
                timerServices.Dispose();
            }
            else
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
        }

        private void SaveActiveAlerts(bool delaySave = false)
        {
            lock (timerServices.SynchronizingObject)
            {
                var alts = timerServices.GetActiveNotification();
                var txt = ConversionsHelper.SerializeToJson(alts, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                if (delaySave)
                {
                    alertsAutoSaveService.Add(txt);
                }
                else
                {
                    alertsAutoSaveService.Stop(true);
                    File.WriteAllText(settings.Notifications, txt, System.Text.Encoding.UTF8);
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timerServices.Stop();
            tslStatus.Text = "Timer is stopped!";
            btnStart.Enabled = true;
        }

        private void cmsIcon_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mntrip = sender as ContextMenuStrip;
            if (mntrip != null)
            {
                foreach (var item in mntrip.Items.OfType<ToolStripItem>())
                {
                    switch (item.Name)
                    {
                        case nameof(tmiStartOrStop):
                            item.Text = timerServices.TimerIsRunning ? "Stop" : "Start";
                            break;
                        case nameof(tmiOpenOrHiden):
                            var isMin = WindowState == FormWindowState.Minimized;
                            item.Text = isMin ? "Open" : "Hidden";
                            break;
                        default: break;
                    }
                }
            }
        }

        private void btnReloadAlerts_Click(object sender, EventArgs e)
        {
            btnStop_Click(sender, e);
            var notis = ReadConfigedAlerts();
            timerServices.ResetAlerts(notis);

            ReBoundDataGrid();
            ShowMessage("Reload alerts successfully", EnMessageType.StatusShow);
        }

        private void btnSaveAlerts_Click(object sender, EventArgs e)
        {
            SaveActiveAlerts();
        }

        private void dgDataList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                var id = (Guid)dgDataList.Rows[e.RowIndex].Cells["Id"].Value;
                var cf = ContentsForm.CreateForm("Helper", new Font(new FontFamily("Times New Roman"), 14f));
                cf.ShowMessage(
                    timerServices.GetTotalNotification().FirstOrDefault(x => x.Id == id)
                    ?? new NotificationModel { Title = "Not Found", Description = "Not Found" }
                    , EnMessageType.NotificationShow);
                cf.Show();
            }
        }
    }
}