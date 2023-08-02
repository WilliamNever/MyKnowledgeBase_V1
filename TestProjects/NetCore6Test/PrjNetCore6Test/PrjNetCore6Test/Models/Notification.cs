namespace PrjNetCore6Test.Models
{
    public class Notification
    {
        public Notification()
        {
            
        }
        //public const string TempDataKey = "TempDataAlerts";

        public string NotificationStyle;/* { get; set; }*/
        //public string Message { get; set; }
        //public bool Dismissable { get; set; }
    }

    public static class NotificationStyles
    {
        public const string Success = "success";
        public const string Information = "info";
        public const string Warning = "warning";
        public const string Danger = "danger";
    }
}
