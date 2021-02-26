using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RunBeck.Helper
{
    public class FileExport : IFileExport
    {
        public async Task Export(string path, List<string> data)
        {
            try
            {
                await File.WriteAllLinesAsync(path, data);
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
