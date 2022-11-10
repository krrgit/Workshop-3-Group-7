using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FluteNote {
    loD,
    F,
    A,
    B,
    hiD
}


public class NotePlacer : MonoBehaviour {
    [SerializeField] private float lineDist = 0.4f;
    [SerializeField] private RectTransform rect;
    [SerializeField] private RectTransform[] notes;


    private int maxNotes;

    private int setNotes = 0;
    // Start is called before the first frame update
    void Start()
    {
        maxNotes = transform.childCount;
        notes = new RectTransform[maxNotes];
        for (int i = 0; i < maxNotes; ++i)
        {
            notes[i] = transform.GetChild(i).GetComponent<RectTransform>();
            notes[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DebugInput();
    }

    void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetNextNote(FluteNote.loD);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetNextNote(FluteNote.F);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetNextNote(FluteNote.A);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetNextNote(FluteNote.B);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetNextNote(FluteNote.hiD);
        }
        
    }

    void Reset()
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
            case FluteNote.loD:
                ypos = 0;
                break;
            case FluteNote.F:
                ypos = 2;
                break;
            case FluteNote.A:
                ypos = 4;
                break;
            case FluteNote.B:
                 ypos = 5;
                 break;
            case FluteNote.hiD:
                ypos = 7;
                break;
            default:
                break;
        }

        ypos *= lineDist;

        return ypos;
    }
}
