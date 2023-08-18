using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.down * MusicPlayManager.instance.speedGain * Time.fixedDeltaTime);
    }
}
