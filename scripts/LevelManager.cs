using Godot;
using System;

public partial class LevelManager : Node
{
	[Export] private CanvasLayer pauseMenu;
	[Export] private CharacterBody2D player;
	[Export] private ProgressBar bar;
	[Export] private Sprite2D cursor;
	[Export] private Camera2D camera;
	[Export] private Timer teleportTimer, deathTimer;
	[Export] private Label timeLabel, accuracyLabel;
	[Export] private GridContainer valuesContainer;

	private int value;
	private bool increasing;
	private float maxDistance;
	private Vector2 mousePosition, destination;
	private Random random;
	private float angle, radius;
	private float x, y;
	private double time, accuracy;
	private GameManager gameManager;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameManager = GameManager.Instance;
		random = new Random(Guid.NewGuid().GetHashCode());
		increasing = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (increasing)
		{
			if (value == 100)
			{
				increasing = false;
			}
			else
			{
				value++;
			}
		}
		else
		{
			if (value == 0)
			{
				increasing = true;
			}
			else
			{
				value--;
			}
		}

		bar.Value = value;

		mousePosition = camera.GetGlobalMousePosition();
		cursor.Position = GetViewport().GetMousePosition();

		if (Input.IsActionPressed("pause"))
		{
			Pause();
		}

		if (Input.IsActionJustPressed("left_click"))
		{
			valuesContainer.Modulate = new Color(0xffffffff);
			destination = GetCoordinate();
			SetTimer();
			time = teleportTimer.WaitTime;
			accuracy = (250 - mousePosition.DistanceTo(destination)) * 2 / 5;
			accuracyLabel.Text = Math.Truncate(accuracy) + "%";
			timeLabel.Text = Math.Truncate(time * 10) + "";
			teleportTimer.Start();
		}
		
		if (time > 0)
		{
			time -= delta;
			timeLabel.Text = Math.Truncate(time * 10) + "";
		}
	}

    private Vector2 GetCoordinate()
    {
		maxDistance = value * 2.5f;
        
		// Generate a random angle between 0 and 2π
        angle = (float)(random.NextDouble() * Math.PI * 2);
        
        // Generate a random radius between 0 and maxDistance
        radius = (float)(random.NextDouble() * maxDistance);
        
        // Convert polar coordinates to Cartesian coordinates
        x = mousePosition.X + radius * (float)Math.Cos(angle);
        y = mousePosition.Y + radius * (float)Math.Sin(angle);
        
        return new Vector2(x, y);
    }
    
	private void SetTimer()
	{
		teleportTimer.WaitTime = 0.05 + (random.NextDouble() * (4 - (value / 25)));
	}

	private void OnTeleportTimeOut()
	{
		player.Position = destination;
		valuesContainer.Modulate = new Color(0xffffff32);
		accuracyLabel.Text = "0";
		timeLabel.Text = "0";
	}
	
	private void OnPlatformCollision(Node2D node)
	{
		Die();
	}

	private void OnAreaCollision(Area2D area)
	{
		if (area.IsInGroup("DEATH"))
		{
			Die();
		}
	}

	private void Die()
	{
		gameManager.isGravityEnabled = false;
		player.RotationDegrees = (float)(random.NextDouble() * 360);
		deathTimer.Start();
	}

	private void OnDeathTimeOut()
	{
		player.Rotation = 0;
		player.Position = gameManager.CurrentCheckpoint.GlobalPosition;
		gameManager.isGravityEnabled = true;
	}

	private void Pause()
	{
		GetTree().Paused = true;
		pauseMenu.Show();
	}

	private void Unpause()
	{
		pauseMenu.Hide();
		GetTree().Paused = false;
	}

	private void Exit()
	{
		GetTree().Quit();
	}
}
