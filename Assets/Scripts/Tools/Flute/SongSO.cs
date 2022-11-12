using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "ScriptableObjects/Song", order = 1)]
public class SongSO : ScriptableObject {
    public FluteNote[] notes;
    public AudioClip song;
}
