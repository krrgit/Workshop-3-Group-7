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
    [SerializeField] private FluteCommands commands;
    [SerializeField] private bool inUse;
    [SerializeField] private int maxNotes = 8;
    [SerializeField] private UnityEvent ControlCatEvent;
    
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
            PlayNote(FluteNote.E);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayNote(FluteNote.G);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayNote(FluteNote.A);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayNote(FluteNote.hiC);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            PlayNote(FluteNote.loC);
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
            StartCoroutine(WaitToReset(1));
        }
    }

    void PlaySong()
    {
        print("Play song!");
        string command = checker.PlaySong();
        StartCoroutine(IEndSong(checker.GetSongLength, command));
        player.DelayStopAllNotes(1f);
        
        ControlCatEvent.Invoke();
        
    }
    
    IEnumerator IEndSong(float waitTime, string command)
    {
        isPlayable = false;
        yield return new WaitForSeconds(waitTime);
        Reset();
        ToolManager.Instance.SetInUse(false);
        Stop();
        
        commands.DoCommand(command);
        musicUI.Animate(false);
        isPlayable = false;
    }

    IEnumerator WaitToReset(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Reset();
    }

    void Reset()
    {
        notePlacer.Reset();
        checker.Reset();
        currentNote = 0;
        player.Reset();
    }

    IEnumerator StartWait()
    {
        yield return new WaitForSeconds(1);
        currentNote = 0;
        isPlayable = true;
    }
    public override bool Use()
    {
        PlayerMovement.Instance.ToggleMove(false);
        musicUI.Animate(true);
        StartCoroutine(StartWait());
        Reset();
        UpdateButtonLabels.Instance.UpdateLabels("Flute");
        print("Use Flute!");
        return true;
    }

    public override bool Stop()
    {
        PlayerMovement.Instance.ToggleMove(true);
        musicUI.Animate(false);
        player.Reset();
        print("Stop using Flute");
        UpdateButtonLabels.Instance.UpdateLabels("Default");
        isPlayable = false;
        return false;
    }
}
