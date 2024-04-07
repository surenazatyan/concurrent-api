using paymentAPI.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace paymentAPI.ViewModels;

public readonly record struct AddTransactionRequestModel(
    [property: Required]
    [property: StringLength(34, ErrorMessage = "Incorrect value for IBAN, should be 34 alphanumeric characters.")]
    string DebtorAccount,

    [property: Required]
    [property: StringLength(34, ErrorMessage = "Incorrect value for IBAN, should be 34 alphanumeric characters.")]
    string CreditorAccount,

    [property: Required]
    [property: RegularExpression(@"^-?[0-9]{1,14}(\.[0-9]{1,3})?$", ErrorMessage = "InstructedAmount has incorrect decimal format, should be 1-14 digital points and up to 3 decimal points.")]
    string InstructedAmount,

    [property: Required]
    [property: StringLength(3)]
    string Currency);
