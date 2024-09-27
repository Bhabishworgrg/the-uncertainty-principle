using Godot;

public partial class LevelManager : Node
{
	[Export] private CanvasLayer _pauseMenu;
	[Export] private CharacterBody2D _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("pause"))
		{
			Pause();
		}

		if (Input.IsActionJustPressed("left_click"))
		{
			_player.Position = GetViewport().GetMousePosition();
		}
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
