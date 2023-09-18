using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public event Action onAttackHit;

    private void AttackHit()
    {
        onAttackHit?.Invoke();
    }
}
