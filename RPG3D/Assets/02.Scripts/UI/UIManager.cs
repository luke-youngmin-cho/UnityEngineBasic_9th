using RPG.Singleton;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RPG.UI
{
	public class UIManager : Singleton<UIManager>
	{
		public LinkedList<IUI> showns = new LinkedList<IUI>();
		public Dictionary<Type, IUI> _dictionary = new Dictionary<Type, IUI>();

		void Register(IUI ui)
		{
			Type type = ui.GetType();

            if ( (_dictionary.ContainsKey(type) == false))
            {
				_dictionary.Add(type, ui);
            }
        }
	}
}