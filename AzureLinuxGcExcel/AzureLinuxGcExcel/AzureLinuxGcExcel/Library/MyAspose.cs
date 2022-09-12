using Aspose.Cells;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace AzureLinuxGcExcel.Library
{
    public class MyAspose
    {
        public MyAspose()
        {

        }

        public MyAspose(string inputFile)
        {
            FileName = Path.GetFileName(inputFile);

            Console.WriteLine("Benchmark for Aspose.Cells");
            Console.WriteLine();

            Console.WriteLine("FileName: \"" + FileName + "\"");
            Console.WriteLine();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Workbook workbook = new Workbook(inputFile);
            stopwatch.Stop();
            OpenTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Open time:      " + OpenTime.ToString("0.###") + "s");

            long memorySize = GC.GetTotalMemory(true);
            MemSizeAfterOpen = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + MemSizeAfterOpen.ToString("##.###") + "M");
            Console.WriteLine();
            stopwatch.Restart();
            workbook.CalculateFormula(new CalculationOptions() { Recursive = true });
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
            workbook.Save(Path.Combine(OutFilePath, FileName));
            stopwatch.Stop();
            SaveTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Save time       " + SaveTime.ToString("0.###") + "s");

            memorySize = GC.GetTotalMemory(true);
            Console.WriteLine("Used Memory:            " + (memorySize / 1024d / 1024d).ToString("##.###") + "M");
            Console.WriteLine();

        }

        private string OutFilePath = Path.Combine("Files", "Output", "Aspose");

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
