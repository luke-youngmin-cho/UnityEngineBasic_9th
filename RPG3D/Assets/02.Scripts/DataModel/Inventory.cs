using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.DataModel
{
	[Serializable]
	public struct SlotData : IEquatable<SlotData>
	{
		public int itemID;
		public int itemNum;

		public SlotData(int itemID, int itemNum)
		{
			this.itemID = itemID;
			this.itemNum = itemNum;
		}

		public bool Equals(SlotData other)
		{
			return (itemID == other.itemID &&
					itemNum == other.itemNum);
		}
	}

	[Serializable]
	public class Inventory : DataModelBase<SlotData>
	{
		public override void SetDefaultItems()
		{
			m_items = new List<Pair<SlotData>>(32);
			for (int i = 0; i < 32; i++)
			{
				m_items.Add(new Pair<SlotData>(i, new SlotData(0, 0)));
			}

			m_items[0] = new Pair<SlotData>(0, new SlotData(1, 5));
		}
	}
}