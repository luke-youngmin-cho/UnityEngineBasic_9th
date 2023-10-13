using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCam_FollowingPlayer : MonoBehaviour
{
    private CinemachineVirtualCamera _vCam;
    private Transform _followTarget;
    private Transform _followTargetRoot;

    [SerializeField] private float _rotateSpeedY;
    [SerializeField] private float _rotateSpeedX;
    [SerializeField] private float _angleXMin = -8.0f;
    [SerializeField] private float _angleXMax = 45.0f;
    [SerializeField] private float _fovMin = 3.0f;
    [SerializeField] private float _fovMax = 30.0f;
    [SerializeField] private float _scrollThreshold;
    [SerializeField] private float _scrollSpeed;


    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
        _followTarget = _vCam.Follow;
        _followTargetRoot = _followTarget.root;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");       
        float mouseY = Input.GetAxis("Mouse Y");

        _followTargetRoot.Rotate(Vector3.up, mouseX * _rotateSpeedY * Time.deltaTime, Space.World);
        _followTarget.Rotate(Vector3.left, mouseY * _rotateSpeedX * Time.deltaTime, Space.Self);
        _followTarget.localRotation = Quaternion.Euler(ClampAngle(_followTarget.eulerAngles.x, _angleXMin, _angleXMax),
                                                       0.0f,
                                                       0.0f);

        if (Mathf.Abs(Input.mouseScrollDelta.y) > _scrollThreshold)
        {
            _vCam.m_Lens.FieldOfView = 
               Mathf.Clamp(_vCam.m_Lens.FieldOfView - Input.mouseScrollDelta.y * _scrollSpeed * Time.deltaTime,
                           _fovMin,
                           _fovMax);
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        // 각도를 양수로 만듬
        //angle = (angle + 360.0f * (1.0f + Mathf.Abs(angle / 360.0f))) % 360.0f;
        angle = (angle + 360.0f) % 360.0f;

        // min 범위를 음수세팅 할수 있도록 (-30도 부터 30 도 까지 쓰는 등에대한 제한)
        if (angle > 180.0f)
        {
            angle -= 360.0f;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
