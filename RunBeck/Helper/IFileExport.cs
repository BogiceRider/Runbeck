using System.Collections.Generic;
using System.Threading.Tasks;

namespace RunBeck
{
    public interface IFileExport
    {
        Task Export(string destination, List<string> data);
    }
}