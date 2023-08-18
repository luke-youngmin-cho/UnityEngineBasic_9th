using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public enum State
    {
        Idle,
        LoadSongData,
        WaitUntilSongDataLoaded,
        StartPlay,
        WaitUntilPlayFinished,
        DisplayScore,
        WaitForUser,
    }
    public State state;

    private void Awake()
    {
        if (instance != null) // ÀÌ¹Ì GameManager °¡ Á¸ÀçÇÏ¸é
        {
            Destroy(gameObject); // »õ·Î ´«¶á¾Ö´Â ÆÄ±«
            return;
        }

        instance = this; // GameManager ¾øÀ¸¸é ½Ì±ÛÅæ µî·Ï
        DontDestroyOnLoad(gameObject); // ¾À ÀüÈ¯µÇµµ ÆÄ±«¾ÈµÇ°Ô
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.LoadSongData:
                {
                    SongDataLoader.Load(SongSelectionUI.s_selected);
                    state = State.WaitUntilSongDataLoaded;
                }
                break;
            case State.WaitUntilSongDataLoaded:
                {
                    if (SongDataLoader.isLoaded)
                    {
                        SceneManager.LoadScene("MusicPlay");
                        state = State.StartPlay;
                    }
                }
                break;
            case State.StartPlay:
                {
                    if (MusicPlayManager.instance != null)
                    {
                        MusicPlayManager.instance.StartMusicPlay();
                        state = State.WaitUntilPlayFinished;
                    }
                }
                break;
            case State.WaitUntilPlayFinished:
                break;
            case State.DisplayScore:
                break;
            case State.WaitForUser:
                break;
            default:
                break;
        }
    }
}
