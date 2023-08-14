using System;
using System.Collections.Generic;

[Serializable]
public class SongData
{
    public string name;
    public List<NoteData> noteDatum = new List<NoteData>();
}