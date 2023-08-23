using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class ScoringText : MonoBehaviour
{
    public int score
    {
        get => _score;
        set
        {
            _after = value;
            _score = value;
            _delta = (_after - _before) / _duration;
        }
    }

    private int _score;
    private int _before;
    private int _after;
    private float _delta;
    private float _duration = 0.1f;
    private TMP_Text _scoreText;
    private StringBuilder _stringBuilder;

    private void Awake()
    {
        _scoreText = GetComponent<TMP_Text>();
        _scoreText.text = _score.ToString();
        _stringBuilder = new StringBuilder();
    }

    private void Update()
    {
        if (_before < _after)
        {
            _before += (int)(_delta * Time.deltaTime);

            if (_before > _after)
                _before = _after;

            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("{0:N0}", _before);
            _scoreText.text = _stringBuilder.ToString();
        }
    }
}
