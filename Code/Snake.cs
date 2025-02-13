using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

namespace SnakeGame;

public partial class Snake : Node2D, ICellOccupier
{
	[Export]public int Speed = 1;
	private bool _isMoving = false;
	private Vector2I _currentPosition = new Vector2I(5, 5);
	public Vector2I GridPosition
	{
		get { return _currentPosition; }
	}
    public CellOccupierType Type
	{
		get { return CellOccupierType.Snake; }
	}
    public enum Direction
	{
		None = 0,
		Up,
		Down,
		Right,
		Left
	}

	public override void _Ready()
	{
		if (Level.Current.Grid.GetWorldPosition(_currentPosition, out Vector2 worldPosition))
		{
			Position = worldPosition;
		}
	}

	private Vector2I GetNextGridPosition(Direction direction, Vector2I currentPosition)
	{
		switch(direction)
		{
			case Direction.Up: return currentPosition + Vector2I.Up;
			case Direction.Down: return currentPosition + Vector2I.Down;
			case Direction.Right: return currentPosition + Vector2I.Right;
			case Direction.Left: return currentPosition + Vector2I.Left;
			default: return currentPosition;
		}
	}

	private Vector2 GetDirectionVector(Direction direction)
	{
		switch(direction)
		{
			case Direction.Up: return Vector2.Up;
			case Direction.Down: return Vector2.Down;
			case Direction.Right: return Vector2.Right;
			case Direction.Left: return Vector2.Left;
			default: return Vector2.Zero;
		}
	}

	private float GetRotation(Direction movementDirection)
	{
		switch (movementDirection)
		{
			case Direction.Up: return 0;
			case Direction.Down: return 180;
			case Direction.Right: return 90;
			case Direction.Left: return -90;
			default: return 0;
		}
	}

	private void Move(Direction direction)
	{
		Vector2I nextPosition = GetNextGridPosition(direction, _currentPosition);
		if (Level.Current.Grid.GetWorldPosition(nextPosition, out Vector2 worldPosition))
		{
			_currentPosition = nextPosition;
			Position = worldPosition;
			RotationDegrees = GetRotation(direction);
		}
	}

	private Direction ReadInput()
	{
		Direction direction = Direction.None;

		if (Input.IsActionJustPressed(Config.MoveUpAction))
		{
			direction = Direction.Up;
		}

		if (Input.IsActionJustPressed(Config.MoveDownAction))
		{
			direction = Direction.Down;
		}

		if (Input.IsActionJustPressed(Config.MoveRightAction))
		{
			direction = Direction.Right;
		}

		if (Input.IsActionJustPressed(Config.MoveLeftAction))
		{
			direction = Direction.Left;
		}
		return direction;
	}

    public override void _Process(double delta)
    {
		Direction direction = ReadInput();
		if (direction != Direction.None)
        {
			Move(direction);
		}

		Level.Current.CheckForCollectables();
    }
}
