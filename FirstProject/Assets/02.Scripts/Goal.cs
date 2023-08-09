using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & _targetMask) > 0)
        {
            if (other.TryGetComponent(out Horse horse))
            {
                horse.doMove = false;
                RaceManager.instance.AddArrived(horse);
            }
        }
    }
}
