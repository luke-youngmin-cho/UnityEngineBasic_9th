using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.UI
{
	public class ConfirmWindowUI : UIMonoBehaviour
	{
		[SerializeField] TMP_Text _content;
		[SerializeField] Button _confirm;
		[SerializeField] Button _cancel;

		public void Show(string content, UnityAction onConfirmed, UnityAction onCanceled = null)
		{
			base.Show();
			_content.text = content;

			_confirm.onClick.RemoveAllListeners();
			_confirm.onClick.AddListener(Hide);
			if (onConfirmed != null)
				_confirm.onClick.AddListener(onConfirmed);

			_cancel.onClick.RemoveAllListeners();
			_cancel.onClick.AddListener(Hide);
			if (onCanceled != null)
				_cancel.onClick.AddListener(onCanceled);
		}
	}
}