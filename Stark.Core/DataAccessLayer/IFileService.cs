namespace Stark.DataAccessLayer
{
    using Stark.ViewModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IFileService
    {
        Task WriteTextToFileAsync(string directoryPath, string fileName, string content, bool append = false);
    }
}