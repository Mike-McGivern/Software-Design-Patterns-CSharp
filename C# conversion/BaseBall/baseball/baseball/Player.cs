using System;

public abstract class Player 
{
	protected ISwingBehavior swingBehavior;
	protected ICatchBehavior catchBehavior;
	protected IThrowBehavior throwBehavior;
	protected string name = "";
	public Player()
	{

	}
	public abstract void Display();

	public void PerformBat()
	{
		swingBehavior.Swing();
	}

	public void PerformCatch()
	{
		catchBehavior.Catch();
	}
	public void PerformThrow() 
	{
		throwBehavior.Throw();
	}

	public void SetName(string playerName)
	{
		name = playerName;
	}

	public void SetThrowBehavior(IThrowBehavior tb)
	{
		throwBehavior = tb;
	}
	public void setCatchBehavior(ICatchBehavior cb)
	{
		catchBehavior = cb;
	}
	public void setBatBehavior(ISwingBehavior sb)
	{
		swingBehavior = sb;
	}

}
