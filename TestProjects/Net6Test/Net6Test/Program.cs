using Microsoft.Extensions.Configuration;
using Net6Test;
using Net6Test.TestEntrances;
using System.Reflection;

var configuration = new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();

var secretValue = configuration["MySecret"];

//using (var fs = new System.IO.StreamWriter("D:\\secret.txt",false))
//{
//    fs.WriteLine(secretValue);
//    fs.Flush();
//}

Console.WriteLine($"The secret value is: {secretValue}");

EntranceBase main
= new F1MainEntrance();
//= new TasksTestsMainEntrance();
var o1 = EntranceBase.b0;
main.MainRun();
var o2 = EntranceBase.b0;
var isSame = object.ReferenceEquals(o1, o2);
var ss = EntranceBase.b0.Base0_Name;



#region End Part

Action EndInfor = () =>
  {
      Console.WriteLine();
      Console.WriteLine($"Any key to exit......");
      while (true)
      {
          var readKey = Console.ReadKey();
          if (readKey.Key == ConsoleKey.Enter) break;
          else Console.Write($" - {readKey.KeyChar} / ");
      }
  };

EndInfor();

#endregion