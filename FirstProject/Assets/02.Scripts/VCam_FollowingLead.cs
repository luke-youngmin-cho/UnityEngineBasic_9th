using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCam_FollowingLead : MonoBehaviour
{
    private CinemachineVirtualCamera _vCam;

    private void Awake()
    {
        _vCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void LateUpdate()
    {
        Transform lead = RaceManager.instance.lead.transform;
        _vCam.Follow = lead;
        _vCam.LookAt = lead;
    }
}
