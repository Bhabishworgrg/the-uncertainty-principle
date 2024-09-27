using Godot;

public partial class Gravity : Node
{
	[Export] private CharacterBody2D _body;
	[Export] private float _gravity;
	private Vector2 _velocity;

	public override void _Ready()
	{
		
	}

	public override void _Process(double delta)
	{
		// Set velocity to 0 if on floor
		if (_body.IsOnFloor())
		{
			_body.Velocity = Vector2.Zero;
		}
		else
		{
			_velocity = _body.Velocity;
			_velocity.Y += _gravity * (float)delta;
			_body.Velocity = _velocity;
		}
		_body.MoveAndSlide();
	}
}
