﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseHead : MonoBehaviour
{
    [SerializeField]
    private ReverseTail tailprefab;

    private float dir = 00f;
    private ReverseTail tail;
    private List<State> states;
    private int offset;
    private State currentState;
    private int stateCount;
    private int lifeTime;
    private int tails;

    // Initialize should be calles as the tailpiece is first created
    public void Initialize(List<State> _states, State _initialState, int _stateCount, int _offset, int _tails)
    {
        currentState = _initialState;
        states = _states;
        stateCount = _stateCount;
        offset = _offset;
        lifeTime = _stateCount;
        tails = _tails;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        currentState = states[stateCount];
        stateCount += 2;
        lifeTime--;
        transform.position = currentState.GetPosition();
        transform.rotation = currentState.GetRotation();
        if (lifeTime == 0) {
            Destroy(gameObject);
        }
        if (tails > 0) {
            Spawntail();
        }
        tails--;
    }

    internal void Spawntail()
    {
        if (tail == null)
        {
            tail = Instantiate(tailprefab, transform.position, transform.rotation);
            tail.Initialize(states, stateCount - 5, lifeTime);
        }
        else
        {
            tail.Spawntail();
        }
    }
}
