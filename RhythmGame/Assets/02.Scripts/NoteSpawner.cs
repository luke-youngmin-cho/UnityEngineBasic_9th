using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public KeyCode key;
    [SerializeField] private GameObject _notePrefab;
    [SerializeField] private Color _color;
    [SerializeField] private Vector3 _noteScale;

    public void Spawn()
    {
        GameObject note = Instantiate(_notePrefab, transform.position, Quaternion.identity);
        note.transform.localScale = _noteScale;
        note.GetComponent<SpriteRenderer>().color = _color;
    }
}
