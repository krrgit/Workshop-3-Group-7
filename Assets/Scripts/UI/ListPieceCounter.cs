using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListPieceCounter : MonoBehaviour
{

    public TextMeshProUGUI listPieceCount;
    
    public int currentListPiece; 
    void Start()
    {
        currentListPiece = ProgressTracker.Instance.FishCaught; 
        UpdateListCount();
    }

    public void UpdateListCount()
    {
        listPieceCount.text = ProgressTracker.Instance.FishCaught.ToString() + " / 4";
        
    }
}
