using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Interfaces;
using TimerNotificatoin.Core.Models;

namespace TimerNotificatoin.Forms
{
    public partial class ContentsForm : Form, INotificatoinMessage
    {
        protected ContentsForm()
        {
            InitializeComponent();
        }
        public static ContentsForm CreateForm(string title, Font fnt)
        {
            var fm = new ContentsForm() { Text = title };
            fm.txtContent.Font = fnt;
            return fm;
        }

        public void ShowMessage(string message, EnMessageType messageType)
        {
            txtContent.Text = message;
            txtContent.Select(0, 0);
        }

        public void ShowMessage(NotificationModel message, EnMessageType messageType)
        {
            if (messageType == EnMessageType.NotificationShow) { 
                TopMost = true;
            }
            Text = $"{message.Title} - {message.AlertDateTime:yyyy-MM-dd HH:mm:ss} {message.AlertDateTime.DayOfWeek}";
            txtContent.Text = $"Date Time - {message.AlertDateTime:yyyy-MM-dd HH:mm:ss} {message.AlertDateTime.DayOfWeek}";
            txtContent.Text += $"{Environment.NewLine}{Environment.NewLine}";
            txtContent.Text += message.Description;
            txtContent.Select(0, 0);
        }

        public void ShowMessage(IEnumerable<NotificationModel> message, EnMessageType messageType)
        {
        }
    }
}
