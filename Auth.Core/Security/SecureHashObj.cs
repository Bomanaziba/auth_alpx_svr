


namespace Auth.Core.Security
{

    public class SecureHashObj
    {
        public SecureHashObj(string salt, string data, string secureData)
        {
            Salt = salt;
            Data = data;
            SecureHash = secureData;
        }

        public string Salt { get; set; }
        public string Data { get; set; }
        public string SecureHash { get; set; }
    }
    
}