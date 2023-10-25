using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameElements
{
	[Serializable]
	public class Stat
	{
		public StatID id;
		public float value
		{
			get => _value;
			set
			{
				if (value == _value)
					return;

				_value = value;
				onValueChanged?.Invoke(value);
			}
		}

		[SerializeField] private float _value;
		public event Action<float> onValueChanged;

		public float valueModified
		{
			get => _valueModified;
			set
			{
				if (value == _valueModified)
					return;

				_valueModified = value;
				onValueModifiedChanged?.Invoke(value);
			}
		}

		private float _valueModified;
		public event Action<float> onValueModifiedChanged;

		private List<StatModifier> _modifiers = new List<StatModifier>();

		public void AddModifier(StatModifier modifier)
		{
			_modifiers.Add(modifier);
			valueModified = CalcValueModified();
		}

		public void RemoveModifier(StatModifier modifier)
		{
			_modifiers.Remove(modifier);
			valueModified = CalcValueModified();
		}

		public void AddModifiers(List<StatModifier> modifiers)
		{
			foreach (var modifier in modifiers)
				_modifiers.Add(modifier);

			valueModified = CalcValueModified();
		}

		public void RemoveModifiers(List<StatModifier> modifiers)
		{
			foreach (var modifier in modifiers)
				_modifiers.Remove(modifier);

			valueModified = CalcValueModified();
		}

		public float CalcValueModified()
		{
			float sumAddFlat = 0.0f;
			float sumAddPercent = 0.0f;
			float sumMulPercent = 0.0f;

			foreach (var modifier in _modifiers)
			{
				switch (modifier.type)
				{
					case StatModType.AddFlat:
						{
							sumAddFlat += modifier.value;
						}
						break;
					case StatModType.AddPercent:
						{
							sumAddPercent += modifier.value;
						}
						break;
					case StatModType.MulPercent:
						{
							sumMulPercent *= modifier.value;
						}
						break;
					default:
						throw new Exception($"[Stat] : {modifier.id} modifier's type is wrong.");
				}
			}

			return (_value + sumAddFlat) +
				   (_value * sumAddPercent) +
				   (_value * sumMulPercent);
		}
	}
}