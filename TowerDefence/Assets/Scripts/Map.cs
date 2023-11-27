using System;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

public class Map : MonoBehaviour
{
	public static Map instance;

	public struct Node
	{
		public Coord coord;
		public int layer;
		public MapNode mapNode;


		public Node(Coord coord, int layer, MapNode mapNode)
		{
			this.coord = coord;
			this.layer = layer;
			this.mapNode = mapNode;
		}
	}
	public Node[,] nodes;

	[SerializeField] private Vector2 _origin;
	[SerializeField] private Coord _length;
	[SerializeField] private LayerMask _nodeMask;
	private Grid _grid;

	public void Register(MapNode mapNode)
	{
		Coord coord = VectorToCoord(mapNode.transform.position);
		nodes[coord.y, coord.x] = new Node(coord, mapNode.gameObject.layer, mapNode);
		Debug.Log($"Registered {coord.x}, {coord.y} !");
	}

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
		float x = (rel.x / (_grid.cellSize.x)) + (rel.y / (_grid.cellSize.y));
		float y = -(rel.x / (_grid.cellSize.x)) + (rel.y / (_grid.cellSize.y));
		return new Coord(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
	}

	private void Awake()
	{
		instance = this;
		nodes = new Node[_length.y, _length.x];
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


	[MenuItem("Map/CreateMapNodes")]
	static void CreateMapNodes()
	{
		Map map = GameObject.FindObjectOfType<Map>();
		map._grid = map.GetComponent<Grid>();
		map?.ArrangeMapNodes();
	}
}
