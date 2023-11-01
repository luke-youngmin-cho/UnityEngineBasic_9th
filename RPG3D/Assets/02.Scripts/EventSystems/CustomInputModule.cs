using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.EventSystems
{
	public class CustomInputModule : StandaloneInputModule
	{
		public static CustomInputModule main;

		protected override void Awake()
		{
			base.Awake();

			if (main != null)
				main = this;
		}

		// 어떤 Raycaster 로 인해 Casting 된 모든 GameObject 를 반환하는 함수
		public bool TryGetHovered<T>(out IEnumerable<GameObject> hovered, int mouseID = kMouseLeftId)
			where T : BaseRaycaster
		{
			// 특정 mouseID 에 대한 마우스 이벤트 데이터를 읽음
			if (m_PointerData.TryGetValue(mouseID, out PointerEventData pointerEventData))
			{
				BaseRaycaster module = pointerEventData.pointerCurrentRaycast.module;
				if (module != null &&
					module is T)
				{
					hovered = pointerEventData.hovered;
					return true;
				}
			}

			hovered = null;
			return false;
		}


		// 어떤 Raycaster 로 인해 Casting 된 GameObject 들 중에 
		// 특정 Component 를 찾는 함수
		public bool TryGetHovered<T, K>(out K hovered, int mouseID = kMouseLeftId)
			where T : BaseRaycaster
		{
			// 특정 mouseID 에 대한 마우스 이벤트 데이터를 읽음
			if (m_PointerData.TryGetValue(mouseID, out PointerEventData pointerEventData))
			{
				BaseRaycaster module = pointerEventData.pointerCurrentRaycast.module;
				if (module != null &&
					module is T)
				{
					foreach (var item in pointerEventData.hovered)
					{
						if (item.TryGetComponent(out hovered))
							return true;
					}
				}
			}

			hovered = default;
			return false;
		}
	}
}