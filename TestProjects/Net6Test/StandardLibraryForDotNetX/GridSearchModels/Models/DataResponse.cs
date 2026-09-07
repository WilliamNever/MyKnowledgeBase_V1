namespace GridSearchModels.Models
{
    public class DataResponse<T>
    {
        public bool IsSuccessful { get; set; } = true;
        public int TotalRows { get; set; }
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
