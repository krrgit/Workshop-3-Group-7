using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpdateButtonLabels : MonoBehaviour {
    public static UpdateButtonLabels Instance;
    private string[] labels;

    public void UpdateLabels(string label)
    {
        int index = GetLabelIndex(label);

        for (int i = 1; i < labels.Length; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        if (index == -1) return;
        
        transform.GetChild(index).gameObject.SetActive(true);
    }

    int GetLabelIndex(string label)
    {
        for (int i = 1; i < labels.Length; ++i)
        {
            if (labels[i] == label)
                return i;
        }

        return -1;
    }
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        labels = new string[transform.childCount];
        for (int i = 0; i < labels.Length; ++i)
        {
            labels[i] = transform.GetChild(i).name;
        }
    }

}
