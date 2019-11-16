﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;
    private GameHandler gamehandler;

    private float dir = 0f;
    private Vector3 pytdir3;
    private int turnrate = 1;

    private IEnumerator tailroutine;
    private float tailDelayTime = 0.15f;

    private Tail tail;
    private int headturn = 0;
    private int tailturn = 0;
        
    [SerializeField]
    private float speed = 30;

    internal void SetGameHandler(GameHandler gh)
    {
        gamehandler = gh;
    }

    // Start is called before the first frame update
    void Start()
    {
        pytdir3 = new Vector3(dir, dir, 0);
    }

    
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0) turnrate = -1;
        if (Input.GetAxis("Horizontal") > 0) turnrate = 1;

        if (Input.GetButtonDown("Jump"))
        {
            Spawntail();
        }

        Debug.Log(turnrate + " = turnrate, " + dir + " = dir");

        dir -= turnrate;

        if (turnrate != headturn)
        {
            if (gamehandler != null) gamehandler.CreateTimestamp(turnrate);
            headturn = turnrate;
            tailroutine = Taildelay(turnrate);
            StartCoroutine(tailroutine);
        }

        if (tail != null)
        {
            tail.Turn(tailturn);
        }
        
        transform.eulerAngles = new Vector3(0, 0, 2 * dir);

        pytdir3.x = dir;
        pytdir3.y = dir;
        if (dir != 0) transform.Translate(pytdir3 * Time.deltaTime * speed * speed / dir);
        
        
    }

    internal Quaternion GetRotation()
    {
        return transform.rotation;
    }

    internal Vector3 GetPosition()
    {
        return transform.position;
    }

    IEnumerator Taildelay(int axis)
    {
        yield return new WaitForSeconds(tailDelayTime);
        tailturn = axis;

    }

    private void Spawntail()
    {
        if (tail == null)
        {
            tail = Instantiate(tailprefab, transform.position, transform.rotation);
            tail.Initialize(dir, speed, tailDelayTime);
        } else
        {
            tail.Spawntail();
        }
    }




}
