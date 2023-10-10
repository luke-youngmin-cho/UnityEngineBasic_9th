using System;
using UnityEngine;

public enum State
{
    None,
    Move,
    Jump,
    Fall,
    Land,
    DoubleJump,
    Attack = 20,
}

public class CharacterController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private StateLayerMaskData _stateLayerMaskData;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        BehaviourBase[] behaviours = _animator.GetBehaviours<BehaviourBase>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            behaviours[i].Init(_stateLayerMaskData);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeState(State.Attack);
        }
    }


    protected void ChangeState(State newState)
    {
        _animator.SetInteger("state", (int)newState);

        int layerIndex = 0;
        foreach (AnimatorLayer layer in Enum.GetValues(typeof(AnimatorLayer)))
        {
            if (layer == AnimatorLayer.None)
                continue;

            if ((layer & _stateLayerMaskData.animatorLayerPairs[newState]) > 0)
            {
                _animator.SetBool($"dirty{layer}", true);
                _animator.SetLayerWeight(layerIndex, 1.0f);
            }
            else
            {
                _animator.SetLayerWeight(layerIndex, 0.0f);
            }
            layerIndex++;
        }
    }
}