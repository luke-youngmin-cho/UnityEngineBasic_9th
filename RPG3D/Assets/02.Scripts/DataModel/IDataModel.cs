using System;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DataModel
{
	public interface IDataModel
	{
		IEnumerable<int> itemIDs { get; }
		IEnumerable items { get; }

		void RequestRead(int itemID, Action<int, object> onSuccess);
		void RequestWrite(int itemID, object item, Action<int, object> onSuccess);

		void SetDefaultItems();
	}
}