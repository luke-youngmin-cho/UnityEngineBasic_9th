using System;

namespace RPG.DataModel
{
	public interface IDataModel
	{
		void RequestRead(Action callBack);
		void RequestWrite(Action callBack);
	}
}