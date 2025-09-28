using System;

public class CannotSwing : ISwingBehavior
{
	public void Swing()
	{
		Console.WriteLine("I Cannot Swing");
	}
}
