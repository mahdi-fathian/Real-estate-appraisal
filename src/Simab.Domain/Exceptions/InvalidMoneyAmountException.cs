namespace Simab.Domain.Exceptions;

/// <summary>
/// Exception thrown when money amount is invalid
/// </summary>
public class InvalidMoneyAmountException : DomainException
{
    public decimal AttemptedAmount { get; }
    
    public InvalidMoneyAmountException(decimal attemptedAmount)
        : base($"Money amount {attemptedAmount} cannot be negative.")
    {
        AttemptedAmount = attemptedAmount;
    }
}
