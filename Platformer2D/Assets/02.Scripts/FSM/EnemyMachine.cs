using System.Collections.Generic;
using UnityEngine;

public class EnemyMachine : CharacterMachine
{
    private enum AI
    {
        None,
        Think,
        ExecuteRandomBehaviour,
        WaitUntilRandomBehaviourFinished,
        Follow,
    }
    [SerializeField] private AI _ai;
    private Transform _target;
    private List<State> _aiBehaviours = new List<State>() { State.Idle, State.Move, State.Jump};

    [Header("AI")]
    [SerializeField] private bool _autoFollow;
    [SerializeField] private float _targetDetectRange;
    [SerializeField] private bool _attackEnable;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _targetDetectMask;
    [SerializeField] private float _thinkTimeMin;
    [SerializeField] private float _thinkTimeMax;
    private float _thinkTimer;

    private CapsuleCollider2D _col;

    protected override void Awake()
    {
        base.Awake();
        _col = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        Initialize(CharacterStateWorkflowsDataSheet.GetWorkflowsForEnemy(this));
        onHpDepleted += (amount) => ChangeState(State.Hurt);
        onHpMin += () =>
        {
            _ai = AI.None;
            move = Vector2.zero;
            ChangeState(State.Die);
        };
        _ai = AI.Think;
    }

    protected override void Update()
    {
        UpdateAI();
        base.Update();
    }

    private void UpdateAI()
    {
        switch (_ai)
        {
            case AI.None:
                break;
            case AI.Think:
                {
                    Collider2D col = Physics2D.OverlapCircle(transform.position, _targetDetectRange, _targetDetectMask);
                    _target = col?.transform;

                    if (_autoFollow &&
                        _target)
                    {
                        _ai = AI.Follow;
                    }
                    else
                    {
                        _ai = AI.ExecuteRandomBehaviour;
                    }
                }
                break;
            case AI.ExecuteRandomBehaviour:
                {
                    horizontal = Random.Range(DIRECTION_LEFT, DIRECTION_RIGHT + 1);
                    State behaviour = _aiBehaviours[Random.Range(0, _aiBehaviours.Count)];
                    if (ChangeState(behaviour))
                    {
                        _thinkTimer = Random.Range(_thinkTimeMin, _thinkTimeMax);
                        _ai = AI.WaitUntilRandomBehaviourFinished;
                    }
                }
                break;
            case AI.WaitUntilRandomBehaviourFinished:
                {
                    if (_thinkTimer <= 0)
                    {
                        _ai = AI.Think;
                    }
                    else
                    {
                        _thinkTimer -= Time.deltaTime;
                    }
                }
                break;
            case AI.Follow:
                {
                    if (_target == null ||
                        Vector2.Distance(transform.position, _target.position) > _targetDetectRange)
                    {
                        _ai = AI.Think;
                        return;
                    }

                    if (transform.position.x < _target.position.x - _col.size.x)
                    {
                        horizontal = DIRECTION_RIGHT;
                    }
                    else if (transform.position.x > _target.position.x + _col.size.x)
                    {
                        horizontal = DIRECTION_LEFT;
                    }

                    if (_attackEnable &&
                        Vector2.Distance(transform.position, _target.position) <= _attackRange)
                    {
                        ChangeState(State.Attack);
                    }
                    else
                    {
                        ChangeState(State.Move);
                    }
                }
                break;
            default:
                break;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (_autoFollow)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _targetDetectRange);
        }

        if (_attackEnable)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _targetDetectMask) > 0)
        {
            if (collision.TryGetComponent(out CharacterMachine target))
            {
                if (target.isInvincible == false)
                {
                    float damage = Random.Range(attackForceMin, attackForceMax);
                    target.DepleteHp(this, damage);
                    Vector2 knockBackForce = new Vector2((target.transform.position - transform.position).normalized.x, 1.0f);
                    target.KnockBack(knockBackForce);
                    DamagePopUp.Create(target.transform.position + Vector3.up * 0.5f,
                                       (int)damage,
                                       gameObject.layer);
                }
            }
        }
    }
}
