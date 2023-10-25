using System;

namespace RPG.GameElements
{
	public enum StatModType
	{
		None,
		AddFlat,
		AddPercent,
		MulPercent,
	}

	[Serializable]
	public class StatModifier
	{
		public StatID id;
		public StatModType type;
		public float value;
	}
}