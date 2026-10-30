namespace StandardLibraryForDotNetX.Annotations
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class InforNameAttribute : Attribute
    {
        public string Name { get; set; }
        public InforNameAttribute(string name)
        {
            Name = name;
        }
    }
}
