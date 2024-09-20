using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimerNotificatoin.Core.Attributes;

namespace TimerNotificatoin.Core.Enums
{
    [Flags]
    public enum EnNotificationType
    {
        [HelperOutput("UnKnown - not classified")]
        UnKnown = 0,
        [HelperOutput("Common - popped up then removed")]
        Common = 1,
        [HelperOutput("Remain - Keep in untill manually removed")]
        Remain = 2,
    }
}
