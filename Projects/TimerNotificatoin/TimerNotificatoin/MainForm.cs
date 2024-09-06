using Microsoft.Extensions.Options;
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
            dgDataList.DataSource = timerServices.GetTotalNotification();
            dgDataList.Refresh();
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
                dgDataList.DataSource = timerServices.GetTotalNotification();
                dgDataList.Refresh();
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
            //to do - edit a row
        }

        private void btnAddAlert_Click(object sender, EventArgs e)
        {
            var alpu = new AlertInput();
            alpu.SetNotification(new NotificationModel() { AlertDateTime = DateTime.Now });
            if (alpu.ShowDialog() == DialogResult.OK)
            {
                btnStop_Click(sender, e);
                timerServices.AppendOrReplaceAlerts(new List<NotificationModel> { alpu.GetNotification() });

                dgDataList.DataSource = timerServices.GetTotalNotification();
                dgDataList.Refresh();
                SaveActiveAlerts();
                ShowMessage($"Added an alert - {alpu.GetNotification().Title}", EnMessageType.StatusShow);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //to do - delete selected rows
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

            dgDataList.DataSource = timerServices.GetTotalNotification();
            dgDataList.Refresh();
            ShowMessage("Reload alerts successfully", EnMessageType.StatusShow);
        }

        private void btnSaveAlerts_Click(object sender, EventArgs e)
        {
            SaveActiveAlerts();
        }
    }
}