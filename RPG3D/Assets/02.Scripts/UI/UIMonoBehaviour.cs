using System;
using UnityEngine;

namespace RPG.UI
{
	[RequireComponent(typeof(Canvas))]
	public abstract class UIMonoBehaviour : MonoBehaviour, IUI
	{
		public int sortOrder 
		{ 
			get => canvas.sortingOrder;
			set => canvas.sortingOrder = value;
		}
		public bool inputActionEnabled { get; set; }

		protected Canvas canvas;

		public event Action onShow;
		public event Action onHide;


		public void Show()
		{
			UIManager.instance.Push(this);
			gameObject.SetActive(true);
			onShow?.Invoke();
		}

		public void Hide()
		{
			UIManager.instance.Pop(this);
			gameObject.SetActive(false);
			onHide?.Invoke();
		}

		protected virtual void Awake()
		{
			UIManager.instance.Register(this);
		}
	}
}