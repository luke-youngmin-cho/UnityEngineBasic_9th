using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public static class SongDataLoader
{
    public static bool isLoaded { get; private set; }

    public static SongData dataLoaded;
    public static VideoClip clipLoaded;

    public static void Load(string songName)
    {
        isLoaded = false;
        dataLoaded = null;
        clipLoaded = null;

        dataLoaded = JsonUtility.FromJson<SongData>(Resources.Load<TextAsset>($"SongDatum/{songName}").ToString());
        clipLoaded = Resources.Load<VideoClip>($"SongClips/{songName}");
        isLoaded = true;
    }
}
