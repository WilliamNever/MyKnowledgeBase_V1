namespace TimerNotificatoin.Core.Consts
{
    public static class ConstsParams
    {
        public static string RootDirectory { get => Environment.CurrentDirectory; }
        public static string ConfigDirectory { get => $"{RootDirectory}\\Configs\\"; }
        public static string HelperFilePath { get => $"{RootDirectory}\\Configs\\ReadMe.txt"; }
    }
}
