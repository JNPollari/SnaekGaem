using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseHead : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;

    private float dir = 00f;
    private Tail tail;
    private List<State> states;
    private int offset;
    private State currentState;
    private int stateCount;

    // Initialize should be calles as the tailpiece is first created
    public void Initialize(List<State> _states, State _initialState, int _stateCount, int _offset)
    {
        currentState = _initialState;
        states = _states;
        stateCount = _stateCount;
        offset = _offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState = states[stateCount];
        stateCount += 2;
        transform.position = currentState.GetPosition();
        transform.rotation = currentState.GetRotation();
    }


    internal void Spawntail()
    {
        if (tail == null)
        {
            tail = Instantiate(tailprefab, transform.position, transform.rotation);
            tail.Initialize(states, offset + 5);
        }
        else
        {
            tail.Spawntail();
        }
    }
}
