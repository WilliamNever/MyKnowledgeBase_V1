using Core31TestProject.Models;
using Core31TestProject.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Core31TestProject.Models.AutoMapperTestModels;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Core31TestProject.MainTestFiles
{
    public class FourthBranchMainEntrance : TestBaseForMainEntrance
    {
        private IServiceProvider provider;

        public FourthBranchMainEntrance(IServiceProvider provider)
        {
            this.provider = provider;
        }

        public override void MainRun()
        {
            //Task.WaitAll(FirstTest());
            //Task.WaitAll(FirstTest_1());
            //Task.WaitAll(S_TestAsync());
            //Task.WaitAll(List_Compare_Test());
            //Task.WaitAll(AutoMapper_Test());
            //Task.WaitAll(ObjectRefference_Test());
            //Task.WaitAll(ListSum_Test());
            //Task.WaitAll(ReflectProperty_Test());
            //Task.WaitAll(XmlSerials_Test());
            //Task.WaitAll(RegexReplacement_Test());
            //Task.WaitAll(StringSplitTestAsync());
            Task.WaitAll(DateTimeCovnert());
        }

        private async Task DateTimeCovnert()
        {
            CultureInfo provider = CultureInfo.CurrentUICulture;
            var dtStr = "Monday, June 15, 2009 13:14";
            DateTime dtValue;
            dtValue = DateTime.ParseExact(dtStr, new string[] { "d", "D", "f", "F", "g", "G", "O", "R", "s", "u", "U" }, provider, DateTimeStyles.AllowWhiteSpaces);
            dtValue = DateTime.Parse(dtStr);

            RedefineStr(dtStr);
        }

        private void RedefineStr(string str)
        {
            str = $"{str} - New Added string";
        }

        private async Task StringSplitTestAsync()
        {
            Console.WriteLine(SolvedScenarioService.GetIntFromExcelColumn("FP"));
            Console.WriteLine(SolvedScenarioService.GetIntFromExcelColumn("AU"));
            Console.WriteLine(SolvedScenarioService.GetIntFromExcelColumn("A"));

            List<SDepart> sdps = new List<SDepart>();
            sdps.Add(new SDepart { Dep_Name = "P1", Numbers = 1, Serv_Numbers = 1 });
            sdps.Add(new SDepart { Dep_Name = "P1", Numbers = 1, Serv_Numbers = 2 });
            sdps.Add(new SDepart { Dep_Name = "P1", Numbers = 1, Serv_Numbers = 3 });
            sdps.Add(new SDepart { Dep_Name = "P1", Numbers = 2, Serv_Numbers = 1 });
            sdps.Add(new SDepart { Dep_Name = "P1", Numbers = 2, Serv_Numbers = 2 });
            sdps.Add(new SDepart { Dep_Name = "P1", Numbers = 2, Serv_Numbers = 3 });
            var list = sdps.OrderByDescending(x => x.Numbers).ThenByDescending(x => x.Serv_Numbers).ToList();

            string str = "TimeStamp-|later-than-or-equal";
            var spt = str.Split("-|");
        }
        private async Task RegexReplacement_Test()
        {
            Base0 b2 = null;// new Base2() { Acg = 3 };
            Base2 b0 = (Base2)b2;

            var input = "ssdd0ff1cc23gg4zz5vv";
            Regex regex = new Regex("[\\d]+");
            var matches = regex.Matches(input);

            //foreach (var match in matches.OfType<Match>())
            //{
            //    var str = match.Result("X");
            //}

            MatchEvaluator match = 
                m => { return "X"; };
            var rplStr = regex.Replace(input, match);
        }

        private async Task XmlSerials_Test()
        {
            AppbookFormModel apb = new AppbookFormModel { FormNumber = "18443B-AK", Sequence = 1 };
            AppBooksModel alb = new AppBooksModel()
            {
                AppBooks = new List<AppBookModel> {
                    new AppBookModel{
                        AppBookForm="AppBookForm",
                        CoverLeft="CoverLeft",
                        CoverRight="CoverRight",
                        Division="Division",
                        FeedId=33,
                        Title="Title",
                        Forms= new List<AppbookFormModel>{ apb}
                    },
                    new AppBookModel{
                        AppBookForm="AppBookForm",
                        CoverLeft="CoverLeft",
                        CoverRight="CoverRight",
                        Division="Division",
                        FeedId=44,
                        Title="Title",
                        Forms= new List<AppbookFormModel>{ apb}
                    }
                }
            }
            ;

            var cver = new XMLConverter<AppBooksModel>();
            string rsl = cver.Serializer(alb);
        }

        private async Task ReflectProperty_Test()
        {
            var p1 = new SDepart { Dep_Name = "P1", Numbers = 1, Serv_Numbers = 1 };
            //Expression<Func<string>> propertyExpression = () => p1.Dep_Name;
            var ss = PropertyName(() => p1.Dep_Name);
            var ss1 = PropertyName(() => p1.Numbers);
            var ss2 = PropertyName(() => p1.Serv_Numbers);
            
            var sb = nameof(p1.Dep_Name);
            var sb1 = nameof(p1.Numbers);
            var sb2 = nameof(p1.Serv_Numbers);
            var sb3 = nameof(p1.GetHH);
        }

        private static string PropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            return (propertyExpression.Body as MemberExpression).Member.Name;
        }

        private async Task ListSum_Test()
        {
            List<SDepart> list = new List<SDepart>();
            list.Add(new SDepart { Numbers = null });
            for (int i = 0; i < 5; i++)
            {
                list.Add(new SDepart { Numbers = i + 1 });
            }
            var itm = list[0];
            var itm1 = new SDepart { Numbers = 22 };
            itm = itm1;
            var total = list.Sum(x => x.Numbers ?? 10);
        }

        private async Task ObjectRefference_Test()
        {
            string sdc = null;
            bool isConverted = DateTime.TryParse(sdc, out _);

            var ssl = float.IsNaN(0.0f / 0.0f);
            var ssm = 5.0f / 0.0f == float.PositiveInfinity;
            var isConvert = float.TryParse("11.1", out _);
            var isInt = int.TryParse("11.1", out _);

            var p1 = new SDepart { Dep_Name = "P1", Numbers = 1, Serv_Numbers = 1 };
            var p3 = new SDepart { Dep_Name = "P1", Numbers = 1, Serv_Numbers = 1 };
            var p2 = new SDepart { Dep_Name = "P2", Numbers = 2, Serv_Numbers = 2 };
            var p4 = p3;
            p4 = p2;

            var isSame = object.ReferenceEquals(p1, p2);
            isSame = object.ReferenceEquals(p1, p3);
            isSame = object.ReferenceEquals(p4, p3);
            await AutoMapper_Test(p1);
        }

        private async Task AutoMapper_Test(SDepart ss = null)
        {
            if (ss != null) ss.Dep_Name = "ssss_dep";

            var mapper = provider.GetService<IMapper>();
            var sCmp = new SourceCompany
            {
                Base_Name = "base_name",
                Ss_Name = "SS_comp_Name",
                Departs = new List<SDepart> {
                    new SDepart{Dep_Name="P1", Numbers=3, Serv_Numbers=1 },
                    new SDepart{Dep_Name="P2", Numbers=5, Serv_Numbers=2 },
                },
            };
            var dest = mapper.Map<DestCompany>(sCmp);
        }

        private async Task List_Compare_Test()
        {
            List<dynamic> list = new List<dynamic>();
            list.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(@"{""N"":""1""}"));
            list.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(@"{""N"":""2""}"));
            list.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(@"{""N"":""1""}"));
            list.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(@"{""N"":""2""}"));
            for (int i = 0; i < 10; i++)
            {
                dynamic itm = new { Num = i&3 };
                list.Add(itm);
            }

            var distincted = list.Distinct(new Compares<dynamic>((x,y)=> {
                var ValX = x.Num ?? x.N;
                var ValY = y.Num ?? y.N;
                return ValX == ValY;
            })).ToList();

            var dyn = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(@"{N:1}");
            var vl1 = dyn.N?.ToString();
            var vl = dyn.Nw?.ToString();

            List<string> mm = new List<string> { "ss", "bb" };
            var sstrsb = string.Join("&", mm);
        }

        private async Task S_TestAsync()
        {
            var str = Newtonsoft.Json.JsonConvert.SerializeObject(new DimensionItem { Id = "1" });
            var deStr = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(@"{""Id"":""1"",""Name"":null,""ContextPath"":null,""Products"":0}") as Newtonsoft.Json.Linq.JObject;
            var deToStr = deStr.ToString();
            var jobj = new Newtonsoft.Json.Linq.JObject(deStr);
            var jstr = jobj.ToString();
            var sDo = Newtonsoft.Json.JsonConvert.DeserializeObject<DimensionItem>(
                //Newtonsoft.Json.JsonConvert.SerializeObject(deStr)
                jobj.ToString()
                );
        }

        private async Task FirstTest_1()
        {
            string[] ResetValueKey = new string[] { "111", "222" };
            List<DimensionItem> pairs = new List<DimensionItem>
            {
                new DimensionItem{ Id="1" },
                new DimensionItem{ Id="2" },
                new DimensionItem{ Id="3" },
                new DimensionItem{ Id="4" },
                new DimensionItem{ Id="5" }
            };

            var list = pairs.FindAll(x => x.Id == "1" || x.Id == "3");
            foreach (var itm in list)
            {
                itm.Name = "xxx";
                var inlistIndex = list.IndexOf(itm);
                int index = pairs.IndexOf(itm);
                pairs[index] = new DimensionItem { Id = itm.Id, Products = 99 };
            }
        }
        private async Task FirstTest()
        {
            string[] ResetValueKey = new string[] { "111", "222" };
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("111", "111"),
                new KeyValuePair<string, string>("222", "222"),
                new KeyValuePair<string, string>("333", "333")
            };

            foreach (var key in ResetValueKey)
            {
                var index = pairs.FindIndex(x => x.Key == key);
                if (index > -1)
                    pairs[index] = new KeyValuePair<string, string>(key, "xxx");
            }

            var list = pairs.FindAll(x => x.Key == "111" || x.Key == "333");
            foreach (var itm in list)
            {
                //int index = pairs.IndexOf(new KeyValuePair<string, string>(itm.Key, itm.Value));
                int index = pairs.IndexOf(itm);
                pairs[index] = new KeyValuePair<string, string>(itm.Key, "vvv");
            }
        }
    }
}
