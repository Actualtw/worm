using Godot;
using System;

namespace SnakeGame;

public partial class Snake : Sprite2D
{
	[Export]public int Speed;
	private Vector2I _direction = new Vector2I(0, 0);
	private bool _isMoving = false;

	public override void _Ready()
	{
		Position = new Vector2I(96, 96);
	}

	public void Move()
	{

		if (Input.IsActionJustPressed("Right"))
		{
			_direction = new Vector2I(1, 0);
			RotationDegrees = 90;
			_isMoving = true;
		}

		if (Input.IsActionJustPressed("Left"))
		{
			_direction = new Vector2I(-1, 0);
			RotationDegrees = -90;
			_isMoving = true;
		}

		if (Input.IsActionJustPressed("Up"))
		{
			_direction = new Vector2I(0, -1);
			RotationDegrees = 0;
			_isMoving = true;
		}

		if (Input.IsActionJustPressed("Down"))
		{
			_direction = new Vector2I(0, 1);
			RotationDegrees = 180;
			_isMoving = true;
		}
	}

    public override void _Process(double delta)
    {
        Move();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _PhysicsProcess(double delta)
	{
		if (_isMoving)
		{
			Position += new Vector2(_direction.X * Speed, _direction.Y * Speed);
			_isMoving = false;
		}
	}
}
