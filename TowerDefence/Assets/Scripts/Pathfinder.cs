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
		bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)]; // 방문 확인용 (지나온 경로는 다시 탐색하면 안되니까)
		Stack<Coord> stack = new Stack<Coord>(); // 다음 탐색 대상 스택
		List<Pair> pairs = new List<Pair>(); // 경로 역추적을위한 경로쌍
		Coord start = Map.instance.VectorToCoord(transform.position);
		Coord end = Map.instance.VectorToCoord(destination);
		stack.Push(start);
		int[,] dir = new int[2, 4]
		{
			// 우 좌 상 하
			{  0,  0,  1, -1 }, // y
			{  1, -1,  0,  0 }  // x
		};

		while (stack.Count > 0)
		{
			Coord current = stack.Pop(); // 스택 젤 위에꺼 꺼내서 탐색
			visited[current.y, current.x] = true; // 방문 완료

			// 탐색완료
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

					// 맵의 범위 초과하는지
					if (next.x < 0 || next.x >= map.GetLength(1) ||
						next.y < 0 || next.y >= map.GetLength(0))
						continue;

					// 이미 방문한 경로인지
					if (visited[next.y, next.x])
						continue;

					// 지나갈 수 있는 방향인지 
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
		bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)]; // 방문 확인용 (지나온 경로는 다시 탐색하면 안되니까)
		Queue<Coord> queue = new Queue<Coord>(); // 다음 탐색 대상 스택
		List<Pair> pairs = new List<Pair>(); // 경로 역추적을위한 경로쌍
		Coord start = Map.instance.VectorToCoord(transform.position);
		Coord end = Map.instance.VectorToCoord(destination);
		queue.Enqueue(start);
		int[,] dir = new int[2, 4]
		{
			// 우 좌 상 하
			{  0,  0,  1, -1 }, // y
			{  1, -1,  0,  0 }  // x
		};

		while (queue.Count > 0)
		{
			Coord current = queue.Dequeue(); // 스택 젤 위에꺼 꺼내서 탐색
			visited[current.y, current.x] = true; // 방문 완료

			// 탐색완료
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

					// 맵의 범위 초과하는지
					if (next.x < 0 || next.x >= map.GetLength(1) ||
						next.y < 0 || next.y >= map.GetLength(0))
						continue;

					// 이미 방문한 경로인지
					if (visited[next.y, next.x])
						continue;

					// 지나갈 수 있는 방향인지 
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
			throw new System.Exception($"[Pathfinder] : 경로 역추적 실패.. 경로에 문제가있음");

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