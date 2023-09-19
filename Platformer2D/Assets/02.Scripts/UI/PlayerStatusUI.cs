using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private PlayerMachine _player;

    private void Start()
    {
        _hpBar.minValue = _player.hpMin;
        _hpBar.maxValue = _player.hpMax;
        _hpBar.value = _player.hpValue;

        _player.onHpChanged += (value) => _hpBar.value = value;
    }
}
