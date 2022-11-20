using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlacer : MonoBehaviour {
    [SerializeField] private float distance = 1.5f;
    [SerializeField]private Vector2 top;
    [SerializeField]private Vector2 down;
    [SerializeField]private Vector2 left;
    [SerializeField]private Vector2 right;

    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private SpriteRenderer[] renderers;

    private int lastIndex;

    public static HandPlacer Instance;

    private void Awake()
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

    private void LateUpdate()
    {
        Place(PlayerMovement.Instance.FacingDir);
    }

    void Place(Vector2 dir)
    {
        lastIndex = GetVectorIndex(dir);
        SetLayer();
        if (ToolManager.Instance.CurrentTool == ToolInUse.Flute)
        {
            PlaceCenter(dir);
            return;
        }

        switch (lastIndex)
        {
            case 0:
                transform.localPosition = top;
                break;
            case 1:
                transform.localPosition = right;
                break;
            case 2:
                transform.localPosition = down;
                break;
            case 3:
                transform.localPosition = left;
                break;
            default:
                break;
        }
    }

    void PlaceCenter(Vector2 dir)
    {
        transform.localPosition = dir * distance;
    }

    int GetVectorIndex(Vector2 dir)
    {
        // 0: up, 1: right, 2: down, 3: left
        if (dir.y > 0)
        {
            return 0;
        } else if (dir.y < 0)
        {
            return 2;
        }
        else
        {
            if (dir.x > 0)
            {
                return 1;
            } else if (dir.x < 0)
            {
                return 3;
            }else
            {
                return lastIndex;
            }
        }
    }

    void SetLayer()
    {
        if (lastIndex == 1 || lastIndex == 2)
        {
            for (int i = 0; i < renderers.Length; ++i)
            {
                renderers[i].sortingOrder = playerSprite.sortingOrder + 1;
            }
        }
        else
        {
            for (int i = 0; i < renderers.Length; ++i)
            {
                renderers[i].sortingOrder = playerSprite.sortingOrder - 1;
            }
        }
    }
}
