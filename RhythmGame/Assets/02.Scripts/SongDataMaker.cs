using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SongDataMaker : MonoBehaviour
{
    public SongData songData;
    public VideoPlayer videoPlayer;
    private KeyCode[] _keys = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.Space, KeyCode.J, KeyCode.K, KeyCode.L };
    private bool _isRecording;

    public void StartRecord()
    {
        if (_isRecording)
            return;

        _isRecording = true;
        songData = new SongData();
        songData.name = videoPlayer.clip.name;
        videoPlayer.Play();
    }

    public void StopRecord()
    {
        if (_isRecording == false)
            return;

        videoPlayer.Stop();
        SaveRecord();
    }

    private void SaveRecord()
    {
        string dir = UnityEditor.EditorUtility.SaveFilePanelInProject("Save Song Data", songData.name, "json", "");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(songData));
    }

    private void Update()
    {
        if (_isRecording == false)
            return;

        for (int i = 0; i < _keys.Length; i++)
        {
            if (Input.GetKeyDown(_keys[i]))
            {
                songData.noteDatum.Add(CreateNoteData(_keys[i]));
            }
        }
    }

    public NoteData CreateNoteData(KeyCode key)
    {
        NoteData noteData = new NoteData()
        {
            key = key,
            time = (float)System.Math.Round(videoPlayer.time, 2)
        };
        Debug.Log($"[SongDataMaker] : Recorded {key}, {noteData.time}");
        return noteData;
    }
}
