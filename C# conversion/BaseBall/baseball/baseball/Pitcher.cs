using System;

public class Pitcher : Player
{
	public Pitcher()
	{
		catchBehavior = new CatchWithGlove();
		throwBehavior = new Pitch();
		swingBehavior = new CannotSwing(); // Fixed typo
		name = ""; // Assign to inherited field
	}
	public override void Display()
	{
		Console.WriteLine($"I'm a Pitcher named {name}");
	}
}
