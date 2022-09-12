using SpreadsheetGear;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GcExcel_Performance
{
    class SSSG
    {
        private static string OutFilePath;
        
        static SSSG()
        {
            SpreadsheetGear.Factory.SetSignedLicense("SpreadsheetGear.License, Type=Trial, Product=BND, Expires=2022-10-09, Company=Dylan Hwang, Email=hdg8902@naver.com, Signature=P+km7k160YponIw8e+yDGMOOjQw7rffXEDKBg2SuufcA#ZyoyiGasjc/hD5YWV7zs2QEYQydXpoAINUmZHqBgG+IA#J");

            OutFilePath = Path.Combine("Files", "Output", "SSG");
        }

        public static void Start(String inputFile, out double openTime, out double saveTime, out double calcTime, out double memSizeAfterOpen, out double memSizeAfterCalc)
        {
            var fileName = Path.GetFileName(inputFile);
            Console.WriteLine("Benchmark for SpreadSheetGear");
            Console.WriteLine();

            Console.WriteLine("FileName: \"" + fileName + "\"");
            Console.WriteLine();

            IWorkbookSet workbookSet = SpreadsheetGear.Factory.GetWorkbookSet();
            workbookSet.CalculationOnDemand = false;
            workbookSet.BackgroundCalculation = false;


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var workbook = workbookSet.Workbooks.Open(inputFile);
            stopwatch.Stop();
            openTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Open time:      " + openTime.ToString("0.###") + "s");

            long memorySize = GC.GetTotalMemory(true);
            memSizeAfterOpen = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + memSizeAfterOpen.ToString("##.###") + "M");
            Console.WriteLine();
            stopwatch.Restart();
            workbookSet.CalculateFull();
            stopwatch.Stop();
            calcTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Calclate time   " + calcTime.ToString("0.###") + "s");

            memorySize = GC.GetTotalMemory(true);
            memSizeAfterCalc = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + memSizeAfterCalc.ToString("##.###") + "M");
            Console.WriteLine();

            if (!Directory.Exists(OutFilePath))
            {
                Directory.CreateDirectory(OutFilePath);
            }

            stopwatch.Restart();
            workbook.SaveAs(Path.Combine(OutFilePath, fileName), FileFormat.OpenXMLWorkbook);
            stopwatch.Stop();
            saveTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Save time       " + saveTime.ToString("0.###") + "s");

            memorySize = GC.GetTotalMemory(true);
            Console.WriteLine("Used Memory:            " + (memorySize / 1024d / 1024d).ToString("##.###") + "M");
            Console.WriteLine();

            workbook.Worksheets[0].Cells[0, 0].Value = 1;
        }
    }
}
