using Economic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIService.ApiServices.Economic
{
    public interface IEconPaymentTermsService
    {
        Task<EconPaymentTerms> GetOrCreateEconPaymentTerms(EconPaymentTerms econPaymentTerm);
        Task<EconPaymentTerms> GetPaymentTerms(string url);
    }
}