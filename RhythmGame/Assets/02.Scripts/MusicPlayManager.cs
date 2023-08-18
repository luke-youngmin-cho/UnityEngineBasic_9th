using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Linq; // Collection 의 다양한 질의(Query) 기능들을 포함하는 네임스페이스
using System;

[RequireComponent(typeof(VideoPlayer))]
public class MusicPlayManager : MonoBehaviour
{
    public static MusicPlayManager instance;

    public float noteFallingDistance => _spawnerCenter.position.y - _hitterCenter.position.y;
    public float noteFallingTime => noteFallingDistance / speedGain;

    public float speedGain = 1.0f;

    public bool isPlaying;
    private VideoPlayer _videoPlayer;
    private Queue<NoteData> _queue;
    private float _timeMark;

    private Dictionary<KeyCode, NoteSpawner> _spawners;
    [SerializeField] private List<NoteSpawner> _spawnerList;
    [SerializeField] private Transform _spawnerCenter;
    [SerializeField] private Transform _hitterCenter;


    private void Awake()
    {
        instance = this;
        _videoPlayer = GetComponent<VideoPlayer>();
        _spawners = new Dictionary<KeyCode, NoteSpawner>();
        foreach (var spawner in _spawnerList)
        {
            _spawners.Add(spawner.key, spawner);
        }
    }

    public void StartMusicPlay()
    {
        _queue = new Queue<NoteData>(SongDataLoader.dataLoaded.noteDatum.OrderBy(x => x.time));
        _videoPlayer.clip = SongDataLoader.clipLoaded;
        Invoke("PlayVideo", noteFallingTime);
        _timeMark = Time.time;
        isPlaying = true;
    }

    private void Update()
    {
        if (isPlaying == false)
            return;

        while (_queue.Count > 0)
        {
            if (_queue.Peek().time <= Time.time - _timeMark)
            {
                _spawners[_queue.Dequeue().key].Spawn();
            }
            else
            {
                break;
            }
        }
            

        if (_queue.Count <= 0)
        {
            isPlaying = false;
        }
    }

    private void PlayVideo()
    {
        _videoPlayer.Play();
    }
}
