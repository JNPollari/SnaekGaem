using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReverseHead : MonoBehaviour
{
    [SerializeField]
    private ReverseTail tailprefab = null;
    private ReverseHeadSprite spriteHandler;
    
    private ReverseTail tail;
    private bool notDestroyed = true;
    private GameHandler gh = null;

    internal void SetSprite(ReverseHeadSprite _reverseHeadSprite)
    {
        spriteHandler = _reverseHeadSprite;
    }


    private List<State> states;
    private int offset;
    private State currentState;
    private float startTime = 0;
    private int stateCount;
    private int lifeTime;
    private int tailsToSpawn;

    // Initialize should be calles as the tailpiece is first created
    public void Initialize(List<State> _states, State _initialState, int _stateCount, float _startTime, int _offset, int _tails, GameHandler _gh)
    {
        currentState = _initialState;
        states = _states;
        stateCount = _stateCount;
        offset = _offset;
        startTime = _startTime;
        lifeTime = _stateCount + Mathf.RoundToInt((Time.time - startTime) * 2);
        tailsToSpawn = _tails;
        gh = _gh;
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
            if (tail != null) tail.FadeAll();
            spriteHandler.FadeOut();
            if (notDestroyed)
            {
                notDestroyed = false;
                gh.DecrementShadows(true);
            }
            Destroy(gameObject, 2);
        }
        if (tailsToSpawn > 0) {
            Spawntail();
        }
        tailsToSpawn--;

        if (states.Count <= stateCount) DemolishAll();
    }

    internal void GainLife(int _stateCount)
    {
        lifeTime += _stateCount + Mathf.RoundToInt((Time.time - startTime) * 2);
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

    internal void DemolishAll()
    {
        if (notDestroyed)
        {
            notDestroyed = false;
            gh.DecrementShadows(false);
        }
        if (tail != null) tail.DemolishAll();
        Destroy(gameObject);
    }
}
