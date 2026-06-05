namespace Net6Test.Models
{
    public class TTempModel<T>
    {
        public T? Temp { get; set; } = default;

        //var t1 = new TTempModel<int>();   t1.Temp = 0
        //var t2 = new TTempModel<int?>();  t1.Temp = null
    }
}
