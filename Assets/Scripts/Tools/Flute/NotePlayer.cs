using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NotePlayer : MonoBehaviour {
    [SerializeField] private float fadeSpeed = 2;
    [SerializeField] private AudioSource loCSource;
    [SerializeField] private AudioSource eSource;
    [SerializeField] private AudioSource gSource;
    [SerializeField] private AudioSource aSource;
    [SerializeField] private AudioSource hiCSource;

    private Coroutine[] fadeCoroutines = new Coroutine[5];

    private FluteNote lastNote = FluteNote.None;
    private AudioSource currentSource;

    public void Reset()
    {
        StopAllNotes();
        lastNote = FluteNote.None;
    }

    void StopAllNotes()
    {
        StopAllCoroutines();
        for (int i = 0; i < 5; ++i)
        {
            StartCoroutine(FadeOut(GetSource((FluteNote)i)));
        }
    }

    public void PlayNote(FluteNote note)
    {
        FadeOutLastNote();
        currentSource = GetSource(note);
        if (fadeCoroutines[(int)note] != null) StopCoroutine(fadeCoroutines[(int)note]);
        StartCoroutine(FadeIn(currentSource));

        lastNote = note;
    }
    
    void StopLastNote()
    {
        if (lastNote == FluteNote.None) return;
        
        GetSource(lastNote).Stop();
    }
    

    public void FadeOutLastNote()
    {
        if (lastNote == FluteNote.None) return;

        fadeCoroutines[(int)lastNote] = StartCoroutine(FadeOut(GetSource(lastNote)));
    }

    AudioSource GetSource(FluteNote note)
    {
        switch (note)
        {
            case FluteNote.loC:
                return loCSource;
            case FluteNote.E:
                return eSource;
            case FluteNote.G:
                return gSource;
            case FluteNote.A:
                return aSource;
            case FluteNote.hiC:
                return hiCSource;
            default:
                break;
        }
        return null;
    }

    IEnumerator FadeIn(AudioSource source)
    {
        source.volume = 0.33f;
        source.Play();
        while (source.volume < 1)
        {
            source.volume += Time.deltaTime * fadeSpeed;
            yield return new WaitForEndOfFrame();
        }

        source.volume = 1;
    }

    IEnumerator FadeOut(AudioSource source)
    {
        while (source.volume > 0)
        {
            source.volume -= Time.deltaTime * fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        
        source.Stop();
        source.volume = 0;
    }
}
