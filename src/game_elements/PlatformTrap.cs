using Godot;

public partial class PlatformTrap : Node2D
{
	[Export] private Timer timer;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	private void OnStepping(Node2D body)
	{
		timer.Start();
	}

	private void OnTimeOut()
	{
		foreach (Node child in GetChildren())
		{
			if (child is RigidBody2D)
			{
				RigidBody2D body = (RigidBody2D) child;
				body.GravityScale = 1;
				body.SetCollisionMaskValue(2, true);
			}
		}
	}
}
