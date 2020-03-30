namespace Stark.DataAccessLayer
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class FileService : IFileService
    {
        public async Task WriteTextToFileAsync(string directoryPath, string fileName, string content, bool append = false)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                throw new ArgumentException($"{nameof(directoryPath)} cannot be null, empty, or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException($"{nameof(fileName)} cannot be null, empty, or whitespace.");
            }

            if (!Directory.Exists(directoryPath))
            {
                await Task.Run(() =>
                {
                    try
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    catch (UnauthorizedAccessException uae)
                    {
                        throw uae;
                    }
                    catch (PathTooLongException ptle)
                    {
                        throw ptle;
                    }
                    catch (IOException ioe)
                    {
                        throw ioe;
                    }
                    catch (Exception ex)
                    {
                        throw new IOException($"An exception occurred when trying to create the directory {directoryPath}. See inner exception.", ex);
                    }
                });
            }

            await Task.Run(() =>
            {
                try
                {
                    string fullPath = Path.Combine(directoryPath, fileName);
                    if (append && File.Exists(fullPath))
                    {
                        string existingContents = File.ReadAllText(fullPath);
                        content = existingContents + Environment.NewLine + content;
                    }

                    File.WriteAllText(Path.Combine(directoryPath, fileName), content);
                }
                catch (UnauthorizedAccessException uae)
                {
                    throw uae;
                }
                catch (PathTooLongException ptle)
                {
                    throw ptle;
                }
                catch (IOException ioe)
                {
                    throw ioe;
                }
                catch (Exception ex)
                {
                    throw new IOException($"An exception occurred when trying to write to file {fileName}. See inner exception.", ex);
                }
            });
        }
    }
}
