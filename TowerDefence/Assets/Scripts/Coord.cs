
using System;

public struct Pair
{
	public Coord prev;
	public Coord next;

	public Pair(Coord prev, Coord next)
	{
		this.prev = prev;
		this.next = next;
	}
}

[Serializable]
public struct Coord
{
	public static Coord up => new Coord(0, 1);
	public static Coord down => new Coord(0, -1);
	public static Coord right => new Coord(1, 0);
	public static Coord left => new Coord(-1, 0);

	public int x, y;

	public Coord(int x, int y)
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

