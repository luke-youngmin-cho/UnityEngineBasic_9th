using System;
using UnityEngine;
using RPG.GameSystems;

namespace RPG.UI
{
	[RequireComponent(typeof(Canvas))]
	public abstract class UIMonoBehaviour : MonoBehaviourWithInit, IUI
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

		public override void Init()
		{
			canvas = GetComponent<Canvas>();
			UIManager.instance.Register(this);
		}
	}
}