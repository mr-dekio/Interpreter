using System;
using System.Collections.Generic;
using System.Collections;

namespace Interpreter
{
	public class Point
	{
		public int Row { set; get; }
		public int Col { set; get; }

		public Point()
		{
		}

		public Point(int row, int col)
		{
			Row = row;
			Col = col;
		}

		public static bool AreEquals(Point a, Point b)
		{
			return a.Row == b.Row && a.Col == b.Col;
		}

		public Point Copy()
		{
			return new Point (Row, Col);
		}
	}

	public enum Direction
	{
		Top,
		Down,
		Left,
		Right
	}

	public class Player
	{
		public Point position { set; get; }
		public Direction direction { set; get; }
	}

	public struct Cell
	{
		public static char Empty = '-';
		public static char Player = 'x';
		public static char Direction = '*';
		public static char Block = '#';
		public static char DirectionOnBlock = '@';
		public static char Destination = 'O';
	}

	public interface IPlayerNotifications
	{
		void PlayerMoved(Player player, GameMatrix matrix);
		void TurnedLeft	(Player player, GameMatrix matrix);
		void TurnedRight(Player player, GameMatrix matrix);
		void Lighted	(Player player, GameMatrix matrix);
	}

	public interface IPlayerAction
	{
		void Move	  (Player player);
		void TurnLeft (Player player);
		void TurnRight(Player player);
		void Light 	  (Player player);
	}

	public class GameMatrix : IPlayerAction
	{
		private char [,] matrix;
		private int rows;
		private int cols;

		public IPlayerNotifications NotificationDelegate { set; get; }

		private ArrayList players = new ArrayList();
		private Point destination = new Point();

		public GameMatrix () : this (10, 10)
		{
		}

		public GameMatrix(int rows, int cols)
		{
			this.rows = rows;
			this.cols = cols;
		}

		public void AddPlayer(Player player)
		{
			players.Add (player);
		}

		public void PrepareMatrix()
		{
			CreateField ();

			PreparePlayersPositions ();

			int cellsInMatrix = rows * cols;
			AddBlocks (0);//(cellsInMatrix / 5);
		}

		public void AddBlocks(int count)
		{
			while (count > 0) {
				Point block = new Point (new Random ().Next (rows), new Random ().Next (cols));
				if (IsCellNotUsed (block)) {
					matrix [block.Row, block.Col] = Cell.Block;
					count--;
				}
			}
		}

		public char [,] GetMatrix()
		{
			return matrix;
		}

		public ArrayList GetPlayers()
		{
			return players;
		}

		public Point getDestination()
		{
			return destination;
		}

		public bool IsPointFree(Point point)
		{
			if (point.Row >= 0 && point.Row < rows && point.Col >= 0 && point.Col < cols) {
				char cell = matrix [point.Row, point.Col];
				return cell != Cell.Block;
			}
			return false;
		}

		// IPlayerActions

		public void Move(Player player)
		{
			if (PlayerExists (player) == false)
				return;

			Point nextPoint = player.position.Copy();
			Point offset = new Point ();

			switch (player.direction) {
			case Direction.Top:
				offset.Row = -1;
				break;

			case Direction.Down:
				offset.Row = 1;
				break;

			case Direction.Left:
				offset.Col = -1;
				break;

			case Direction.Right:
				offset.Col = 1;
				break;
			}

			nextPoint.Row += offset.Row;
			nextPoint.Col += offset.Col;

			if (IsPointFree (nextPoint)) {
				Point previously = player.position;
				player.position = nextPoint;

				UpdatePlayerPosition (player, previously);

				if (NotificationDelegate != null)
					NotificationDelegate.PlayerMoved (player, this);
			}

		}

		public void TurnLeft(Player player)
		{
			if (PlayerExists (player) == false)
				return;

			Direction turn = new Direction();

			switch (player.direction) {
			case Direction.Top:
				turn = Direction.Left;
				break;
			case Direction.Left:
				turn = Direction.Down;
				break;
			case Direction.Down:
				turn = Direction.Right;
				break;
			case Direction.Right:
				turn = Direction.Top;
				break;
			}

			player.direction = turn;

			if (NotificationDelegate != null) {
				NotificationDelegate.TurnedLeft (player, this);
			}
		}

		public void TurnRight(Player player)
		{
			if (PlayerExists (player) == false)
				return;

			Direction turn = new Direction ();

			switch (player.direction) {
			case Direction.Top:
				turn = Direction.Right;
				break;
			case Direction.Right:
				turn = Direction.Down;
				break;
			case Direction.Down:
				turn = Direction.Left;
				break;
			case Direction.Left:
				turn = Direction.Top;
				break;
			}

			player.direction = turn;

			if (NotificationDelegate != null) {
				NotificationDelegate.TurnedRight (player, this);
			}
		}

		public void Light (Player player)
		{
			if (PlayerExists (player) == false)
				return;

			if (Point.AreEquals (player.position, destination)) {
				if (NotificationDelegate != null)
					NotificationDelegate.Lighted (player, this);
			} else {
				// error lighting
			}
		}

		// private

		private void PreparePlayersPositions()
		{
			foreach (Player player in players) // possible error if 2 robots stay on the same cell
				UpdatePlayerPosition (player);

			do {
				destination = new Point (new Random ().Next (rows), new Random ().Next (cols));
			} while (IsCellNotUsed (destination) == false);

			matrix [destination.Row, destination.Col] = Cell.Destination;
		}

		private bool PlayerExists(Player player)
		{
			return players.Contains (player);
		}

		private bool IsCellNotUsed(Point point)
		{
			return matrix [point.Row, point.Col] == '-';
		}
			
		private void CreateField()
		{
			matrix = new char [rows, cols];
			for (int i = 0; i < rows; i++) 
			{
				for (int j = 0; j < cols; j++)
					matrix [i, j] = Cell.Empty;
			}
		}

		private void UpdatePlayerPosition(Player player, Point previously = null)
		{
			if (previously != null)
				matrix [previously.Row, previously.Col] = Cell.Empty;

			matrix [player.position.Row, player.position.Col] = Cell.Player;
		}

	}
		
	public class Context
	{
		private Dictionary<string, int> variables = new Dictionary<string, int>();

		private GameMatrix matrix;
		private Player player;

		public Context ()
		{
		}

		public Context (GameMatrix gameMatrix)
		{
			matrix = gameMatrix;
		}

		public void SetMatrix(GameMatrix gameMatrix)
		{
			matrix = gameMatrix;
		}

		public int GetValue(string varName)
		{
			return variables [varName];
		}

		public void SetValue(string varName, int value)
		{
			variables [varName] = value;
		}

		public bool hasVariable(string name)
		{
			int value = int.MinValue;
			return variables.TryGetValue (name, out value) == true;
		}

		public void PrepareContext()
		{
			player = new Player ();
			player.position = new Point (0, 0);
			player.direction = Direction.Down;

			matrix.AddPlayer (player);
			matrix.PrepareMatrix ();
		}

		public void Move()
		{
			matrix.Move (player);
		}

		public void Left()
		{
			matrix.TurnLeft (player);
		}
			
		public void Right()
		{
			matrix.TurnRight (player);
		}

		public void Light()
		{
			matrix.Light (player);
		}
	}
}

