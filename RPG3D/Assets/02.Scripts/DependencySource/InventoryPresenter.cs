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
			public AddCommand(InventoryPresenter presenter)
			{
				_presenter = presenter;
			}

			private InventoryPresenter _presenter;
			/// <summary>
			/// 
			/// </summary>
			/// <param name="itemID"> 획득할 아이템 ID</param>
			/// <param name="num"> 획득할 아이템 갯수</param>
			/// <param name="remains"> 획득하지 못하고 남은 갯수 </param>
			/// <returns></returns>
			public bool CanExecute(int itemID, int num, out int remains)
			{
				InventorySource source = _presenter.inventorySource;
				int numMax = ItemDatum.instance[itemID].numMax;
				remains = num;
				for (int i = 0; i < source.Count; i++)
				{
					if ((itemID == source[i].itemID && source[i].itemNum < numMax) ||
						source[i].isEmpty)
					{
						remains -= numMax - source[i].itemNum;

						if (remains <= 0)
						{
							remains = 0;
							return true;
						}	
					}
				}

				return remains < num;
			}

			public void Execute(int itemID, int num, out int remains)
			{
				Inventory inventory = Repository.instance.Get<Inventory>();
				List<Pair<SlotData>> source = inventory.m_items;
				int numMax = ItemDatum.instance[itemID].numMax;
				remains = num;
				for (int i = 0; i < source.Count; i++)
				{
					if ((itemID == source[i].item.itemID && source[i].item.itemNum < numMax) ||
						source[i].item.isEmpty)
					{
						remains -= numMax - source[i].item.itemNum;

						if (remains <= 0)
						{
							inventory.RequestWrite(source[i].id,
												   new SlotData(itemID, numMax + remains),
												   (id, slotData) =>
														_presenter.inventorySource[id] = slotData
													);
							remains = 0;
						}
						else
						{
							inventory.RequestWrite(source[i].id,
												   new SlotData(itemID, numMax),
												   (id, slotData) =>
														_presenter.inventorySource[id] = slotData
													);
						}
					}
				}
			}
		}
		public AddCommand addCommand;

		public void Init()
		{
			Repository.instance.Get<Inventory>()
				.RequestReadAll(data =>
				{
					inventorySource 
						= new InventorySource(data.Select(x => x.item));
				});

			addCommand = new AddCommand(this);
		}
	}
}