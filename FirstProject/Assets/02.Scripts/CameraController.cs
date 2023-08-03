using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 _offset;
    private Vector3 _angle;

    private void Awake()
    {
        _offset = transform.position;
        _angle = transform.eulerAngles;
    }

    private void LateUpdate()
    {
        Transform target = RaceManager.instance.lead.transform;

        if (target != null)
        {
            transform.position = target.position + _offset;
        }
    }
}
