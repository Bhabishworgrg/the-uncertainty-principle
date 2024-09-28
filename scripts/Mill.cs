using Godot;

public partial class Mill : StaticBody2D
{
	public override void _Process(double delta)
	{
		Rotation += (float)delta;
	}
}
