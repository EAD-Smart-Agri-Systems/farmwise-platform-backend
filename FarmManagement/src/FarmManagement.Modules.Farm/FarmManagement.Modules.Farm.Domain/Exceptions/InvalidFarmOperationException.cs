namespace FarmManagement.Modules.Farm.Domain.Exceptions;

public class InvalidFarmOperationException : Exception
{
    public InvalidFarmOperationException(string message) : base(message) { }
}
