using System.Threading.Tasks;
using System.Net;

namespace ModernInfoTechInventory.ErrorHelper
{
    public interface IApiExceptions
    {
        int ErrorCode { get; set; }
        string ErrorDescription { get; set; }
        HttpStatusCode HttpStatus { get; set; }
        string ReasonPhrase { get; set; }
    }
}
