namespace Stark.DataAccessLayer
{
    using System;
    using System.Management;

    public class WmiService
    {
        // TODO: Logging

        public WmiService()
        {

        }

        public ManagementScope ConnectToRemoteWmi(string hostname, string scope, ConnectionOptions options)
        {
            string wminamespace = $"\\\\{hostname}{scope}";

            try
            {
                var wmiscope = new ManagementScope(wminamespace, options);
                wmiscope.Connect();

                return wmiscope;
            }
            catch (UnauthorizedAccessException uae)
            {
                this.LogExceptionMessage(wminamespace, uae.Message);
                throw new UnauthorizedAccessException($"Failed to connect to WMI namespace {wminamespace}: {uae.Message}", uae);
            }
            catch (Exception e)
            {
                this.LogExceptionMessage(wminamespace, e.Message);
                throw new Exception($"Failed to connect to WMI namespace {wminamespace}", e);
            }

        }

        private void LogExceptionMessage(string wmiNamespace, string exceptionMessage)
        {
            // TODO: Replace with logger service.
            Console.WriteLine($"Failed to connect to WMI namespace {wmiNamespace}");
            Console.WriteLine($"Exception message: {exceptionMessage}");
        }
    }
}
