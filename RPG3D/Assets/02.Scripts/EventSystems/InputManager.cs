using RPG.Singleton;
using RPG.UI;
using UnityEngine;

namespace RPG.EventSystems
{
	public class InputManager : MonoSingleton<InputManager>
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.I))
			{
				InventoryUI inventory = UIManager.instance.Get<InventoryUI>();
				if (inventory.gameObject.activeSelf)
					inventory.Hide();
				else
					inventory.Show();
            }

			if (Input.GetKeyDown(KeyCode.E))
			{
				ItemsEquippedUI itemsEquippedUI = UIManager.instance.Get<ItemsEquippedUI>();
				if (itemsEquippedUI.gameObject.activeSelf)
					itemsEquippedUI.Hide();
				else
					itemsEquippedUI.Show();
			}
		}
	}
}