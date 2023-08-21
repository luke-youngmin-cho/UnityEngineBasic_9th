using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitJudge
{
    None,
    Bad,
    Miss,
    Good,
    Great,
    Cool,
}

public class NoteHitter : MonoBehaviour
{
    [SerializeField] private KeyCode _key;
    [SerializeField] private LayerMask _targetMask;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private GameObject _spotLightEffect;
    [SerializeField] private ParticleSystem _hitEffect;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_key))
        {
            _spriteRenderer.color = _pressedColor;
            _spotLightEffect.SetActive(true);
            _hitEffect.Play();
        }

        if (Input.GetKeyUp(_key))
        {
            _spriteRenderer.color = _originalColor;
            _spotLightEffect.SetActive(false);
        }
    }
}
