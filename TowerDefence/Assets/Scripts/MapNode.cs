using System;
using UnityEngine;

[Flags]
public enum DirectionMask
{
	Nothing = 0 << 0,
	Right = 1 << 0,
	Left = 1 << 1,
	Up = 1 << 2,
	Down = 1 << 3,
}

public class MapNode : MonoBehaviour
{
	public DirectionMask directionMask;

	private void Start()
	{
		Map.instance.Register(this);
	}
}
