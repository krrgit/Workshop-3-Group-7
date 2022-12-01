using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioSource[] sources;
    public static SoundManager Instance;
    [SerializeField] private AudioSource blacklightclick;
    [SerializeField] private AudioSource usingWrench;
    [SerializeField] private AudioSource switchTools;
    [SerializeField] private AudioSource openDoor;
    void Awake()
    {
        // This only allows one instance of GameStateManager to exist in any scene
        // This is to avoid the need for GetComponent Calls. Use GameStateManager.Instance instead.
        if (Instance == null) {
            Instance = this;
        }else {
            Destroy(this);
        }
    }

    void Start()
    {
        GetSources();
    }

    void GetSources()
    {
        int size = transform.childCount;
        sources = new AudioSource[size];
        for (int i = 0; i < size; ++i)
        {
            sources[i] = transform.GetChild(i).GetComponent<AudioSource>();
            
            if (sources[i].clip) sources[i].gameObject.name = sources[i].clip.name;
        }
    }

    public void Play(string name)
    {
        int sfx = FindAudio(name);
        if (sfx == -1) return;
        
        sources[sfx].Play();
    }

    int FindAudio(string name)
    {
        int audioIndex = -1;
        for (int i = 0; i < sources.Length; ++i)
        {
            if (sources[i].gameObject.name == name)
            {
                audioIndex = i;
                break;
            }
        }

        return audioIndex;
    }

    public void PlaytoggleBlacklight()
    {
        blacklightclick.Play();
    }

    public void PlayusingWrench()
    {
        usingWrench.Play();
    }

    public void PlayswitchTools()
    {
        switchTools.Play();
    }

    public void PlayopenDoor()
    {
        openDoor.Play();
    }
}
