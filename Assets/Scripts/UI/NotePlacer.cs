using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FluteNote {
    loC,
    E,
    G,
    A,
    hiC,
    None
}


public class NotePlacer : MonoBehaviour {
    [SerializeField] private float lineDist = 0.4f;
    [SerializeField] private RectTransform rect;
    [SerializeField] private RectTransform[] notes;
    
    private int maxNotes;

    private void OnDisable()
    {
        Reset();
    }

    private int setNotes = 0;
    // Start is called before the first frame update
    void Awake()
    {
        maxNotes = transform.childCount;
        notes = new RectTransform[maxNotes];
        for (int i = 0; i < maxNotes; ++i)
        {
            notes[i] = transform.GetChild(i).GetComponent<RectTransform>();
            notes[i].gameObject.SetActive(false);
        }
    }
    
    public void Reset()
    {
        for (int i = 0; i < maxNotes; ++i)
        {
            notes[i].gameObject.SetActive(false);
        }

        setNotes = 0;
    }
    
    public void SetNextNote(FluteNote note)
    {
        if (setNotes == maxNotes) return;
        var pos = notes[setNotes].anchoredPosition;
        pos.y = GetStaffPos(note);
        notes[setNotes].anchoredPosition = pos;
        notes[setNotes].gameObject.SetActive(true);

        ++setNotes;
    }

    float GetStaffPos(FluteNote note)
    {
        float ypos = 0;
        switch (note)
        {
            case FluteNote.loC:
                ypos = 0;
                break;
            case FluteNote.E:
                ypos = 2;
                break;
            case FluteNote.G:
                ypos = 4;
                break;
            case FluteNote.A:
                 ypos = 5;
                 break;
            case FluteNote.hiC:
                ypos = 7;
                break;
            default:
                break;
        }

        ypos *= lineDist;

        return ypos;
    }
}
