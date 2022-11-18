using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongChecker : MonoBehaviour {
    [SerializeField] private SongSO[] songs;
    [SerializeField] private AudioSource source;

    private List<SongSO> possibleSongs = new List<SongSO>();

    private int noteIndex;

    public float GetSongLength
    {
        get { return songs[0].song.length; }
    }

    public string PlaySong()
    {
        StartCoroutine(WaitToPlaySong());
        return songs[0].command;
    }

    IEnumerator WaitToPlaySong()
    {
        source.clip = songs[0].song;
        yield return new WaitForSeconds(1f);
        source.Play();
    }
    public bool SongCheck(FluteNote note)
    {
        print("possible songs: " + possibleSongs.Count);
        if (possibleSongs.Count == 0) return false;
        return RemoveWrongSongs(note);
    }

    public void Reset()
    {
        possibleSongs.Clear();
        for(int i=0;i<songs.Length;++i)
        {
            possibleSongs.Add(songs[i]);
        }

        noteIndex = 0;
    }

    bool RemoveWrongSongs(FluteNote note)
    {
        if (possibleSongs.Count == 0) return false;
        for(int i=0;i<possibleSongs.Count;++i)
        {
            print("note:" +note + " | checkedNote: " + possibleSongs[i].notes[noteIndex]);
            if (noteIndex >= possibleSongs[i].notes.Length|| possibleSongs[i].notes[noteIndex] != note)
            {
                possibleSongs.Remove(possibleSongs[i]);
            }
        }

        ++noteIndex;
        // print("notes: " + possibleSongs[0].notes.Length + noteIndex);
        return possibleSongs.Count == 1 && (noteIndex == possibleSongs[0].notes.Length);
    }
}
