using Godot;
using System;

namespace SnakeGame;
public partial class TestScript : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print($"Paikallinen sijainti: {Position}");
		GD.Print($"Globaali sijainti: {GlobalPosition}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
