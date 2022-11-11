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
            playSong = checker.SongCheck(note, currentNote);
            player.PlayNote(note);
        }

        if (playSong)
        {
            // play song
        }
        ++currentNote;
        
        if (!playSong && currentNote == maxNotes)
        {
            Reset();
        }
    }

    void Reset()
    {
        StartCoroutine(HoldOnReset());
    }

    IEnumerator HoldOnReset()
    {
        isPlayable = false;
        yield return new WaitForSeconds(1);
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
    public override void Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        musicUI.Animate(true);
        StartCoroutine(StartWait());
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
}
