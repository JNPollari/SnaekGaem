using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;

    private float dir = 00f;
    private Tail tail;
    private List<State> states;
    private int offset;
    private State currentState;

    internal void SetSprite(TailSprite tailSprite)
    {
        tailSprite.FadeIn(offset);
    }

    // Initialize should be calles as the tailpiece is first created
    public void Initialize(List<State> _states, int _offset)
    {
        states = _states;
        offset = _offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState = states[offset];
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
