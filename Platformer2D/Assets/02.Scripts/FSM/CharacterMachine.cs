using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum State
{
    None,
    Idle,
    Move,
    Jump,
    JumpDown,
    SecondJump,
    Fall,
    Land,
    Crouch,
    LadderClimbing,
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
    public const int DIRECTION_UP = 1;
    public const int DIRECTION_DOWN = -1;

    // Movement
    public virtual float horizontal { get; set; }
    public virtual float vertical { get; set; }
    public float speed;
    [HideInInspector] public Vector2 move;
    [HideInInspector] public bool isMovable;
    private Rigidbody2D _rigidbody;

    public State current;
    private Dictionary<State, IWorkflow<State>> _states;
    private bool _isDirty;
    public Animator animator;

    // Flags
    public bool hasJumped;
    public bool hasSecondJumped;

    // Ground detection
    public bool isGrounded { get; private set; }
    public bool isGroundExistBelow { get; private set; }
    public Collider2D ground { get; private set; }
    [Header("Ground Detection")]
    [SerializeField] private Vector2 _groundDetectCenter;
    [SerializeField] private Vector2 _groundDetectSize;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Vector2 _groundBelowDetectCenter;
    [SerializeField] private float _groundBelowDetectDistance;

    // Ladder detection
    public bool canLadderUp { get; private set; }
    public bool canLadderDown { get; private set; }
    public Ladder upLadder;
    public Ladder downLadder;
    [SerializeField] private float _ladderUpDetectOffset;
    [SerializeField] private float _ladderDownDetectOffset;
    [SerializeField] private float _ladderDetectRadius;
    [SerializeField] private LayerMask _ladderMask;



    public void Initialize(IEnumerable<KeyValuePair<State, IWorkflow<State>>> copy)
    {
        _states = new Dictionary<State, IWorkflow<State>>(copy);
        current = copy.First().Key;
    }

    public bool ChangeState(State newState, object[] parameters = null)
    {
        if (_isDirty)
            return false;

        if (newState == current)
            return false;

        if (_states[newState].CanExecute == false)
            return false;

        _states[current].OnExit();
        current = newState;
        _states[newState].OnEnter(parameters);
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
        ChangeState(_states[current].OnUpdate());

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
        _states[current].OnFixedUpdate();
        _rigidbody.position += move * Time.fixedDeltaTime;
        DetectGround();
        DetectLadder();
    }

    private void LateUpdate()
    {
        _isDirty = false;
    }

    private void DetectGround()
    {
        ground = Physics2D.OverlapBox(_rigidbody.position + _groundDetectCenter,
                                      _groundDetectSize,
                                      0.0f,
                                      _groundMask);

        isGrounded = ground;

        if (isGrounded)
        {
            RaycastHit2D hit =
                Physics2D.BoxCast(origin: _rigidbody.position + _groundBelowDetectCenter,
                                  size: _groundDetectSize,
                                  angle: 0.0f,
                                  direction: Vector2.down,
                                  distance: _groundBelowDetectDistance,
                                  layerMask: _groundMask);

            isGroundExistBelow = hit.collider;
        }
        else
        {
            isGroundExistBelow = false;
        }
    }

    private void DetectLadder()
    {
        Collider2D upCol =
        Physics2D.OverlapCircle(_rigidbody.position + Vector2.up * _ladderUpDetectOffset,
                                _ladderDetectRadius,
                                _ladderMask);

        upLadder = upCol ? upCol.GetComponent<Ladder>() : null;
        canLadderUp = upLadder;

        Collider2D downCol =
        Physics2D.OverlapCircle(_rigidbody.position + Vector2.up * _ladderDownDetectOffset,
                                _ladderDetectRadius,
                                _ladderMask);

        downLadder = downCol ? downCol.GetComponent<Ladder>() : null;
        canLadderDown = downLadder;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)_groundDetectCenter,
                            _groundDetectSize);

        Gizmos.color = Color.grey;
        Gizmos.DrawWireCube(transform.position + (Vector3)_groundBelowDetectCenter + Vector3.down * _groundBelowDetectDistance / 2.0f,
                            new Vector3(_groundDetectSize.x, _groundDetectSize.y + _groundBelowDetectDistance));

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position + Vector3.up *_ladderUpDetectOffset, _ladderDetectRadius);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position + Vector3.up * _ladderDownDetectOffset, _ladderDetectRadius);
    }
}