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
				path = null; // todo -> back track path.
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
					if (((int)map[next.y, next.x].mapNode.directionMask & (1 << i)) == 0)
						continue;

					stack.Push(next);
					pairs.Add(new Pair(current, next));
				}
			}
		}

		path = null;
		return false;
	}
}