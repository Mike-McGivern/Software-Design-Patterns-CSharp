using System;

public class ThrowToPitcher : IThrowBehavior
{
	public void Throw()
	{
		Console.WriteLine("I throw the ball back to the picther");
	}
}
