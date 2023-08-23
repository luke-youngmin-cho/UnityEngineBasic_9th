using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopUpText : MonoBehaviour
{
    private TMP_Text _message;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _direction = Vector3.up;
    [SerializeField] private float _moveSpeed = 0.5f;
    [SerializeField] private float _fadeSpeed = 0.5f;
    private Color _colorOrigin;
    public void PopUp()
    {
        transform.position = _startPos;
        _message.color = _colorOrigin;
        gameObject.SetActive(true);
    }

    public void PopUp(string newMessage)
    {
        _message.text = newMessage;
        PopUp();
    }

    private void Awake()
    {
        _message = GetComponent<TMP_Text>();
        _colorOrigin = _message.color;
    }

    private void Update()
    {
        transform.Translate(_direction * _moveSpeed * Time.deltaTime);

        float a = _message.color.a - _fadeSpeed * Time.deltaTime;

        if (a > 0.0f)
        {
            _message.color = new Color(_colorOrigin.r, _colorOrigin.g, _colorOrigin.b, a);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
