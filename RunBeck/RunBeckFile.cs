using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RunBeck
{
    public class RunBeckFile : IRunBeckFile
    {
        public int NumberOfField;

        public string Splitter;

        public string[] TotalRecords;

        public List<string> CorrectRecords;

        public List<string> IncorrectRecords;

        private readonly IFileExport _fileExport;

        public RunBeckFile(IFileExport fileExport)
        {
            this._fileExport = fileExport;
            TotalRecords = new string[] { };
            CorrectRecords = new List<string>();
            IncorrectRecords = new List<string>();
        }

        public async Task Process()
        {
            try
            {
                foreach (var record in TotalRecords.Skip(1).ToArray())
                {
                    if (record.Split(Splitter).Length == NumberOfField)
                    {
                        CorrectRecords.Add(record);
                    }
                    else
                    {
                        IncorrectRecords.Add(record);
                    }
                }

                if (CorrectRecords.Count > 0)
                {
                    await _fileExport.Export(@"C:\Output\CorrectFormat.txt", CorrectRecords);
                }
                if (IncorrectRecords.Count > 0)
                {
                    await _fileExport.Export(@"C:\Output\IncorrectFormat.txt", IncorrectRecords);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
