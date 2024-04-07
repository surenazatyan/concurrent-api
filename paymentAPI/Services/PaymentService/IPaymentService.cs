using paymentAPI.Core.Types;
using paymentAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace paymentAPI.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<string> AddTransaction(string clientId, AddTransactionRequestModel transaction);
        List<GetTransactionResponseModel> GetTransactions(string iban);
    }
}