using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongSelectionUI : MonoBehaviour
{
    public static string s_selected;

    [SerializeField] private TMP_Text _title;
    [SerializeField] private Button _startPlay;

    private void Awake()
    {
        Select(string.Empty);
    }

    private void Start()
    {
        s_selected = string.Empty;
        _startPlay.onClick.AddListener(() => GameManager.instance.state = GameManager.State.LoadSongData);
    }

    public void Select(string selected)
    {
        s_selected = selected;
        if (string.IsNullOrEmpty(selected))
        {
            _title.text = string.Empty;
            _startPlay.interactable = false;
        }
        else
        {
            _title.text = selected;
            _startPlay.interactable = true;
        }
    }
}
