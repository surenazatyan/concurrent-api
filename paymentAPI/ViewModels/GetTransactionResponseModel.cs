using paymentAPI.Core.Types;

namespace paymentAPI.ViewModels;

public readonly record struct GetTransactionResponseModel(string paymentId,IBAN DebtorAccount, IBAN CreditorAccount, decimal TransactionAmount, string Currency);
