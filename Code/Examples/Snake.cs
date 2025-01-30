using Godot;
using System;

namespace SnakeGame;

public partial class Snake : Sprite2D
{
	public int _speed = 50;
	Vector2 _direction = new Vector2(1, 0);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void ReadInput()
	{
		if (Input.IsKeyPressed(Key.Right) || Input.IsKeyPressed(Key.D))
		{
			GD.Print("Suuntatoiminto oikea suoritettu");
			_direction = new Vector2(1, 0);
		}

		if (Input.IsKeyPressed(Key.Left) || Input.IsKeyPressed(Key.A))
		{
			GD.Print("Suuntatoiminto vasen suoritettu");
			_direction = new Vector2(-1, 0);
		}

		if (Input.IsKeyPressed(Key.Up) || Input.IsKeyPressed(Key.W))
		{
			GD.Print("Suuntatoiminto ylös suoritettu");
			_direction = new Vector2(0, -1);
		}

		if (Input.IsKeyPressed(Key.Down) || Input.IsKeyPressed(Key.S))
		{
			GD.Print("Suuntatoiminto alas suoritettu");
			_direction = new Vector2(0, 1);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		ReadInput();
		GlobalPosition += _direction * (float)delta * _speed;
	}
}
