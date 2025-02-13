using Godot;
using SnakeGame;
using System;

public partial class NuclearWaste : Collectable
{
    public override void Collect()
    {
        Level.Current.KillSnake();
        QueueFree();
    }
}
