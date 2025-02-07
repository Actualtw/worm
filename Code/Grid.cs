using System.Collections.Generic;
using Godot;

namespace SnakeGame
{
	public partial class Grid : Node2D
	{
		[Export] private string _cellScenePath = "res://Levels/Cell.tscn";
		[Export] private int _width = 15;
		[Export] private int _height = 13;
		[Export] private Vector2I _cellSize = new Vector2I(16, 16);

		public int Width => _width;
		public int Height => _height;

		private Cell[,] _cells = null;

		// Ne solut, joissa ei ole mitään olioita.
		private List<Cell> _freeCells = new List<Cell>();

		public override void _Ready()
		{
			// Alusta _cells taulukko
			_cells = new Cell[_width, _height];

			// Laske se piste, josta taulukon rakentaminen aloitetaan. Koska 1. solu luodaan gridin vasempaan
			// yläkulmaan, on meidän laskettava sitä koordinaattia vastaava piste. Oletetaan Gridin pivot-pisteen
			// olevan kameran keskellä (https://en.wikipedia.org/wiki/Pivot_point).
			Vector2 offset = new Vector2((_width * _cellSize.X) / 2, (_height * _cellSize.Y) / 2);

			Vector2 halfNode = new Vector2(_cellSize.X / 2f, _cellSize.Y / 2f);

			if (Width % 2 != 0)
			{
				offset.X -= halfNode.X;
			}

			if (Height % 2 != 0)
			{
				offset.Y -= halfNode.Y;
			} 

			// Lataa Cell-scene. Luomme tästä uuden olion kutakin ruutua kohden.
			PackedScene cellScene = ResourceLoader.Load<PackedScene>(_cellScenePath);
			if (cellScene == null)
			{
				GD.PrintErr("Cell sceneä ei löydy! Gridiä ei voi luoda!");
				return;
			}

			// Alustetaan Grid kahdella sisäkkäisellä for-silmukalla.
			for (int x = 0; x < _width; ++x)
			{
				for (int y = 0; y < _height; ++y)
				{
					// Luo uusi olio Cell-scenestä.
					Cell cell = cellScene.Instantiate<Cell>();
					// Lisää juuri luotu Cell-olio gridin Nodepuuhun.
					AddChild(cell);

					// Laske ja aseta ruudun sijainti niin maailman koordinaatistossa kuin
					// ruudukonkin koordinaatistossa. Aseta ruudun sijainti käyttäen cell.Position propertyä.
					Vector2 worldPosition = new Vector2(x * _cellSize.X, y * _cellSize.Y) - offset;
					cell.Position = worldPosition;
					cell.GridPosition = new Vector2I(x, y);

					// Tallenna ruutu tietorakenteeseen oikealle paikalle.
					_cells[x, y] = cell;
					_freeCells.Add(cell);
				}
			}
		}

		public bool CheckGridPosition(Vector2I gridPosition)
		{
			if (gridPosition.X >= 0 && gridPosition.X < Width && gridPosition.Y >= 0 && gridPosition.Y < Height)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool GetWorldPosition(Vector2I gridPosition, out Vector2 worldPosition)
		{
			if (CheckGridPosition(gridPosition))
			{
				worldPosition = _cells[gridPosition.X, gridPosition.Y].Position;
				return true;
			}
			worldPosition = Vector2I.Zero;
			return false;
		}

		public bool OccupyCell(ICellOccupier occupier, Vector2I gridPosition)
		{
			if (CheckGridPosition(gridPosition))
			{
				Cell cell = _cells[gridPosition.X, gridPosition.Y];
				bool canOccupy = cell.Occupy(occupier);
				if (canOccupy)
				{
					_freeCells.Remove(cell);
					return canOccupy;
				}
			}

			return false;
		}

		public bool ReleaseCell(Vector2I gridPosition)
		{
			if (!CheckGridPosition(gridPosition))
			{
				return false;
			}

			Cell cell = _cells[gridPosition.X, gridPosition.Y];
			cell.Release();
			_freeCells.Add(cell);

			return true;
		}

		public Cell GetRandomFreeCell()
		{
			int randomIndex = GD.RandRange(0, _freeCells.Count - 1);
			return _freeCells[randomIndex];
		}
	}
}
