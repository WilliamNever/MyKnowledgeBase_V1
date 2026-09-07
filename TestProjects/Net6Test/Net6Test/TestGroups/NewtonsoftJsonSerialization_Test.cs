using Net6Test.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Net6Test.TestGroups
{
    public class NewtonsoftJsonSerialization_Test
    {
        public static void OptionalPropertySerialization_Test()
        {
            var cm = new ClassModelForNewtonJson();
            var json1 = JsonConvert.SerializeObject(cm, Formatting.Indented);

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CustomerContractResolver(new[] { "UnShownEx" }),  //""
                Formatting = Formatting.Indented
            };

            var json = JsonConvert.SerializeObject(cm, settings);
        }
    }

    public class ClassModelForNewtonJson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Init";
        public int IndexOrder { get; set; } = 10;
        public EnWeek Week { get; set; } = EnWeek.Sun;
        public EnOp Operations_A { get; set; } = EnOp.A_B | EnOp.C | EnOp.D;
        public EnOp Operations_B { get; set; } = EnOp.A | EnOp.B | EnOp.C | EnOp.D;
        public EnOp Operations_C { get; set; } = EnOp.C | EnOp.D;
        public EnOp Operations_D { get; set; } = EnOp.B | EnOp.C | EnOp.D;
        public string? UnShownEx { get; set; } = null;
        public string? UnShown { get; set; } = null;

        public bool ShouldSerializeUnShown()
        {
            return false;
        }
    }

    public sealed class CustomerContractResolver : DefaultContractResolver
    {
        private readonly HashSet<string> _hiddenProperties;

        public CustomerContractResolver(IEnumerable<string> hiddenProperties)
        {
            _hiddenProperties = new HashSet<string>(hiddenProperties, StringComparer.OrdinalIgnoreCase);
        }

        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (_hiddenProperties.Contains(property.PropertyName!))
            {
                property.PropertyName = $"{member.Name}+XXR";
                property.ShouldSerialize = _ => {
                    var obj = _;
                    return true; 
                } ;

                //property.ShouldSerialize = _ => false;
            }

            return property;
        }
    }
}
