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
    public State[] states;
    private Animator _animator;
    [SerializeField] private StateLayerMaskData _stateLayerMaskData;
    public float comboResetTimer;
    public bool isComboAvailable;
    public int comboMax;
    public int comboStack;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        BehaviourBase[] behaviours = _animator.GetBehaviours<BehaviourBase>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            behaviours[i].Init(this, _stateLayerMaskData);
        }

        Array layers = Enum.GetValues(typeof(AnimatorLayer));
        states = new State[layers.Length - 1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isComboAvailable)
                ChangeStateForcely(State.Attack);
            else if (comboMax == 0)
                ChangeState(State.Attack);
        }

        if (comboResetTimer > 0.0f)
        {
            comboResetTimer -= Time.deltaTime;

            if (comboResetTimer <= 0.0f)
            {
                ResetCombo();
            }
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
                if (states[layerIndex] != newState)
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

    protected void ChangeStateForcely(State newState)
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


    private void ResetCombo()
    {
        isComboAvailable = false;
        comboStack = 0;
        comboMax = 0;
        _animator.SetInteger("comboStack", 0);
    }

    private void Hit()
    {
        comboStack++; 

        if (comboStack < comboMax)
        {
            isComboAvailable = true;
            _animator.SetInteger("comboStack", comboStack);
        }
        else
        {
            ResetCombo();
        }
    }
}