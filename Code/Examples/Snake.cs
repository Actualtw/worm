using Godot;
using System;

namespace SnakeGame;

public partial class Snake : Sprite2D
{
	[Export]public float Speed;
	public float _idle;
	Vector2 _direction = new Vector2(1, 0);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void Move()
	{
		if (Input.IsKeyPressed(Key.Right) || Input.IsKeyPressed(Key.D))
		{
			_direction = new Vector2(1, 0);
			_idle = Speed;
		}

		if (Input.IsKeyPressed(Key.Left) || Input.IsKeyPressed(Key.A))
		{
			_direction = new Vector2(-1, 0);
			_idle = Speed;
		}

		if (Input.IsKeyPressed(Key.Up) || Input.IsKeyPressed(Key.W))
		{
			_direction = new Vector2(0, -1);
			_idle = Speed;
		}

		if (Input.IsKeyPressed(Key.Down) || Input.IsKeyPressed(Key.S))
		{
			_direction = new Vector2(0, 1);
			_idle = Speed;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		_idle = 0;
		Move();
		GlobalPosition += _direction * (float)delta * _idle;
	}
}
