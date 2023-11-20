using RPG.DependencySource;
using RPG.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
	public class InventoryUI : UIMonoBehaviour
	{
		[SerializeField] InventorySlot _slotPrefab;
		[SerializeField] Image _selectedPreview;
		private InventoryPresenter _presenter;
		private List<InventorySlot> _slots;
		private int _selectedID;

		public override void Init()
		{
			base.Init();

			GridLayoutGroup content = GetComponentInChildren<GridLayoutGroup>();

			_presenter = new InventoryPresenter();
			_presenter.Init();

			_slots = new List<InventorySlot>();
			for (int i = 0; i < _presenter.inventorySource.Count; i++)
			{
				InventorySlot slot = Instantiate(_slotPrefab, content.transform);
				slot.Refresh(_presenter.inventorySource[i]);
				slot.id = i;
				_slots.Add(slot);
			}

			_presenter.inventorySource.onItemChanged += (id, data) =>
			{
				_slots[id].Refresh(data);
			};
		}

		protected override void InputAction()
		{
			base.InputAction();
			if (Input.GetMouseButtonDown(0))
			{
				if (_selectedID < 0)
				{
					if (CustomInputModule.main.TryGetHovered<GraphicRaycaster, InventorySlot>
						(out InventorySlot slot))
					{
						Select(slot.id);
						return;
					}
				}
				else
				{
					if (CustomInputModule.main.TryGetHovered<GraphicRaycaster>
						(out IEnumerable<GameObject> hovered))
					{
						foreach (var item in hovered)
						{
							if (item.TryGetComponent(out InventorySlot inventorySlot))
							{
								if (inventorySlot != _slots[_selectedID])
									if (_presenter.swapCommand.CanExecute(_selectedID, inventorySlot.id))
										_presenter.swapCommand.Execute(_selectedID, inventorySlot.id);
							}
							//else if (item.TryGetComponent(out itemEquippedSlot itemEquippedSlot))
							//{
							//	// 장비 장착 시도
							//}
						}
					}
					else
					{
						// UI 캐스팅 안됐는지 ? -> 정말 버릴건지에 대한 확인창 팝업 & 캔슬
					}

					

				}
				Deselect();
			}
			else if (Input.GetMouseButtonDown(1))
			{
				// todo -> use item
				Deselect();
			}

			_selectedPreview.transform.position = Input.mousePosition;
		}

		private void Select(int slotID)
		{
			_selectedID = slotID;
			_selectedPreview.sprite
				= ItemDatum.instance[_presenter.inventorySource[_selectedID].itemID].icon;
			_selectedPreview.enabled = true;
		}

		private void Deselect()
		{
			_selectedID = -1;
			_selectedPreview.enabled = false;
		}
	}
}