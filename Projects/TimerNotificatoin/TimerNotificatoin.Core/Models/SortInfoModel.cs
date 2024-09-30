using TimerNotificatoin.Core.Enums;

namespace TimerNotificatoin.Core.Models
{
    public record SortInfoModel(string DataPropertyName, EnOrderBy OrderBy)
    {
        public string DataPropertyName { get; set; } = DataPropertyName;
        public EnOrderBy OrderBy { get; set; } = OrderBy;
        public void ReverOrderBy()
        {
            OrderBy ^= EnOrderBy.desc;
        }
    }
}
