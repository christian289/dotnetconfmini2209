using System.Diagnostics;
using System;
using GrapeCity.Documents.Excel;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace AzureLinuxGcExcel.Library
{
    public class MyGcExcel
    {
        public MyGcExcel()
        {
        }

        public MyGcExcel(string inputFile)
        {
            FileName = Path.GetFileName(inputFile);

            Console.WriteLine("Benchmark for GcExcel");
            Console.WriteLine();

            Console.WriteLine("FileName: \"" + FileName + "\"");
            Console.WriteLine();

            Workbook workbook = new Workbook();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            workbook.Open(inputFile);
            stopwatch.Stop();
            OpenTime = stopwatch.ElapsedMilliseconds / 1000d;
            Console.WriteLine("Open time:      " + OpenTime.ToString("0.###") + "s");

            long memorySize = GC.GetTotalMemory(true);
            MemSizeAfterOpen = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + MemSizeAfterOpen.ToString("##.###") + "M");
            Console.WriteLine();
            stopwatch.Restart();
            workbook.Dirty();
            workbook.Calculate();
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
            workbook.Save(Path.Combine(OutFilePath, FileName), null, new SaveOptions() { IsCompactMode = true });
            stopwatch.Stop();
            SaveTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Save time       " + SaveTime.ToString("0.###") + "s");

            memorySize = GC.GetTotalMemory(true);
            var memSizeAfterSave = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + memSizeAfterSave.ToString("##.###") + "M");
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
