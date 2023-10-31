using RPG.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
	public class UIManager : Singleton<UIManager>
	{
		public LinkedList<IUI> showns = new LinkedList<IUI>();
		public Dictionary<Type, IUI> _dictionary = new Dictionary<Type, IUI>();

		public void Register(IUI ui)
		{
			Type type = ui.GetType();

            if ((_dictionary.ContainsKey(type) == false))
            {
				_dictionary.Add(type, ui);
				Debug.Log($"[UIManager] : Registered {ui.GetType()}");
            }
        }

		/// <summary>
		/// UI 참조 가져오기
		/// </summary>
		/// <typeparam name="T"> 필요한 UI 타입 </typeparam>
		/// <returns> UI </returns>
		public T Get<T>()
			where T : IUI
		{
			return (T)_dictionary[typeof(T)];
		}

		public void Push(IUI ui)
		{
			// 이미 이 UI 가 가장 뒤에 올라와있음
			if (showns.Count > 0 &&
				showns.Last.Value == ui)
				return;

			// 가장 뒤에 있던 UI 보다 뒤로
			int sortOrder = 0;
			if (showns.Last != null)
			{
				sortOrder = showns.Last.Value.sortOrder + 1;
				showns.Last.Value.inputActionEnabled = false;
			}
			ui.sortOrder = sortOrder;
			ui.inputActionEnabled = true;
			showns.Remove(ui); // 중간에 UI 있으면 뺌
			showns.AddLast(ui); // UI 젤 뒤로 보냄

			if (showns.Count == 1)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.Confined;
			}
		}

		public void Pop(IUI ui)
		{
			// 빼려는 UI 가 가장 뒤에있었고, 
			// 그 앞에 다음 UI가 있다면, 해당 UI 의 유저입력처리 허용
			if (showns.Count > 1 &&
				showns.Last.Value == ui)
			{
				showns.Last.Previous.Value.inputActionEnabled = true;
			}

			showns.Remove(ui);

			if (showns.Count == 0)
			{
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
		}

		public void HideLast()
		{
			if (showns.Count <= 0)
				return;

			showns.Last.Value.Hide();
		}
	}
}