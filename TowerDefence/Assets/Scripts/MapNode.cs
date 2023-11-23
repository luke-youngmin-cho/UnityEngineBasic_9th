using UnityEngine;

public enum DirectionMask
{
	Nothing,
	Right,
	Left,
	Up,
	Down,
}

public class MapNode : MonoBehaviour
{
	public DirectionMask directionMask;
}
