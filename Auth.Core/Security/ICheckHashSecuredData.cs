

namespace Auth.Core.Security
{
    public interface ICheckHashSecuredData
    {
        bool IsHashSecureDataEqual(string data, string secureData, string salt);
    }
}

