using System;

public class CannotThrow : IThrowBehavior
{
	public void Throw()
	{
		Console.WriteLine("I cant throw");
	}
}
