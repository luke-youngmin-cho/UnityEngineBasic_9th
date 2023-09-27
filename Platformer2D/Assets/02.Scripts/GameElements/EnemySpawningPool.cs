using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemySpawningPool : MonoBehaviour
{
    private IObjectPool<EnemyMachine> _pool;
    [SerializeField] private int _maxSize;
    [SerializeField] private EnemyMachine _enemy;
    private BoxCollider2D _bound;
    [SerializeField] private LayerMask _spawnableMask;
    [SerializeField] private float _spawnDelay;
    private WaitForSeconds _waitForSpawnDelay;

    private void Awake()
    {
        _bound = GetComponent<BoxCollider2D>();
        _pool = new ObjectPool<EnemyMachine>(Create, GetAction, ReleaseAction, DestroyAction, true, _maxSize, _maxSize);
        _waitForSpawnDelay = new WaitForSeconds(_spawnDelay);
    }

    private void Start()
    {
        for (int i = 0; i < _maxSize; i++)
        {
            Spawn();
        }
    }

    private EnemyMachine Create()
    {
        EnemyMachine enemy = Instantiate(_enemy);
        enemy.onHpMin += () => _pool.Release(enemy);
        return enemy;
    }

    private void GetAction(EnemyMachine enemy)
    {
        enemy.RecoverHp(null, float.MaxValue);
        enemy.isInvincible = false;
        enemy.ai = EnemyMachine.AI.Think;
        enemy.gameObject.SetActive(true);
        enemy.transform.SetParent(null);
    }

    private void ReleaseAction(EnemyMachine enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.transform.SetParent(transform);
        StartCoroutine(C_Spawn());
    }

    private void DestroyAction(EnemyMachine enemy)
    {
        Destroy(enemy.gameObject);
    }

    IEnumerator C_Spawn()
    {
        yield return _waitForSpawnDelay;
        Spawn();
    }

    private void Spawn()
    {
        int count = 10;
        while (count > 0)
        {
            float x = Random.Range(transform.position.x + _bound.offset.x + _bound.size.x / 2.0f,
                               transform.position.x + _bound.offset.x - _bound.size.x / 2.0f);

            float y = Random.Range(transform.position.y + _bound.offset.y + _bound.size.y / 2.0f,
                                   transform.position.y + _bound.offset.y - _bound.size.y / 2.0f);

            RaycastHit2D hit =
                Physics2D.Raycast(new Vector2(x, y),
                                  Vector2.down,
                                  y - (transform.position.y + _bound.offset.y - _bound.size.y / 2.0f),
                                  _spawnableMask);

            Debug.DrawLine(new Vector2(x, y),
                           new Vector2(x, transform.position.y + _bound.offset.y - _bound.size.y / 2.0f),
                           Color.magenta,
                           1.0f);

            if (hit.collider != null)
            {
                EnemyMachine enemy = _pool.Get();
                enemy.transform.position = hit.point + Vector2.up * 0.001f;
                break;
            }

            count--;
        }
    }
}
