using Godot;

public partial class Checkpoint : Area2D
{
	private bool isActivate = false;

	private void OnCheckpointReached(Node2D node)
	{
		if (!isActivate)
		{
			Activate();
		}
	}

	public void Activate()
	{
		isActivate = true;
		GameManager.Instance.CurrentCheckpoint = this;
	}
}
