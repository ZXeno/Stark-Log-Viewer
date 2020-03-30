namespace Stark.Extensions
{
    using Stark.Models;
    using System.Collections.Generic;
    using System.Text.Json;

    public static class LogModelExtensions
    {
        /// <summary>
        /// Converts a <see cref="LogModel"/> to JSON formatted string with pretty print.
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static string ToJson(this LogModel log)
        {
            return JsonSerializer.Serialize<LogModel>(log, new JsonSerializerOptions() { WriteIndented = true });
        }

        /// <summary>
        /// Gets a shortened version of a <see cref="LogModel"/> Message property value based on the proposed length.
        /// </summary>
        /// <param name="log">The <see cref="LogModel"/></param>
        /// <param name="proposedLength">Int value of the desired length.</param>
        /// <returns></returns>
        public static string GetShortenedMessageString(this LogModel log, int proposedLength)
        {
            if (log.Message.Length >= proposedLength)
            {
                return log.Message.Substring(0, proposedLength) + "...";
            }

            return log.Message;
        }

        /// <summary>
        /// Converts a <see cref="List{LogModel}"/> to a JSON formatted string without pretty print.
        /// </summary>
        /// <param name="logs">The <see cref="List{LogModel}"/> collection to convert.</param>
        /// <returns>JSON formatted string.</returns>
        public static string ToJson(this List<LogModel> logs)
        {
            if (logs == null)
            {
                return "{ }";
            }

            return LogModelExtensions.ToJson(logs, false);
        }

        /// <summary>
        /// Converts a <see cref="List{LogModel}"/> to a JSON formatted string without pretty print.
        /// </summary>
        /// <param name="logs">The <see cref="List{LogModel}"/> collection to convert.</param>
        /// <param name="prettyPrint">Flag determining whether to use pretty print JSON.</param>
        /// <returns></returns>
        public static string ToJson(this List<LogModel> logs, bool prettyPrint)
        {
            if (logs == null)
            {
                return "{ }";
            }

            return JsonSerializer.Serialize<List<LogModel>>(logs, new JsonSerializerOptions() { WriteIndented = prettyPrint });
        }
    }
}
