using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadCatcher : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << collision.gameObject.layer & _targetMask) > 0)
        {
            if (collision.TryGetComponent(out Note note))
            {
                Destroy(note.gameObject);
                MusicPlayManager.instance.badCount++;
            }
        }
    }
}
