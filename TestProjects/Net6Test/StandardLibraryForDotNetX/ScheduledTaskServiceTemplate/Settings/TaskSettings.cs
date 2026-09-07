namespace StandardLibraryForDotNetX.ScheduledTaskServiceTemplate.Settings
{
    public class TaskSettings
    {
        public string WorkingCronoExpress { get; set; }
        public int WorkingTasks { get; set; } = 1;
        public bool IsEnabled { get; set; } = true;
    }
}
