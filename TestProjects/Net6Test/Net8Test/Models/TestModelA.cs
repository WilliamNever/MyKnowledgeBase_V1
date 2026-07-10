using System.Diagnostics.CodeAnalysis;

namespace Net8Test.Models
{
    public class TestModelA
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public TestModelA()
        {
            
        }

        [SetsRequiredMembers]
        public TestModelA(string name)
        {
            Name = name;
        }
    }
}
