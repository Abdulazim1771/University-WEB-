namespace LMS.Exceptions;

public class InvalidPriceException : Exception
{
	public InvalidPriceException() : base()
	{

	}

	public InvalidPriceException(string message) : base(message) 
	{

	}
}
