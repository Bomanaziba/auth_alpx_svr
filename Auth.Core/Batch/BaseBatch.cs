


using System.Threading.Tasks;
using Auth.Core.Contract;

namespace Auth.Core.Batch
{
    public interface BaseBatch
    {
        Task<ResponseBaseObject> ExecuteBatch();
    }
}