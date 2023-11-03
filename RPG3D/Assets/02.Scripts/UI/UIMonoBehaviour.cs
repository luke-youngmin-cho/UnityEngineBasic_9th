using System;
using UnityEngine;
using RPG.GameSystems;
using RPG.EventSystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
		[SerializeField] private bool _hideWhenClickedOutside;

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

			if (_hideWhenClickedOutside)
			{
				GameObject outSidePanel = new GameObject("OutsideTrigger");
				outSidePanel.transform.SetParent(transform);
				outSidePanel.transform.SetAsFirstSibling();
				Image image = outSidePanel.AddComponent<Image>();
				image.color = new Color(0.0f, 0.0f, 0.0f, 0.5f);

				RectTransform rect = outSidePanel.GetComponent<RectTransform>();
				rect.anchorMin = Vector2.zero;
				rect.anchorMax = Vector2.one;
				rect.pivot = Vector2.one * 0.5f;
				rect.localScale = Vector3.one;

				EventTrigger trigger = outSidePanel.AddComponent<EventTrigger>();
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener(eventData => Hide());
				trigger.triggers.Add(entry);
			}
		}

		protected virtual void InputAction()
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				if (CustomInputModule.main.TryGetHovered<GraphicRaycaster,CanvasRenderer>
					(out CanvasRenderer canvasRenderer))
				{
					if (canvasRenderer.transform.root.TryGetComponent(out UIMonoBehaviour ui) &&
						ui != this)
					{
						UIManager.instance.Push(ui);
						ui.InputAction();
					}
				}
			}
		}

		private void Update()
		{
			if (inputActionEnabled)
				InputAction();
		}
	}
}