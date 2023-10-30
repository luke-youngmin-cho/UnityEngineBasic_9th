using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
	public interface IUI
	{
		int sortOrder { get; set; }
		bool inputActionEnabled { get; set; }
		void Show();
		void Hide();

		event Action onShow;
		event Action onHide;
	}
}