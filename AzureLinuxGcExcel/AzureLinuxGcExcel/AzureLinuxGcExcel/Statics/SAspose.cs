using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace GcExcel_Performance
{
    class SAspose
    {
        private static string OutFilePath;

        static SAspose()
        {
            OutFilePath = Path.Combine("Files", "Output", "Aspose");
        }

        public static void Start(String inputFile, out double openTime, out double saveTime, out double calcTime, out double memSizeAfterOpen, out double memSizeAfterCalc)
        {

            var fileName = Path.GetFileName(inputFile);

            Console.WriteLine("Benchmark for Aspose.Cells");
            Console.WriteLine();

            Console.WriteLine("FileName: \"" + fileName + "\"");
            Console.WriteLine();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Workbook workbook = new Workbook(inputFile);
            stopwatch.Stop();
            openTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Open time:      " + openTime.ToString("0.###") + "s");

            long memorySize = GC.GetTotalMemory(true);
            memSizeAfterOpen = (memorySize / 1024d / 1024d);
            Console.WriteLine("Used Memory:            " + memSizeAfterOpen.ToString("##.###") + "M");
            Console.WriteLine();
            stopwatch.Restart();
            workbook.CalculateFormula(new CalculationOptions() { Recursive = true });
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
            workbook.Save(Path.Combine(OutFilePath, fileName));
            stopwatch.Stop();
            saveTime = (stopwatch.ElapsedMilliseconds / 1000d);
            Console.WriteLine("Save time       " + saveTime.ToString("0.###") + "s");

            memorySize = GC.GetTotalMemory(true);
            Console.WriteLine("Used Memory:            " + (memorySize / 1024d / 1024d).ToString("##.###") + "M");
            Console.WriteLine();

            // Prevent the GC collect the workbook before we show the memory size.
            workbook.Worksheets[0].Cells[0, 0].Value = 1;
        }
    }
}
