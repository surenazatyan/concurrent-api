namespace paymentAPI.Core.Types;

public readonly record struct IBAN(string Value)
{
    public static implicit operator IBAN(string value) => new IBAN(value);
    public static implicit operator string(IBAN iban) => iban.Value;

    public override string ToString() => Value;
}
