using System;
using System.Collections.Generic;

namespace RPG.DataModel
{
	/// <summary>
	/// 데이터 모델 인터페이스
	/// </summary>
	/// <typeparam name="T"> 아이템 타입 </typeparam>
	public interface IDataModel<T> : IDataModel
	{
		IEnumerable<T> items { get; }

		void RequestRead(int itemID, Action<int, T> onSuccess);
		void RequestWrite(int itemID, T item, Action<int, T> onSuccess);
	}
}