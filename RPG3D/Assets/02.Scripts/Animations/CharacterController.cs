using System;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField] private float _groundDetectRadius;
    private Vector3 _inertia;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _slope = 45.0f;
    private Rigidbody _rigidbody;


	public virtual bool useAI
	{
		get => _useAI;
		set => _useAI = value;
	}
	[SerializeField] private bool _useAI;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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

    protected virtual void FixedUpdate()
    {
		if (_useAI)
			return;

        if (DetectGround())
            _inertia.y = 0.0f;
        else
            _inertia.y += Physics.gravity.y * Time.fixedDeltaTime;

        if (_inertia.magnitude > 0.0f)
            transform.Translate(_inertia * Time.fixedDeltaTime);

        Vector3 expected = transform.position
                           + Quaternion.LookRotation(transform.forward, Vector3.up) * move * Time.fixedDeltaTime;
        float distanceExpected = Vector3.Distance(transform.position, expected);

        Debug.DrawRay(expected + Vector3.up,
                      Vector3.down * (1.0f + Mathf.Tan(_slope) * distanceExpected),
                      Color.red);

        if (Physics.Raycast(expected + Vector3.up,
                            Vector3.down,
                            out RaycastHit hit,
                            1.0f + Mathf.Tan(_slope) * distanceExpected,
                            _groundMask))
        {
            // 45�� ������ ����
            if (hit.point.y < transform.position.y + Mathf.Tan(_slope) * distanceExpected)
            {
                expected = new Vector3(expected.x, hit.point.y, expected.z);
                transform.position = expected;
                //if (NavMesh.SamplePosition(expected, out NavMeshHit navMeshHit, float.PositiveInfinity, NavMesh.AllAreas))
                //{
                //    transform.position = navMeshHit.position;
                //}
            }
        }

    }

    private bool DetectGround()
    {
        Collider[] cols 
            = Physics.OverlapSphere(transform.position, _groundDetectRadius, _groundMask);
        return cols.Length > 0;
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

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _groundDetectRadius);
    }
}