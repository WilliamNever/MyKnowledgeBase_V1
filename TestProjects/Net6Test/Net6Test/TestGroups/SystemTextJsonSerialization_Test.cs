using Net6Test.Enums;
using System;
using System.Text.Json.Serialization.Metadata;

namespace Net6Test.TestGroups
{
    public class SystemTextJsonSerialization_Test
    {
        public static void SerializeDeS_Test()
        {
            bool showUnshown = true;
            var sopt = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
            };
            sopt.TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                    {
                        typeInfo =>
                        {
                            if (typeInfo.Type == typeof(ClassModel))
                            {
                                var prop = typeInfo.Properties
                                    .FirstOrDefault(p => p.Name == nameof(ClassModel.UnShown));

                                if (prop is not null)
                                {
                                    prop.ShouldSerialize = (_, _) => showUnshown;
                                }
                            }
                        }
                    }
            };

            var cm = new ClassModel();
            var js = System.Text.Json.JsonSerializer.Serialize(cm, sopt);



            var objModel = System.Text.Json.JsonSerializer.Deserialize<ClassModel>(js);

            var json = @"
            {
              ""Id"": ""95975a9f-a133-4fe0-9175-c86283ec7c82"",
              ""Name"": ""Init"",
              ""IndexOrder"": 10,
              ""Week"": ""6"",
              ""Operations_A"": ""15"",
              ""Operations_B"": 15,
              ""Operations_C"": 12,
              ""Operations_D"": 14
            }
            ";
            var jsOptions = new System.Text.Json.JsonSerializerOptions()
            {
                WriteIndented = true,
            };
            jsOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            var objModel1 = System.Text.Json.JsonSerializer.Deserialize<ClassModel>(json, jsOptions);
            var js1 = System.Text.Json.JsonSerializer.Serialize(objModel1, jsOptions);
        }
    }

    public class ClassModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Init";
        public int IndexOrder { get; set; } = 10;
        public EnWeek Week { get; set; } = EnWeek.Sun;
        public EnOp Operations_A { get; set; } = EnOp.A_B | EnOp.C | EnOp.D;
        public EnOp Operations_B { get; set; } = EnOp.A | EnOp.B | EnOp.C | EnOp.D;
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public EnOp Operations_C { get; set; } = EnOp.C | EnOp.D;
        [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumConverter))]
        public EnOp Operations_D { get; set; } = EnOp.B | EnOp.C | EnOp.D;
        //[System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public string? UnShown { get; set; } = null;
    }

}
