namespace Stark
{
    using Stark.Extensions;
    using Stark.Models;
    using Stark.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal static class LogFormatter
    {
        public static async Task<string> FormatLogsByExtensionAsync(string extension, IEnumerable<LogViewModel> logs)
        {
            return await Task.Run(() =>
            {
                IEnumerable<LogModel> logModels = logs.Select(x => x.LogModel);

                switch (extension.ToLower())
                {
                    case "json":
                        return FormatLogAsJson(logModels);
                    case "txt":
                        return FormatLogAsText(logModels);
                    default:
                        return FormatLogAsText(logModels);
                }
            });
        }

        private static string FormatLogAsText(IEnumerable<LogModel> logModels)
        {
            StringBuilder sb = new StringBuilder();

            foreach (LogModel log in logModels)
            {
                sb.AppendLine(log.ToString("f"));
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private static string FormatLogAsJson(IEnumerable<LogModel> logs)
        {
            return logs.ToJson(true);
        }
    }
}
