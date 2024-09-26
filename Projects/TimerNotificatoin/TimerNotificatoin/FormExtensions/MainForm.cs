using System.Diagnostics;
using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Helpers;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Forms;

namespace TimerNotificatoin
{
    public partial class MainForm : INotificatoinMessage
    {
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
                    nfyTimer.Text = $"Notification Timer - {message} - {DateTime.Now}";
                }
                if ((messageType & EnMessageType.Started) > 0)
                {
                    btnStart.Enabled = false;
                    nfyTimer.Text = $"Notification Timer - {message}";
                }
                if ((messageType & EnMessageType.CheckPoint) > 0)
                {
                    nfyTimer.Text = $"Notification Timer - {DateTime.Now}";
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
                ReBoundControlData();
                SaveActiveAlerts(true);
            });
        }
        #endregion
        
        private void SaveActiveAlerts(bool delaySave = false)
        {
            lock (timerServices.SynchronizingObject)
            {
                var alts = timerServices.GetSavedNotification();
                var txt = ConversionsHelper.NJ_SerializeToJson(alts, new Newtonsoft.Json.JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                });
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

        private void OpenFolderSelectFiles(string pathFile)
        {
            var psi = new ProcessStartInfo("Explorer.exe")
            {
                Arguments = pathFile
            };
            Process.Start(psi);
        }

        private void ReFreshControlStyles()
        {
            var ndt = DateTime.Now.Date;
            var nfts = HOSTServices.GetClassifications();
            for (var i = 0; i < dgDataList.Rows.Count; i++)
            {
                var ntfid = (int)dgDataList.Rows[i].Cells["ClassificationID"].Value;
                var ntf = nfts.FirstOrDefault(x => x.ID == ntfid) ?? new ClassificationModel();
                var rtype = ntf.NotificationType;

                dgDataList.Rows[i].Cells["OrderIndex"].Value = $"{i + 1}";
                if (
                    bool.TryParse(dgDataList.Rows[i].Cells["IsAlerted"].Value?.ToString(), out bool rsl) && !rsl
                    && DateTime.TryParse(dgDataList.Rows[i].Cells["AlertDateTime"].Value?.ToString(), out DateTime dtRsl)
                    && dtRsl.Date <= ndt
                    )
                {
                    dgDataList.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else if (rsl || ((rtype & EnNotificationType.Common) == 0))
                {
                    dgDataList.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(ntf.Alpha, ntf.Red, ntf.Green, ntf.Blue);
                }
            }
            dgDataList.ClearSelection();
        }

        private void CreateOrUpdateNotification(NotificationModel notification, object sender, EventArgs e)
        {
            var alpu = new AlertInput();
            alpu.SetNotification(notification);
            if (alpu.ShowDialog() == DialogResult.OK)
            {
                btnStop_Click(sender, e);
                timerServices.AppendOrReplaceAlerts(new List<NotificationModel> { alpu.GetNotification() });
                ReBoundControlData();
                SaveActiveAlerts();
                ShowMessage($"Update alerts list successfully - {alpu.GetNotification().Title}", EnMessageType.StatusShow);
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

        private void ReBoundControlData()
        {
            dgDataList.DataSource = timerServices.GetTotalNotification();
            ReFreshControlStyles();
            dgDataList.Refresh();
        }

        private List<NotificationModel> ReadConfigedAlerts()
        {
            var txt = File.ReadAllText(settings.Notifications);
            var notifies = ConversionsHelper.NJ_DeserializeToJson<List<NotificationModel>>(txt);
            return notifies ?? new List<NotificationModel>();
        }
    }
}
