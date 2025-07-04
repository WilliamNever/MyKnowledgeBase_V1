﻿using TimerNotificatoin.Core.Attributes;
using TimerNotificatoin.Core.Enums;
using TimerNotificatoin.Core.Interfaces;

namespace TimerNotificatoin.Core.Models
{
    [HelperOutput("class Classification Model - Indicated the classification of the notification")]
    public class ClassificationModel : ICompareIdentity<int>
    {
        int ICompareIdentity<int>.Identity { get => ID; }
        [HelperOutput("int ID - the identity of the classification, default is 1")]
        public int ID { get; set; } = 1;
        [HelperOutput("string Name - name of the classification, default is Unclassified")]
        public string Name { get; set; } = "Unclassified";
        [HelperOutput("EnNotificationType NotificationType - Indicated if the notification can be remained after alerted, default is EnNotificationType.Unclassified")]
        public EnNotificationType NotificationType { get; set; } = EnNotificationType.Unclassified;
        [HelperOutput("int Alpha - alpha value in ARGB, default is 0xff")]
        public int Alpha { get; set; } = 0xff;
        [HelperOutput("int Red - red value in ARGB, default is 0xff")]
        public int Red { get; set; } = 0xff;
        [HelperOutput("int Green - green value in ARGB, default is 0xff")]
        public int Green { get; set; } = 0xff;
        [HelperOutput("int Blue - blue value in ARGB, default is 0xff")]
        public int Blue { get; set; } = 0xff;

        public string DisplayName { get => $"{Name} - {NotificationType}"; }
    }
}
