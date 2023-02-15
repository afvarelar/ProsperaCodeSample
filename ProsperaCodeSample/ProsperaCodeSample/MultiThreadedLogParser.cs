using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ProsperaCodeSample
{
    public static class MultiThreadedLogParser
    {
        private static ReaderWriterLock locker = new ReaderWriterLock();
        public static void WriteData(this string text, string path)
        {
            try
            {
                // This line is just to test if the parser is still reading the file even if this call takes time.
                //Thread.Sleep(5000);
                locker.AcquireWriterLock(int.MaxValue);
                File.AppendAllLines(path, new[] { text });
            }
            finally
            {
                locker.ReleaseWriterLock();
            }
        }
        public static void RunParser(string originPath, string outputPath)
        {
            Parallel.ForEach(File.ReadLines(originPath), (line, _, lineNumber) =>
            {
                if (line.Contains("Donec"))
                {
                    WriteData(line, outputPath);
                }
            });
        }
    }
}
