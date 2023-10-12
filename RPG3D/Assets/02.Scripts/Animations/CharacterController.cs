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

public abstract class CharacterController : MonoBehaviour
{
    public abstract float horizontal { get; }
    public abstract float vertical { get; }
    public abstract float moveGain { get; }

    public Vector3 move;
    public bool isMovable
    {
        get
        {
            if (states[0] != State.Move)
                return false;

            for (int i = 1; i < states.Length; i++)
            {
                if (states[i] != State.None)
                    return false;
            }

            return true;
        }
    }


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

    protected virtual void Update()
    {
        if (isMovable)
        {
            move = new Vector3(horizontal, 0.0f, vertical).normalized * moveGain;
        }
        _animator.SetFloat("h", horizontal * moveGain);
        _animator.SetFloat("v", vertical * moveGain);


        if (comboResetTimer > 0.0f)
        {
            comboResetTimer -= Time.deltaTime;

            if (comboResetTimer <= 0.0f)
            {
                ResetCombo();
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(move * Time.fixedDeltaTime, Space.Self);
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