using Cronos;
using TimerNotificatoin.Core.Consts;
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
        public static ContentsForm CreateForm(string title, Action? ClosedAction, Font fnt)
        {
            var fm = CreateForm(title, fnt);
            fm.SelfClosed = ClosedAction;
            return fm;
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
            if (messageType == EnMessageType.NotificationShow)
            {
                TopMost = true;
            }
            Text = $"{message.Title} - {message.CurrentAlertDateTime:yyyy-MM-dd HH:mm:ss} {message.CurrentAlertDateTime.DayOfWeek}";
            txtContent.Text = $"Date Time - {message.CurrentAlertDateTime:yyyy-MM-dd HH:mm:ss} {message.CurrentAlertDateTime.DayOfWeek}";
            txtContent.Text += $"{Environment.NewLine}{Environment.NewLine}";
            txtContent.Text += message.Description;

            if (message.NotificationType == EnNotificationType.Loop && message.ToAlert)
            {
                txtContent.Text += $"{Environment.NewLine}{Environment.NewLine}" +
                    $"-------------------------------------------------------" +
                    $"{Environment.NewLine}{Environment.NewLine}";

                if (message.AlertDateTime == message.CurrentAlertDateTime)
                {
                    var tmp = HOSTServices.GetTemplates().FirstOrDefault(x => x.Id == message.NTemplateId);
                    if (tmp != null)
                    {
                        CronExpression expression = CronExpression.Parse(tmp.CronoExp);
                        var next = expression.GetNextOccurrence(new DateTimeOffset(message.CurrentAlertDateTime), TimeZoneInfo.Local);
                        if (next.HasValue)
                            txtContent.Text += $"Next alert date time - {next.Value.DateTime:yyyy-MM-dd HH:mm:ss} {next.Value.DateTime.DayOfWeek}";
                    }
                }
                else
                {
                    txtContent.Text += $"Next alert date time - {message.AlertDateTime:yyyy-MM-dd HH:mm:ss} {message.CurrentAlertDateTime.DayOfWeek}";
                }
            }
            txtContent.Select(0, 0);
        }

        public void ShowMessage(IEnumerable<NotificationModel> message, EnMessageType messageType)
        {
        }

        public Action? SelfClosed { get; set; } = null;
        private void ContentsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SelfClosed != null)
                SelfClosed();
        }
    }
}
