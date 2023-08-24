using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _coolCount;
    [SerializeField] private TMP_Text _greatCount;
    [SerializeField] private TMP_Text _goodCount;
    [SerializeField] private TMP_Text _missCount;
    [SerializeField] private TMP_Text _badCount;
    [SerializeField] private TMP_Text _rankCount;
    [SerializeField] private Button _lobby;
    [SerializeField] private Button _replay;

    private void OnEnable()
    {
        MusicPlayManager musicPlayManager = MusicPlayManager.instance;
        _score.text = musicPlayManager.score.ToString();
        _coolCount.text = musicPlayManager.coolCount.ToString();
        _greatCount.text = musicPlayManager.greatCount.ToString();
        _goodCount.text = musicPlayManager.goodCount.ToString();
        _missCount.text = musicPlayManager.missCount.ToString();
        _badCount.text = musicPlayManager.badCount.ToString();
        _rankCount.text = musicPlayManager.rank;
    }

    private void Awake()
    {
        _lobby.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SelectSong");
            GameManager.instance.state = GameManager.State.Idle;
        });
        _replay.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            GameManager.instance.state = GameManager.State.StartPlay;
        });
    }
}
