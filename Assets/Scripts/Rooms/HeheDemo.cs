using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeheDemo : MonoBehaviour {
    private bool isPlaying;
    public void PlaySong()
    {
        if (isPlaying) return;
        isPlaying = true;
        SoundManager.Instance.Playhehe();
    }
}
