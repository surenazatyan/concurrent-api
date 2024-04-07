using paymentAPI.Repositories.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using paymentAPI.ViewModels;
using paymentAPI.Core.Types;
using paymentAPI.Repositories;
using System.Transactions;
using paymentAPI.Core;
using System.Linq;

namespace paymentAPI.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        PaymentRepository paymentRepository;
        ILogger logger;
        public PaymentService(ILogger<PaymentService> logger, PaymentRepository paymentRepository)
        {
            this.logger = logger;
            this.paymentRepository = paymentRepository;
        }

        public async Task<string> AddTransaction(string clientId, AddTransactionRequestModel transactioVM)
        {
            DecimalParser.TryParseInvariant(transactioVM.InstructedAmount, out var InstructedAmountDecimal);

            var transactionDto = new TransactionDTO
            {
                DebtorAccount = transactioVM.DebtorAccount,
                CreditorAccount = transactioVM.CreditorAccount,
                InstructedAmount = InstructedAmountDecimal,
                Currency = transactioVM.Currency
            };

            var succeess = await paymentRepository.InitiatePaymentAsync(clientId, transactionDto);

            return succeess;

        }

        public List<GetTransactionResponseModel> GetTransactions(string iban)
        {
            var transactionsDto = paymentRepository.GetTransactions(iban);

            var transactionsResponse = transactionsDto
                ?.Select(dto => new GetTransactionResponseModel(
                    dto.paymentId,
                    dto.DebtorAccount,
                    dto.CreditorAccount,
                    dto.InstructedAmount,
                    dto.Currency))
                .ToList();

            return transactionsResponse;
        }
    }
}
