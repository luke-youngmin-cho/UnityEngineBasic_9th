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
				path = null; // todo -> back track path.
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