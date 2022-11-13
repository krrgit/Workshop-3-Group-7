using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluteTool : Tool {
    [SerializeField] private AnimateMusicUI musicUI;
    [SerializeField] private NotePlacer notePlacer;
    [SerializeField] private SongChecker checker;
    [SerializeField] private NotePlayer player;
    [SerializeField] private bool inUse;
    [SerializeField] private int maxNotes = 8;
    
    private int currentNote;
    private bool isPlayable;
    private bool playSong;
    private bool isResetting;

    public override bool CanUnequip()
    {
        return isPlayable;
    }
    
    public override void Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        musicUI.Animate(true);
        StartCoroutine(HoldOnReset(1));
        print("Use Flute!");
    }
    
    public override void Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        musicUI.Animate(false);
        player.Reset();
        print("Stop using Flute");
        isPlayable = false;
    }
    

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
        if (Input.GetKeyDown(KeyCode.X))
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
            StartCoroutine(HoldOnReset(1));
        }
    }

    void PlaySong()
    {
        print("Play song!");
        checker.PlaySong();
        StartCoroutine(HoldOnReset(checker.GetSongLength));
        player.DelayStopAllNotes(1f);
    }

    void Reset()
    {
        notePlacer.Reset();
        checker.Reset();
        currentNote = 0;
        player.Reset();
    }

    IEnumerator HoldOnReset(float waitTime)
    {
        isPlayable = false;
        yield return new WaitForSeconds(waitTime);
        Reset();
        isPlayable = true;
    }
}
