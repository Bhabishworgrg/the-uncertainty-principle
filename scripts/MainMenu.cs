using Godot;

public partial class MainMenu : CanvasLayer
{
	private void Start()
	{
		GetTree().ChangeSceneToFile("res://scenes/base_level.tscn");
	}

	private void Exit()
	{
		GetTree().Quit();
	}
}
