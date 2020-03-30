using System.Management;

namespace Stark.DataAccessLayer
{
    public interface IWmiService
    {
        ManagementScope ConnectToRemoteWmi(string hostname, string scope, ConnectionOptions options);
    }
}