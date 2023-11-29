using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
	public enum Option
	{
		DFS,
		BFS,
	}
	public Option option;
	public Vector2 destination;
	public List<Vector2> path;
	private LineRenderer _lineRenderer;
	[SerializeField] private float _drawLineSpeed = 1.0f;
	[SerializeField] private Vector3 _drawLineOffset = Vector3.back;

	private void Start()
	{
		_lineRenderer = GetComponent<LineRenderer>();
		TryGetPath();
	}

	public bool TryGetPath()
	{
		bool result = false;
		switch (option)
		{
			case Option.DFS:
				result = TryGetDFSPath(out path);
				break;
			case Option.BFS:
				result = TryGetBFSPath(out path);	
				break;
			default:
				break;
		}

		if (result)
			StartCoroutine(C_DrawPathLine());

		return result;
	}


	public bool TryGetDFSPath(out List<Vector2> path)
	{
		Map.Node[,] map = Map.instance.nodes;
		bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)]; // �湮 Ȯ�ο� (������ ��δ� �ٽ� Ž���ϸ� �ȵǴϱ�)
		Stack<Coord> stack = new Stack<Coord>(); // ���� Ž�� ��� ����
		List<Pair> pairs = new List<Pair>(); // ��� ������������ ��ν�
		Coord start = Map.instance.VectorToCoord(transform.position);
		Coord end = Map.instance.VectorToCoord(destination);
		stack.Push(start);
		int[,] dir = new int[2, 4]
		{
			// �� �� �� ��
			{  0,  0,  1, -1 }, // y
			{  1, -1,  0,  0 }  // x
		};

		while (stack.Count > 0)
		{
			Coord current = stack.Pop(); // ���� �� ������ ������ Ž��
			visited[current.y, current.x] = true; // �湮 �Ϸ�

			// Ž���Ϸ�
			if (current == end)
			{
				path = BacktrackPath(start, end, pairs);
				return true;
			}
			else
			{
				for (int i = dir.GetLength(1) - 1; i >= 0; i--)
				{
					Coord next = current + new Coord(dir[1, i], dir[0, i]);

					// ���� ���� �ʰ��ϴ���
					if (next.x < 0 || next.x >= map.GetLength(1) ||
						next.y < 0 || next.y >= map.GetLength(0))
						continue;

					// �̹� �湮�� �������
					if (visited[next.y, next.x])
						continue;

					// ������ �� �ִ� �������� 
					if (((int)map[current.y, current.x].mapNode.directionMask & (1 << i)) == 0)
						continue;

					stack.Push(next);
					pairs.Add(new Pair(current, next));
				}
			}
		}

		path = null;
		return false;
	}

	public bool TryGetBFSPath(out List<Vector2> path)
	{
		Map.Node[,] map = Map.instance.nodes;
		bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)]; // �湮 Ȯ�ο� (������ ��δ� �ٽ� Ž���ϸ� �ȵǴϱ�)
		Queue<Coord> queue = new Queue<Coord>(); // ���� Ž�� ��� ����
		List<Pair> pairs = new List<Pair>(); // ��� ������������ ��ν�
		Coord start = Map.instance.VectorToCoord(transform.position);
		Coord end = Map.instance.VectorToCoord(destination);
		queue.Enqueue(start);
		int[,] dir = new int[2, 4]
		{
			// �� �� �� ��
			{  0,  0,  1, -1 }, // y
			{  1, -1,  0,  0 }  // x
		};

		while (queue.Count > 0)
		{
			Coord current = queue.Dequeue(); // ���� �� ������ ������ Ž��
			visited[current.y, current.x] = true; // �湮 �Ϸ�

			// Ž���Ϸ�
			if (current == end)
			{
				path = BacktrackPath(start, end, pairs);
				return true;
			}
			else
			{
				for (int i = 0; i < dir.GetLength(1); i++)
				{
					Coord next = current + new Coord(dir[1, i], dir[0, i]);

					// ���� ���� �ʰ��ϴ���
					if (next.x < 0 || next.x >= map.GetLength(1) ||
						next.y < 0 || next.y >= map.GetLength(0))
						continue;

					// �̹� �湮�� �������
					if (visited[next.y, next.x])
						continue;

					// ������ �� �ִ� �������� 
					if (((int)map[current.y, current.x].mapNode.directionMask & (1 << i)) == 0)
						continue;

					queue.Enqueue(next);
					pairs.Add(new Pair(current, next));
				}
			}
		}

		path = null;
		return false;
	}

	private List<Vector2> BacktrackPath(Coord start, Coord end, List<Pair> pairs)
	{
		List<Vector2> path = new List<Vector2>();

		Coord coord = end;
		for (int i = pairs.Count - 1; i >= 0; i--)
		{
			if (pairs[i].next == coord)
			{
				path.Add(Map.instance.CoordToVector(coord));
				coord = pairs[i].prev;
			}
			pairs.RemoveAt(i);
		}

		if (coord != start)
			throw new System.Exception($"[Pathfinder] : ��� ������ ����.. ��ο� ����������");

		path.Add(Map.instance.CoordToVector(coord));
		path.Reverse();
		return path;
	}

	private IEnumerator C_DrawPathLine()
	{
		_lineRenderer.positionCount = 2;
		_lineRenderer.SetPosition(0, (Vector3)path[0] + _drawLineOffset);

		for (int i = 1; i < path.Count; i++)
		{
			float timeMark = Time.time;
			float t = 0;
			while (t < 1)
			{
				t = _drawLineSpeed * (Time.time - timeMark);
				Vector3 pos = Vector3.Lerp(path[i - 1], path[i], t) + _drawLineOffset;
				_lineRenderer.SetPosition(i, pos);
				yield return null;
			}

			if (i < path.Count - 1)
				_lineRenderer.positionCount++;
		}

	}
}