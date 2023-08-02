using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    // [SerializeField] : 해당 필드를 유니티에디터의 인스펙터창에 노출시키기위한 Attribute.

    public bool doMove;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _stability;

    private void FixedUpdate()
    {
        if (doMove)
            Move();
    }

    // 거리 = 속력 x 시간
    // 한프레임당 거리 = 속력 x 한프레임당시간
    private void Move()
    {
        transform.position += Vector3.forward * _speed * Time.fixedDeltaTime;
    }
}
