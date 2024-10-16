using TimerNotificatoin.Core.Attributes;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput($"class OutterToolModel - indicate outter tool")]
    public class OutterToolModel
    {
        [HelperOutput($"public string FullFileName - indicate the execute file in full file name")]
        public string FullFileName { get; set; }
        [HelperOutput($"public string WorkingDirectory - indicate the working directory of the execute file")]
        public string WorkingDirectory { get; set; }
        [HelperOutput($"public bool IsExecuteFile - indicate if the file is an execute file, default is false")]
        public bool IsExecuteFile { get; set; } = false;
        [HelperOutput($"public bool IsSyncRunning - indicate if the file needs running in Sync, default is false")]
        public bool IsSyncRunning { get; set; } = false;
        [HelperOutput($"public string? Description - indicate the description of the file")]
        public string? Description { get; set; }
    }
}
