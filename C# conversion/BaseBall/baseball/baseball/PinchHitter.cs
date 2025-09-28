using System;
using System.ComponentModel;

public class PinchHitter : Player
{
	public PinchHitter()
	{
		catchBehavior = new CannotCatch();
		throwBehavior = new CannotThrow();
		swingBehavior = new SwingForPower();
		string name = "";
	}

	public override void Display()
	{
		Console.WriteLine($"I'm a Pinch Hitter named {name}");
	}
}
