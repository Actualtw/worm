using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace SnakeGame
{
	public partial class Level : Node2D
	{

		private static Level _current = null;
		public static Level Current
		{
			get { return _current; }
		}

		[Export] private string _snakeScenePath = "res://Levels/Snake.tscn";
		[Export] private string _appleScenePath = "res://Levels/Collectables/Apple.tscn";
		[Export] private string _nuclearWasteScenePath = "res://Levels/Collectables/NuclearWaste.tscn";
		private PackedScene _snakeScene = null;
		private PackedScene _appleScene = null;
		private PackedScene _nuclearWasteScene = null;
		private bool _waitingForRestart = false;
		private int _score = 0;
		private Grid _grid = null;
		private Snake _snake = null;
		private Apple _apple = null;
		private NuclearWaste _nuclearWaste = null;

		public int Score
		{
			get { return _score; }
			set { _score = value; }
		}

		public Grid Grid
		{
			get { return _grid; }
		}

		public Snake Snake
		{
			get { return _snake; }
		}

		public Level()
		{
			_current = this;
		}

		public override void _Ready()
		{
			_grid = GetNode<Grid>("Grid");
			if (_grid == null)
			{
				GD.PrintErr("Gridiä ei löytynyt Levelin lapsinodeista!");
			}

			ResetGame();
		}

		public void ResetGame()
		{
			if (_snake != null)
			{
				_snake.QueueFree();
				_snake = null;
			}
				_snake = CreateSnake();

				AddChild(_snake);

				Score = 0;

				ReplaceApple();

				ReplaceNuclearWaste();
		}

		private Snake CreateSnake()
		{
			if (_snakeScene == null)
			{
				_snakeScene = ResourceLoader.Load<PackedScene>(_snakeScenePath);
				if (_snakeScene == null)
				{
					GD.PrintErr("Madon sceneä ei löydy!");
					return null;
				}
			}
			return _snakeScene.Instantiate<Snake>();
		}

		public void ReplaceApple()
		{
			if (_apple != null)
			{
				Grid.ReleaseCell(_apple.GridPosition);
				_apple.QueueFree();
				_apple = null;
			}

			if (_appleScene == null)
			{
				_appleScene = ResourceLoader.Load<PackedScene>(_appleScenePath);
				if (_appleScene == null)
				{
					GD.PrintErr("Can't load apple scene!");
					return;
				}
			}

			_apple = _appleScene.Instantiate<Apple>();
			AddChild(_apple);

			Cell freeCell = Grid.GetRandomFreeCell();
			if (Grid.OccupyCell(_apple, freeCell.GridPosition))
			{
				_apple.SetPosition(freeCell.GridPosition);
			}
		}

		public void ReplaceNuclearWaste()
		{
			if (_nuclearWaste != null)
			{
				Grid.ReleaseCell(_nuclearWaste.GridPosition);
				_nuclearWaste.QueueFree();
				_nuclearWaste = null;
			}

			if (_nuclearWasteScene == null)
			{
				_nuclearWasteScene = ResourceLoader.Load<PackedScene>(_nuclearWasteScenePath);
				if (_nuclearWasteScene == null)
				{
					GD.PrintErr("Can't load nuclear waste scene!");
					return;
				}
			}

			_nuclearWaste = _nuclearWasteScene.Instantiate<NuclearWaste>();
			AddChild(_nuclearWaste);

			Cell freeCell = Grid.GetRandomFreeCell();
			if (Grid.OccupyCell(_nuclearWaste, freeCell.GridPosition))
			{
				_nuclearWaste.SetPosition(freeCell.GridPosition);
			}
		}

		public void CheckForCollectables()
		{
			if (_nuclearWaste.GridPosition == _snake.GridPosition)
			{
				_nuclearWaste.Collect();
				_nuclearWaste = null;
			}
		}

		public void KillSnake()
		{
			_snake.QueueFree();
			_snake = null;
			WaitForContinue();
		}

		public void WaitForContinue()
		{
			_waitingForRestart = true;
		}

		public override void _Input(InputEvent @event)
		{
			if (!_waitingForRestart) return; // Jos ei odoteta restartia, ei tehdä mitään.

			if (@event is InputEventKey { Pressed: true, Keycode: Key.Space })
			{
				_waitingForRestart = false;
				ResetGame();
			}
		}
	}
}
