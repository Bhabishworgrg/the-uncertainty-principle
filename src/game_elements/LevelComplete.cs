using Godot;

public partial class LevelComplete : Area2D
{
	[Export] private CanvasLayer winScreen;

	private void OnLevelCompleted(Node2D body)
	{
		GD.Print("WIN");
		GetTree().Paused = true;
		winScreen.Show();
	}
}
