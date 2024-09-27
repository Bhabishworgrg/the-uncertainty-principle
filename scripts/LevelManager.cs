using Godot;
using System;

public partial class LevelManager : Node
{
	[Export] private CanvasLayer pauseMenu;
	[Export] private CharacterBody2D player;
	[Export] private ProgressBar bar;
	[Export] private Sprite2D cursor;
	[Export] private Camera2D camera;
	[Export] private Timer timer;
	[Export] private Label timeLabel, accuracyLabel;

	private int value;
	private bool increasing;
	private float maxDistance;
	private Vector2 mousePosition, destination;
	private Random random;
	private float angle, radius;
	private float x, y;
	private double time, accuracy;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
			destination = GetCoordinate();
			SetTimer();
			time = timer.WaitTime;
			accuracy = (250 - mousePosition.DistanceTo(destination)) * 2 / 5;
			accuracyLabel.Text = Math.Truncate(accuracy) + "%";
			timeLabel.Text = Math.Truncate(time * 10) + "";
			timer.Start();
		}
		
		if (time > 0)
		{
			time -= delta;
			timeLabel.Text = Math.Truncate(time * 10) + "";
		}
		else
		{
			timeLabel.Text = "0";
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
		double time = 0.05 + (random.NextDouble() * (4 - (value / 25)));		
		timer.WaitTime = time;
	}

	private void OnTimeOut()
	{
		player.Position = destination;
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
