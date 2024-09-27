using Godot;
using System;

public partial class LevelManager : Node
{
	[Export] private CanvasLayer _pauseMenu;
	[Export] private CharacterBody2D _player;
	[Export] private ProgressBar _bar;
	[Export] private Sprite2D _cursor;
	[Export] private Camera2D _camera;

	private int _value;
	private bool _increasing;
	private float _maxDistance;
	private Vector2 _mousePosition;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_increasing = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_increasing)
		{
			if (_value == 100)
			{
				_increasing = false;
			}
			else
			{
				_value++;
			}
		}
		else
		{
			if (_value == 0)
			{
				_increasing = true;
			}
			else
			{
				_value--;
			}
		}

		_bar.Value = _value;

		_maxDistance = _value * 2.5f;
		
		_mousePosition = _camera.GetGlobalMousePosition();
		_cursor.Position = GetViewport().GetMousePosition();

		if (Input.IsActionPressed("pause"))
		{
			Pause();
		}

		if (Input.IsActionJustPressed("left_click"))
		{
			_player.Position = GetCoordinate();
		}
	}

    public Vector2 GetCoordinate()
    {

		Random random = new Random();

        // Generate a random angle between 0 and 2π
        float angle = (float)(random.NextDouble() * Math.PI * 2);
        
        // Generate a random radius between 0 and _maxDistance
        float radius = (float)(random.NextDouble() * _maxDistance);
        
        // Convert polar coordinates to Cartesian coordinates
        float x = _mousePosition.X + radius * (float)Math.Cos(angle);
        float y = _mousePosition.Y + radius * (float)Math.Sin(angle);
        
        return new Vector2(x, y);
    }

	private void Pause()
	{
		GetTree().Paused = true;
		_pauseMenu.Show();
	}

	private void Unpause()
	{
		_pauseMenu.Hide();
		GetTree().Paused = false;
	}

	private void Exit()
	{
		GetTree().Quit();
	}
}
