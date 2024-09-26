using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNotificatoin.Core.Attributes;

namespace TimerNotificatoin.Core.Enums
{
    [Flags]
    [HelperOutput("[Flags] enum EnNotificationType - to show the classification of a notification")]
    public enum EnNotificationType
    {
        [HelperOutput("Unclassified(0) - not classified, Keep in untill manually removed")]
        Unclassified = 0,
        [HelperOutput("Common(1) - popped up then removed")]
        Common = 1,
        [HelperOutput("Remain(2) - Keep in untill manually removed")]
        Remain = 2,
    }
}
