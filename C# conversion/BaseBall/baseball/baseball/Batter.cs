using System;

public class Batter : Player
{
	public Batter()
	{
		catchBehavior = new CatchWithHand();
		throwBehavior = new CannotThrow();
		swingBehavior = new SwingForContact();
		name = "";
	}

	public override void Display()
	{
		Console.WriteLine($"I'm a Batter named {name}");
	}
}
