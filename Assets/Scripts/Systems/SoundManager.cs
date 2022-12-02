using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioSource[] sources;
    public static SoundManager Instance;
    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource correctSong;
    [SerializeField] private AudioSource toggleBlacklight;
    [SerializeField] private AudioSource sparkling;
    [SerializeField] private AudioSource castHook;
    [SerializeField] private AudioSource switchTool;
    [SerializeField] private AudioSource pullHook;
    [SerializeField] private AudioSource useWrench;
    [SerializeField] private AudioSource frogSong;
    [SerializeField] private AudioSource puzzleSolved;
    [SerializeField] private AudioSource wrongPuzzle;
    [SerializeField] private AudioSource foundSomething;
    [SerializeField] private AudioSource waterSplash;
    [SerializeField] private AudioSource movingCart;
    
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

    public void PlaydoorOpen()
    {
        doorOpen.Play();
    }

    public void PlaycorrectSong()
    {
        correctSong.Play();
    }

    public void PlaytoggleBlacklight()
    {
        toggleBlacklight.Play();
    }

    public void PlaySparkling()
    {
        sparkling.Play();
    }

    public void PlaycastHook()
    {
        castHook.Play();
    }

    public void PlayswitchTool()
    {
        switchTool.Play();
    }

    public void PlaypullHook()
    {
        pullHook.Play();
    }

    public void PlayuseWrench()
    {
        useWrench.Play();
    }

    public void PlayfrogSong()
    {
        frogSong.Play();
    }

    public void PlaypuzzleSolved()
    {
        puzzleSolved.Play();
    }

    public void PlaywrongPuzzle()
    {
        wrongPuzzle.Play();
    }

    public void PlayfoundSomething()
    {
        foundSomething.Play();
    }

    public void PlaywaterSplash()
    {
        waterSplash.Play();
    }

    public void PlaymovingCart()
    {
        movingCart.Play();
    }

    public void StoppullHook()
    {
        pullHook.Stop();
    }

    public void StopmovingCart()
    {
        movingCart.Stop();
    }
}
