using paymentAPI.Core.Types;

namespace paymentAPI.Repositories.Models;

public record struct TransactionDTO(string paymentId, IBAN DebtorAccount, IBAN CreditorAccount, decimal InstructedAmount, string Currency);
