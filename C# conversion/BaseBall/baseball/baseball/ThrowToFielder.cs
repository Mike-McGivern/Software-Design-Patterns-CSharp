using System;

public class ThrowToFielder : IThrowBehavior
{
	public void Throw()
	{
		Console.WriteLine("I throw to another fielder");
	}
}
