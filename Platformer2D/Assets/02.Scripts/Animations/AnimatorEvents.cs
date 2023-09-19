using System;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public event Action onAttackHit;

    private void AttackHit()
    {
        Debug.Log("Hit");
        onAttackHit?.Invoke();
    }
}
