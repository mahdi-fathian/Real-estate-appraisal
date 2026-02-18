using Simab.Domain.Common;
using Simab.Domain.Exceptions;

namespace Simab.Domain.ValueObjects;

/// <summary>
/// Value object representing money with currency
/// </summary>
public class Money : ValueObject
{
    public decimal Amount { get; init; }
    public string Currency { get; init; }
    
    public Money(decimal amount, string currency = "IRR")
    {
        if (amount < 0)
            throw new InvalidMoneyAmountException(amount);
            
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Currency cannot be null or empty", nameof(currency));
            
        Amount = amount;
        Currency = currency;
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
        yield return Currency;
    }
    
    public Money Add(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot add money with different currencies: {Currency} and {other.Currency}");
            
        return new Money(Amount + other.Amount, Currency);
    }
    
    public Money Subtract(Money other)
    {
        if (Currency != other.Currency)
            throw new InvalidOperationException($"Cannot subtract money with different currencies: {Currency} and {other.Currency}");
            
        return new Money(Amount - other.Amount, Currency);
    }
    
    public Money Multiply(decimal multiplier)
    {
        return new Money(Amount * multiplier, Currency);
    }
}
