namespace Stark.Extensions
{
    using Stark.Models;
    using System.Text.Json;

    public static class LogModelExtensions
    {

        public static string ToJson(this LogModel log)
        {
            return JsonSerializer.Serialize<LogModel>(log, new JsonSerializerOptions() { WriteIndented = true });
        }

        public static string GetShortenedMessageString(this LogModel log, int proposedLength)
        {
            if (log.Message.Length >= proposedLength)
            {
                return log.Message.Substring(0, proposedLength) + "...";
            }

            return log.Message;
        }
    }
}
