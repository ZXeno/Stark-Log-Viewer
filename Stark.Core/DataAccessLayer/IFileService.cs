namespace Stark.DataAccessLayer
{
    using System.Threading.Tasks;

    public interface IFileService
    {
        Task WriteTextToFileAsync(string directoryPath, string fileName, string content, bool append = false);
    }
}