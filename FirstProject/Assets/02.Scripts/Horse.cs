using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    public float totalDistance => Vector3.Distance(transform.position, _startPos);

    // [SerializeField] : 해당 필드를 유니티에디터의 인스펙터창에 노출시키기위한 Attribute.
    public bool doMove;
    [SerializeField] private float _speed = 5.0f;
    [Range(0.0f, 1.0f)] [SerializeField] private float _stability;
    private float _speedModified;
    private float _speedModifyingDistance = 1f;
    private float _speedModifyedDistanceMark;
    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
        _speedModified = _speed * Random.Range(_stability, 1.0f);
    }

    private void Start()
    {
        RaceManager.instance.Register(this);
    }

    private void FixedUpdate()
    {
        if (doMove)
        {
            if (totalDistance - _speedModifyedDistanceMark > _speedModifyingDistance)
            {
                _speedModified = _speed * Random.Range(_stability, 1.0f);
                _speedModifyedDistanceMark = totalDistance;
            }

            Move();
        }
    }

    // 거리 = 속력 x 시간
    // 한프레임당 거리 = 속력 x 한프레임당시간
    private void Move()
    {
        transform.position += Vector3.forward * _speedModified * Time.fixedDeltaTime;
    }
}
