using System;
using System.ComponentModel;

public class Fielder : Player
{
	public Fielder()
	{
		catchBehavior = new CatchWithGlove();
		throwBehavior = new ThrowToFielder();
		swingBehavior = new CannotSwing();
		string name = "";
	}

	public override void Display()
	{
		Console.WriteLine("I'm a Fielder named {name}");
	}
}
