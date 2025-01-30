using Godot;
using System;
using System.Drawing;

public partial class ProtoMover : Sprite2D
{
    [Export] Boolean MoveBetweenPoints;
    [Export] private int _speed;
    [Export] private Vector2 _direction;
    [Export] private Vector2 StartPosition;
    [Export] private Vector2 TargetPosition;

    private Vector2 TempTarget;
    public override void _Ready()
    {
        Position = StartPosition;
        TempTarget = TargetPosition;
    }

    public override void _Process(double delta)
    {
        if (MoveBetweenPoints)
        {
            var directionToTarget = (TempTarget - Position).Normalized();
            Position += directionToTarget * (float)delta * _speed;

            if (Position.DistanceTo(TempTarget) < 1)
            {
                TempTarget = TempTarget == TargetPosition ? StartPosition : TargetPosition;
            }
        }
            GlobalPosition += _direction * (float)delta * _speed;
    }
}
