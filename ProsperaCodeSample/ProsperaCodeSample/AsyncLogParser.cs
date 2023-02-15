using System;
using System.IO;
using System.Threading.Tasks;

namespace ProsperaCodeSample
{
    public static class AsyncLogParser
    {
        private static async void writeText(string text, string outputPath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(outputPath, true))
                {
                    await sw.WriteLineAsync(text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task processFileAsync(string originPath, string outputPath)
        {
            string result = string.Empty;
            try
            {
                using (StreamReader file = new StreamReader(originPath))
                {
                    string ln;

                    while ((ln = await file.ReadLineAsync().ConfigureAwait(false)) != null)
                    {
                        if (ln.Contains("Donec"))
                        {
                            writeText(ln, outputPath);
                        }
                    }
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
