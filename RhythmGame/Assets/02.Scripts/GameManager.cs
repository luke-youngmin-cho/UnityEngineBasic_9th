using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (instance != null) // �̹� GameManager �� �����ϸ�
        {
            Destroy(gameObject); // ���� ����ִ� �ı�
            return;
        }

        instance = this; // GameManager ������ �̱��� ���
        DontDestroyOnLoad(gameObject); // �� ��ȯ�ǵ� �ı��ȵǰ�
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.LoadSongData:
                break;
            case State.WaitUntilSongDataLoaded:
                break;
            case State.StartPlay:
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
