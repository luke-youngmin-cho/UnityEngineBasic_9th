using System;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[Serializable]
public struct Coord
{
	public static Coord up => new Coord(0, 1);
	public static Coord down => new Coord(0, -1);
	public static Coord right => new Coord(1, 0);
	public static Coord left => new Coord(-1, 0);

	public int x, y;

	public Coord (int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public static bool operator ==(Coord op1, Coord op2) 
		=> (op1.x == op2.x) && (op1.y == op2.y);

	public static bool operator !=(Coord op1, Coord op2)
		=> !(op1 == op2);

	public static Coord operator +(Coord op1, Coord op2)
		=> new Coord(op1.x + op2.x, op1.y + op2.y);

	public static Coord operator -(Coord op1, Coord op2)
		=> new Coord(op1.x - op2.x, op1.y - op2.y);
}


public class Map : MonoBehaviour
{
	public struct Node
	{
		public Coord coord;
		public int layer;
		public int itemID;

		public Node(Coord coord, int layer, int itemID = 0)
		{
			this.coord = coord;
			this.layer = layer;
			this.itemID = itemID;
		}
	}
	public Node[,] nodes;

	[SerializeField] private Vector2 _origin;
	[SerializeField] private Coord _length;
	[SerializeField] private LayerMask _nodeMask;
	private Grid _grid;

	public Node GetNode(Vector2 point)
	{
		Coord coord = VectorToCoord(point);
		return nodes[coord.y, coord.x];
	}

	public Vector2 CoordToVector(Coord coord)
	{
		return _origin +
			   coord.x * Vector2.right * _grid.cellSize.x / 2.0f +
			   coord.x * Vector2.up * _grid.cellSize.y / 2.0f +
			   coord.y * Vector2.left * _grid.cellSize.x / 2.0f +
			   coord.y * Vector2.up * _grid.cellSize.y / 2.0f;
	}

	public Coord VectorToCoord(Vector2 point)
	{
		Vector2 rel = point - _origin;
		rel =
			new Vector2(rel.x / (+_grid.cellSize.x / 2.0f + _grid.cellSize.y / 2.0f),
						rel.y / (-_grid.cellSize.x / 2.0f + _grid.cellSize.y / 2.0f));
		return new Coord(Mathf.RoundToInt(rel.x), Mathf.RoundToInt(rel.y));
	}

	private void Awake()
	{
		_grid = GetComponent<Grid>();
	}

	private void ArrangeMapNodes()
	{
		for (int i = 0; i < _length.y; i++)
		{
			for (int j = 0; j < _length.x; j++)
			{
				Coord coord = new Coord(j, i);
				Vector2 point = CoordToVector(coord);

				Collider2D col =
					Physics2D.OverlapPoint(point, _nodeMask);

				MapNode node = new GameObject("Node").AddComponent<MapNode>();
				node.transform.position = point;
			}
		}
	}

	private void SetUp()
	{
		for (int i = 0; i < _length.y; i++)
		{
			for (int j = 0; j < _length.x; j++)
			{
				Coord coord = new Coord(j, i);
				Vector2 point = CoordToVector(coord);

				Collider2D col =
					Physics2D.OverlapPoint(point, _nodeMask);

				int layer = col ? col.gameObject.layer : 0;
				nodes[i, j] = new Node(coord, layer);
			}
		}
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(20, 40, 80, 20), "Arrange Map Nodes"))
		{
			ArrangeMapNodes();
		}
	}
}
