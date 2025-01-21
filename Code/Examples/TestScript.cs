using Godot;
using System;

namespace SnakeGame;
public partial class TestScript : Node
{
	private int _FiboSequence = 0;
	private int _FiboNum1 = 0;
	private int _FiboNum2 = 1;
	private int _FrameCount = 2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game started");
		GD.Print($"Frame 1: {_FiboSequence}");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_FiboSequence = _FiboNum1 + _FiboNum2;
		_FiboNum1 = _FiboNum2;
		_FiboNum2 = _FiboSequence;
		GD.Print($"Frame {_FrameCount}: {_FiboSequence}");
		_FrameCount++;
	}
}
