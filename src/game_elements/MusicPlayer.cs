using Godot;

public partial class MusicPlayer : AudioStreamPlayer
{
	public override void _Process(double delta)
	{
		if (VolumeDb < -7)
		{
			VolumeDb += 0.27f * (float)delta;
		}
	}
}
