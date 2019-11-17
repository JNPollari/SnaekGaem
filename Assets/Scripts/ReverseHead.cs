using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReverseHead : MonoBehaviour
{
    [SerializeField]
    private ReverseTail tailprefab;
    private ReverseHeadSprite spriteHandler;
    
    private float dir = 00f;
    private ReverseTail tail;

    internal void SetSprite(ReverseHeadSprite _reverseHeadSprite)
    {
        spriteHandler = _reverseHeadSprite;
    }



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
        lifeTime = _stateCount + Mathf.RoundToInt(Time.time * 2);
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
            if (tail != null) tail.Fade();
            spriteHandler.FadeOut();
            Destroy(gameObject, 1);
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("menuscene");
        }
    }
}
