﻿using ConsolePreTest.Tests;

var dtDes = DateTime.Now.AddDays(34);
Console.WriteLine($"{dtDes}");


//CommonTest.UTTest1();
//CommonTest.UTTest2();
//CommonTest.UTTest3();
//CommonTest.UTStackTest();
//CommonTest.UTTimerTest();
//CommonTest.JSEncodingTest();
//CommonTest.ThreadTimerTest();

//SecTest.OpenTest1();
//SecTest.DateTimeUtcConverTest();

await CronoExp_Test.CronoExpress_Test();



Console.WriteLine();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine("Ending ......");
Console.ReadLine();
