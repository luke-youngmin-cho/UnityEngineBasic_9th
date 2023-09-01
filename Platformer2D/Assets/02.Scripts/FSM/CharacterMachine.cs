using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum State
{
    None,
    Idle,
    Move,
    Jump,
    Fall,
    Land,
}

public abstract class CharacterMachine : MonoBehaviour
{
    // Direction
    public int direction
    {
        get => _direction;
        set
        {
            if (isDirectionChangeable == false)
                return;

            if (_direction == value)
                return;

            if (value > 0)
            {
                transform.eulerAngles = Vector3.zero;
                _direction = DIRECTION_RIGHT;
            }
            else if (value < 0)
            {
                transform.eulerAngles = Vector3.up * 180.0f;
                _direction = DIRECTION_LEFT;
            }
            else
                throw new System.Exception("[CharacterMachine] : Invalid direction (0).");
        }
    }
    private int _direction;
    [HideInInspector] public bool isDirectionChangeable;
    public const int DIRECTION_RIGHT = 1;
    public const int DIRECTION_LEFT = -1;

    // Movement
    public virtual float horizontal { get; set; }
    public float speed;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public bool isMovable;
    private Rigidbody2D _rigidbody;

    public State current;
    private Dictionary<State, IWorkflow<State>> _states;
    private bool _isDirty;
    public Animator animator;

    public bool isGrounded
    {
        get
        {
            return Physics2D.OverlapBox(_rigidbody.position + _groundDetectCenter,
                                        _groundDetectSize,
                                        0.0f,
                                        _groundMask);
        }
    }
    [Header("Ground Detection")]
    [SerializeField] private Vector2 _groundDetectCenter;
    [SerializeField] private Vector2 _groundDetectSize;
    [SerializeField] private LayerMask _groundMask;

    public void Initialize(IEnumerable<KeyValuePair<State, IWorkflow<State>>> copy)
    {
        _states = new Dictionary<State, IWorkflow<State>>(copy);
        current = copy.First().Key;
    }

    public bool ChangeState(State newState)
    {
        if (_isDirty)
            return false;

        if (newState == current)
            return false;

        if (_states[newState].CanExecute == false)
            return false;

        current = newState;
        _states[newState].Reset();
        ChangeState(_states[newState].MoveNext());
        _isDirty = true;
        return true;
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        direction = DIRECTION_RIGHT;
    }

    protected virtual void Update()
    {
        ChangeState(_states[current].MoveNext());

        if (isMovable)
        {
            move = new Vector2(horizontal * speed, 0.0f);
        }

        if (isDirectionChangeable &&
            Mathf.Abs(horizontal) > 0.0f)
        {
            direction = horizontal < 0.0f ? DIRECTION_LEFT : DIRECTION_RIGHT;
        }
    }


    private void FixedUpdate()
    {
        _rigidbody.position += move * Time.fixedDeltaTime;
    }

    private void LateUpdate()
    {
        _isDirty = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_groundDetectCenter,
                            _groundDetectSize);
    }
}