using RPG.Collections;
using RPG.DataModel;
using System.Collections.Generic;
using System.Linq;

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

		public class RemoveCommand
		{
			public RemoveCommand(InventoryPresenter presenter) => _presenter = presenter;

			private InventoryPresenter _presenter;

			/// <param name="slotID"> 양수면 특정 슬롯에 아이템 지울수 있는지, 음수면 전체 인벤토리에서 아이템 지울 수 있는지</param>
			public bool CanExecute(int slotID, int itemID, int itemNum)
			{
				InventorySource source = _presenter.inventorySource;

				if (slotID >= 0)
				{
					if (source[slotID].itemID.Equals(itemID) && 
						source[slotID].itemNum >= itemNum)
					{
						return true;
					}
				}
				else
				{
					List<int> indices = source.FindAllIndex(slot => slot.itemID.Equals(itemID));
					int remains = itemNum;
					for (int i = 0; i < indices.Count; i++)
					{
						remains -= source[i].itemNum;
					}

					return remains >= 0;
				}
				return false;
			}

			public void Execute(int slotID, int itemID, int itemNum)
			{
				Inventory inventory = Repository.instance.Get<Inventory>();
				List<Pair<SlotData>> source = inventory.m_items;

				if (slotID >= 0)
				{
					if (source[slotID].item.itemID.Equals(itemID) &&
						source[slotID].item.itemNum >= itemNum)
					{
						int numEstimated = source[slotID].item.itemNum - itemNum;
						inventory.RequestWrite(slotID, 
											   numEstimated > 0 ? new SlotData(itemID, numEstimated)
																: SlotData.empty,
											   (slotID, slotData) =>
											   {
												   _presenter.inventorySource[slotID] = slotData;
											   });
					}
				}
				else
				{
					int remains = itemNum;
					for (int i = 0; i < source.Count; i++)
					{
						if (source[i].item.itemID.Equals(itemID))
						{
							remains -= source[i].item.itemNum;

							if (remains >= 0)
							{
								inventory.RequestWrite(i,
													   SlotData.empty,
													   (slotID, slotData) =>
													   {
														   _presenter.inventorySource[slotID] = slotData;
													   });
								if (remains == 0)
									return;
							}
							else
							{
								throw new System.Exception($"[InventoryPresneter] : there're some problems with refreshing dependency source");
							}
						}
					}
				}
			}
		}
		public RemoveCommand removeCommand;


		public void Init()
		{
			Repository.instance.Get<Inventory>()
				.RequestReadAll(data =>
				{
					inventorySource 
						= new InventorySource(data.Select(x => x.item));
				});

			addCommand = new AddCommand(this);
			removeCommand = new RemoveCommand(this);
		}
	}
}