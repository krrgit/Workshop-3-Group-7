using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minecart : MonoBehaviour {
    [SerializeField] private float speed = 1;
    [SerializeField] private Transform resetPos;
    [SerializeField] private bool isMoving;
    [SerializeField] private bool catInCart;
    [SerializeField] private GameObject startInteractor;

    public void Reset()
    {
        transform.position = resetPos.position;
        transform.up = resetPos.up;
        isMoving = false;
        startInteractor.SetActive(true);
    }

    public void StartCart()
    {
        if (isMoving) return;
        catInCart = true;
        isMoving = true;
        startInteractor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (!isMoving) return;
        transform.position += transform.up * speed * Time.deltaTime;
    }


    private void OnCollisionEnter2D(Collision2D col)
    {
        Reset();

    }
}
