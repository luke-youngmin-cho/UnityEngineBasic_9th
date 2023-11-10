using RPG.DataModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Test
{
	public class Test_GetDataModelButton : MonoBehaviour
	{
		private void OnGUI()
		{
			GUI.Box(new Rect(10, 10, 100, 90), "Data Load Test");

			if (GUI.Button(new Rect(20, 40, 80, 20), "Load Inventory"))
			{
				Inventory inventory = Repository.instance.Get<Inventory>();
			}
		}
	}
}