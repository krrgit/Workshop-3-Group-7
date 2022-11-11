using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChecker : MonoBehaviour {
    [SerializeField] private SongSO[] songs;

    private List<SongSO> possibleSongs = new List<SongSO>();
    
    public bool SongCheck(FluteNote note, int noteIndex)
    {
        if (possibleSongs.Count == 0) return false;
        return RemoveWrongSongs(note, noteIndex);
    }

    public void Reset()
    {
        possibleSongs.Clear();
        for(int i=0;i<songs.Length;++i)
        {
            possibleSongs.Add(songs[i]);
        }
    }

    bool RemoveWrongSongs(FluteNote note, int noteIndex)
    {
        if (possibleSongs.Count == 0) return false;
        for(int i=0;i<possibleSongs.Count;++i)
        {
            if (possibleSongs[i].notes[noteIndex] != note)
            {
                possibleSongs.Remove(possibleSongs[i]);
            }
        }

        return possibleSongs.Count == 1;
    }
}
