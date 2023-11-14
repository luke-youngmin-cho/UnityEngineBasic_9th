using RPG.Collections;
using RPG.DataModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace RPG.DependencySource
{
	public class InventoryPresenter
	{
		public class InventorySource : ObservableCollection<SlotData>
		{
			public InventorySource(IEnumerable<SlotData> copy)
				:base(copy) { }
		}
		public InventorySource inventorySource;

		public class AddCommand
		{
			public bool CanExecute(int itemID, int num, out int remains)
			{

			}

			public void Execute(int itemID, int num, out int remains)
			{

			}
		}

		public void Init()
		{
			Repository.instance.Get<Inventory>()
				.RequestReadAll(data =>
				{
					inventorySource 
						= new InventorySource(data.Select(x => x.item));
				});
		}
	}
}