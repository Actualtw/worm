using Godot;
using System;

public partial class ProtoMover : Sprite2D
{
    [Export] private int _speed = 1;
    [Export] private Vector2 _direction = new Vector2(0, 0);
    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {

        GlobalPosition += _direction * (float)delta * _speed;
    }
}
