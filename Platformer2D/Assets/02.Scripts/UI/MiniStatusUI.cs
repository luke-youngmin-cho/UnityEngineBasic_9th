using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniStatusUI : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;

    private void Start()
    {
        IHp target = GetComponentInParent<IHp>();
        _hpBar.minValue = target.hpMin;
        _hpBar.maxValue = target.hpMax;
        _hpBar.value = target.hpValue;
        target.onHpChanged += (value) => _hpBar.value = value;

        CharacterMachine machine = GetComponentInParent<CharacterMachine>();
        machine.onDirectionChanged += (direction) =>
        {
            transform.eulerAngles = Vector2.zero;
        };
    }
}
