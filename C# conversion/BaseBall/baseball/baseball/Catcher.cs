using System;

public class Catcher : Player
{
	public Catcher()
	{
		catchBehavior = new CatchWithMitt();
		throwBehavior = new ThrowToPitcher();
		swingBehavior = new CannotSwing();
		name = "";
	}

	public override void Display()
	{
		Console.WriteLine($"I'm a Catcher named {name}");
	}
}
