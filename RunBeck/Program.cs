using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RunBeck.App_Start;

namespace RunBeck
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Where is the file located? ");
            string fileLocation = Console.ReadLine();
            Console.WriteLine("Is the file format CSV (comma-separated values) or TSV (tab-separated values)?");
            string fileFormat = Console.ReadLine();
            Console.WriteLine("How many fields should each record contain?");
            string numberOfField = Console.ReadLine();

            try
            {
                if (InputValidation(fileLocation, fileFormat, numberOfField))
                {
                    string[] lines = await File.ReadAllLinesAsync(@$"{fileLocation.Trim()}");
                    if (lines.Length > 0)
                    {
                        ContainerConfig.Init();
                        var file = (RunBeckFile)ContainerConfig.GetInstance<IRunBeckFile>();

                        Int32.TryParse(numberOfField, out file.NumberOfField);
                        file.Splitter = fileFormat.Trim().ToLower() == "csv" ? "," : "\t";
                        file.TotalRecords = lines;
                        await file.Process();
                    }
                    Console.WriteLine("Success.");
                    Console.ReadKey();
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        static private bool InputValidation(string fileLocation, string fileFormat, string numberOfField)
        {
            int nof;
            while (string.IsNullOrEmpty(fileLocation))
            {
                Console.WriteLine("Please provide file location:");
                fileLocation = Console.ReadLine();
            }

            while (string.IsNullOrEmpty(fileFormat))
            {
                Console.WriteLine("Please provide file format:");
                fileFormat = Console.ReadLine();
            }

            while (!(fileFormat.ToLowerInvariant() == "csv" || fileFormat.ToLowerInvariant() == "tsv"))
            {
                Console.WriteLine("Unsupported format. Please select CSV or TSV.");
                fileFormat = Console.ReadLine();
            }

            while (string.IsNullOrEmpty(numberOfField))
            {
                Console.WriteLine("Please provide number of field:");
                numberOfField = Console.ReadLine();
            }

            while (!int.TryParse(numberOfField, out nof))
            {
                Console.WriteLine("Please provide a valid number of field:");
                numberOfField = Console.ReadLine();
            }

            return true;
        }
    }
}
