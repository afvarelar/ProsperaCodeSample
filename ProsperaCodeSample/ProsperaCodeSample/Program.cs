using System;
using System.IO;
using static System.Console;

namespace ProsperaCodeSample
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string originPath = string.Empty;
            string outputPathAsync = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\outputFileAsync.txt";
            string outputPathMultiThreaded = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\outputFileMultithreaded.txt";
            bool firstTry = true;
            while (String.IsNullOrEmpty(originPath) || !File.Exists(originPath) || Path.GetExtension(originPath).ToLower() != ".txt")
            {
                if (firstTry)
                    firstTry = false;
                else
                    WriteLine("Invalid location or extension, only txt file accepted.");

                WriteLine("Input the log file location: ");
                originPath = ReadLine();
            }
            /*
             * This class contains asynchronous methods that process the file without holding up the application, but it does not read the file continously while writing.
             */
            AsyncLogParser.processFileAsync(originPath, outputPathAsync).Wait();

            /*
             * This class contains a simple method that triggers a parallel thread loop to read the file and validate the expected string, while at the same time
             * triggering the function to write the results into a file, in this case the ReaderWriterLock helps to manage the multiple threads triggering the same file.
             */
            MultiThreadedLogParser.RunParser(originPath, outputPathMultiThreaded);

            WriteLine($"You can find the results in {outputPathAsync} or {outputPathMultiThreaded}");

            Console.ReadKey();
        }

        
    }
}
