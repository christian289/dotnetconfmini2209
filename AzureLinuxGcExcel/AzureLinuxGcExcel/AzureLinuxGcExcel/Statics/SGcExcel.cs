using GrapeCity.Documents.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GcExcel_Performance
{
    class SGcExcel
    {
        private static string OutFilePath;

        static SGcExcel()
        {
            OutFilePath = Path.Combine("Files", "Output", "GcExcel");
        }

        public static void Start(String inputFile, out double openTime, out double saveTime, out double calcTime, out double  memSizeAfterOpen, out double memSizeAfterCalc)
        {

            var fileName = Path.GetFileName(inputFile);

            Console.WriteLine("Benchmark for GcExcel");
            Console.WriteLine();

            Console.WriteLine("FileName: \"" + fileName + "\"");
            Console.WriteLine();

            Workbook workbook = new Workbook();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            workbook.Open(inputFile);
            stopwatch.Stop();
            openTime = stopwatch.ElapsedMilliseconds / 1000d;
            Console.WriteLine("Open time:      " + openTime.ToString("0.###") + "s");

            long memorySize = GC.GetTotalMemory(true);
            memSizeAfterOpen = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + memSizeAfterOpen.ToString("##.###") + "M");
            Console.WriteLine();
            stopwatch.Restart();
            workbook.Dirty();
            workbook.Calculate();
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
            workbook.Save(Path.Combine(OutFilePath, fileName), null, new SaveOptions() { IsCompactMode = true });
            stopwatch.Stop();
            saveTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Save time       " + saveTime.ToString("0.###") + "s");

            memorySize = GC.GetTotalMemory(true);
            var memSizeAfterSave = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + memSizeAfterSave.ToString("##.###") + "M");
            Console.WriteLine();

            // Prevent the GC collect the workbook before we show the memory size.
            workbook.Worksheets[0].Cells[0, 0].Value = 1;
        }
    }
}
