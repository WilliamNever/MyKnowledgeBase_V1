using TimerNotificatoin.Core.Consts;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;
using TimerNotificatoin.Core.Services;
using TimerNotificatoin.Forms;

namespace TimerNotificatoin
{
    public partial class MainForm : Form, INotificatoinMessage
    {
        private List<NotificationModel> Notifications = new();
        private readonly TimerServices timerServices;
        public MainForm()
        {
            InitializeComponent();
            dgDataList.AutoGenerateColumns = false;

            Initial();
            timerServices = APPHOST.GetTimerServices(this);
        }

        private void Initial()
        {
            SwichWindowModel(tmiOpenOrHiden, WindowState);

#if Debug
            Notifications.Add(new NotificationModel { AlertDateTime = DateTime.Now, Description = "Lots", Title = "al Title" });
            Notifications.Add(new NotificationModel { AlertDateTime = DateTime.Now, Description = "Lots_1", Title = "al Title_1" });
            Notifications.Add(new NotificationModel { AlertDateTime = DateTime.Now, Description = "Lots_2", Title = "al Title_2" });
#endif
            dgDataList.DataSource = Notifications;
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

        private void dgDataList_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //to do - edit a row
        }

        private void btnAddAlert_Click(object sender, EventArgs e)
        {
            //to do - add a row
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
                    break;
                default:
                    break;
            }
        }
    }
}