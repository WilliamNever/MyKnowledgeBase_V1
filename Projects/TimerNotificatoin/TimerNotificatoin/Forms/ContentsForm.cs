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
        }

        public void ShowMessage(NotificationModel message, EnMessageType messageType)
        {
        }
    }
}
