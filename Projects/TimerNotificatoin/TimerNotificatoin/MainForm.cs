using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Core.Services;

namespace TimerNotificatoin
{
    public partial class MainForm : Form, INotificatoinMessage
    {
        private List<NotificationModel> Notifications = new();
        private readonly TimerServices timerServices;
        public MainForm()
        {
            InitializeComponent();
            Initial();
            timerServices = new TimerServices(1000, true, this);
        }

        private void Initial()
        {
            SwichWindowModel(tmiOpenOrHiden, WindowState);
        }

        #region INotificatoinMessage members
        public void ShowMessage(string message, EnMessageType messageType)
        {
            Invoke(() => { });
        }
        public void ShowMessage(NotificationModel message, EnMessageType messageType)
        {
            Invoke(() => { });
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
                    Close();
                    Application.Exit();
                    break;
                default:
                    break;
            }
        }
        private void SwichWindowModel(ToolStripItem item, FormWindowState fwState)
        {
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
            nfyTimer.ShowBalloonTip(3000, "Test", "T Content", ToolTipIcon.Info);
        }
    }
}