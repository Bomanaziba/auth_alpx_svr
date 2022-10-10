

using System.IO;
using System.Reflection;

namespace Auth.Core.Utils
{

    public class Paths
    {
        public static string ApplicationBasePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    }
    
}