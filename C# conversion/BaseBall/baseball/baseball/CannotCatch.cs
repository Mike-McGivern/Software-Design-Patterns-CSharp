using System;

public class CannotCatch : ICatchBehavior
{
	public void Catch()
	{
		Console.WriteLine("I can't Catch");
	}
}
