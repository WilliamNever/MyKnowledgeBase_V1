using Microsoft.Extensions.Options;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Core.Services;
using TimerNotificatoin.Core.Settings;
using TimerNotificatoin.Forms;

namespace TimerNotificatoin
{
    public partial class MainForm : Form
    {
        private readonly TimerServices timerServices;
        private bool Exiting = false;
        private AppSettings settings;
        private AlertsAutoSaveService alertsAutoSaveService;
        public MainForm()
        {
            InitializeComponent();
            settings = HOSTServices.GetRequiredService<IOptions<AppSettings>>().Value;
            alertsAutoSaveService = HOSTServices.GetRequiredService<AlertsAutoSaveService>();
            dgDataList.AutoGenerateColumns = false;
            timerServices = HOSTServices.GetTimerServices(this, ReadConfigedAlerts());

            Initial();
        }

        private void Initial()
        {
            ReBoundControlData();
            SwichWindowModel(tmiOpenOrHiden, WindowState);
            
            nfyTimer.Text = $"Notification Timer - Initial";
        }

        #region Action Events

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
                    ReBoundControlData();
                    SaveActiveAlerts();
                    ShowMessage($"Remove alerts successfully!", EnMessageType.StatusShow);
                }
            }
            else
            {
                MessageBox.Show(this, "Please select rows to remove.", "No rows selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tslStatus.Text = "";
            switch (e.ClickedItem.AccessibleName)
            {
                case "HelperFile":
                    var helpfile = HOSTServices.GetService<OutputHelperService>();
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

            ReBoundControlData();
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

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            ReFreshControlStyles();
        }

        #endregion
    }
}