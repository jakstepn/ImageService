namespace ImageService.Exceptions;

public class InvalidAmountException : Exception
{
    public string Description { get; }
    public int Amount { get; }
    
    public InvalidAmountException(string description, int amount)
    {
        Description = description;
        Amount = amount;
    }
}