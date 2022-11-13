using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FluteTool : Tool {
    [SerializeField] private AnimateMusicUI musicUI;
    [SerializeField] private NotePlacer notePlacer;
    [SerializeField] private SongChecker checker;
    [SerializeField] private NotePlayer player;
    [SerializeField] private bool inUse;
    [SerializeField] private int maxNotes = 8;
<<<<<<< Updated upstream
=======
    [SerializeField] private UnityEvent ControlCatEvent;
    
>>>>>>> Stashed changes
    private int currentNote;

    private bool isPlayable;

    private bool playSong;

    // Update is called once per frame
    void Update()
    {
        DebugInput();
    }

    void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayNote(FluteNote.loC);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayNote(FluteNote.E);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayNote(FluteNote.G);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayNote(FluteNote.A);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayNote(FluteNote.hiC);
        }
        
    }


    void PlayNote(FluteNote note)
    {
        if (!isPlayable) return;
        if (currentNote != maxNotes)
        {
            notePlacer.SetNextNote(note);
            playSong = checker.SongCheck(note);
            player.PlayNote(note);
        }
        currentNote++;

        if (playSong)
        {
            PlaySong();
        }

        if (!playSong && currentNote == maxNotes)
        {
            Reset(1);
        }
    }

    void PlaySong()
    {
        print("Play song!");
        checker.PlaySong();
<<<<<<< Updated upstream
        Reset(checker.GetSongLength);
=======
        StartCoroutine(HoldOnReset(checker.GetSongLength));
        StartCoroutine(WaitForFluteEvent(checker.GetSongLength));
>>>>>>> Stashed changes
        player.DelayStopAllNotes(1f);
    }

    void Reset(float waitTime)
    {
        StartCoroutine(HoldOnReset(waitTime));
    }

    IEnumerator HoldOnReset(float waitTime)
    {
        isPlayable = false;
        yield return new WaitForSeconds(waitTime);
        notePlacer.Reset();
        checker.Reset();
        currentNote = 0;
        player.Reset();
        isPlayable = true;
    }

    IEnumerator StartWait()
    {
        yield return new WaitForSeconds(1);
        currentNote = 0;
        isPlayable = true;
    }
<<<<<<< Updated upstream
    public override void Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        musicUI.Animate(true);
        StartCoroutine(StartWait());
        StartCoroutine(HoldOnReset(0));
        print("Use Flute!");
    }
    
    public override void Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        musicUI.Animate(false);
        player.Reset();
        print("Stop using Flute");
        isPlayable = false;
=======

    IEnumerator WaitForFluteEvent(float waitTime)
    {        
        yield return new WaitForSeconds(waitTime);
        ControlCatEvent.Invoke();
>>>>>>> Stashed changes
    }
}
