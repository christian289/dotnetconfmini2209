using SpreadsheetGear;
using System.Diagnostics;
using System;
using Aspose.Cells;
using System.ComponentModel.DataAnnotations;

namespace AzureLinuxGcExcel.Library
{
    public class MySSG
    {
        public MySSG()
        {

        }

        public MySSG(string inputFile)
        {
            FileName = Path.GetFileName(inputFile);
            Console.WriteLine("Benchmark for SpreadSheetGear");
            Console.WriteLine();

            Console.WriteLine("FileName: \"" + FileName + "\"");
            Console.WriteLine();

            IWorkbookSet workbookSet = SpreadsheetGear.Factory.GetWorkbookSet();
            workbookSet.CalculationOnDemand = false;
            workbookSet.BackgroundCalculation = false;


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var workbook = workbookSet.Workbooks.Open(inputFile);
            stopwatch.Stop();
            OpenTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Open time:      " + OpenTime.ToString("0.###") + "s");

            long memorySize = GC.GetTotalMemory(true);
            MemSizeAfterOpen = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + MemSizeAfterOpen.ToString("##.###") + "M");
            Console.WriteLine();
            stopwatch.Restart();
            workbookSet.CalculateFull();
            stopwatch.Stop();
            CalcTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Calclate time   " + CalcTime.ToString("0.###") + "s");

            memorySize = GC.GetTotalMemory(true);
            MemSizeAfterCalc = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + MemSizeAfterCalc.ToString("##.###") + "M");
            Console.WriteLine();

            if (!Directory.Exists(OutFilePath))
            {
                Directory.CreateDirectory(OutFilePath);
            }

            stopwatch.Restart();
            workbook.SaveAs(Path.Combine(OutFilePath, FileName), FileFormat.OpenXMLWorkbook);
            stopwatch.Stop();
            SaveTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Save time       " + SaveTime.ToString("0.###") + "s");

            memorySize = GC.GetTotalMemory(true);
            Console.WriteLine("Used Memory:            " + (memorySize / 1024d / 1024d).ToString("##.###") + "M");
            Console.WriteLine();
        }


        private string OutFilePath = Path.Combine("Files", "Output", "SpreadsheetGear");

        [Display(Name = "FileName")]
        public string FileName { get; set; }

        [Display(Name = "OpenTime")]
        public double OpenTime { get; set; }

        [Display(Name = "SaveTime")]
        public double SaveTime { get; set; }

        [Display(Name = "CalcTime")]
        public double CalcTime { get; set; }

        [Display(Name = "MemSizeAfterOpen")]
        public double MemSizeAfterOpen { get; set; }

        [Display(Name = "MemSizeAfterCalc")]
        public double MemSizeAfterCalc { get; set; }

    }
}
