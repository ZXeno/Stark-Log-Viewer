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
            try
            {
                var wmiscope = new ManagementScope($"\\\\{hostname}{scope}", options);
                wmiscope.Connect();
                return wmiscope;
            }
            catch (Exception e)
            {
                // TODO: Replace with logger service.
                Console.WriteLine($"Failed to connect to WMI namespace \\\\{hostname}{scope}");
                Console.WriteLine($"Exception message: {e.Message}");

                return null;
            }

        }
    }
}
