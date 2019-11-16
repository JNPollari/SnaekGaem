using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;
    private GameHandler gameHandler;
    
    [SerializeField]
    private GameObject snack;
    
    private float dir = 0;
    private int turnDirection = 1;

    private Tail tail;
        
    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnSpeed;

    private int score = 0;
    private int tails = 0;
    private int initialTails = 4;
    private int offset;
    private int startStates = 0;

    private List<State> states;


    internal void SetGameHandler(GameHandler gh)
    {
        gameHandler = gh;
    }

    void Start()
    {
        StartCoroutine("TailSpawn");
    }

    IEnumerator TailSpawn() {
        for (int i=0; i < initialTails; i++) {
            yield return new WaitForSeconds(0.2f);
            growTail();
        }
    }

    void FixedUpdate() { 
        states.Insert(0, new State(transform.position, transform.rotation));
        Debug.Log(states);

        // tails--;
        // if (tails%10 == 0 && tails > 0) {
        //     growTail();
        // }
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0) turnDirection = -1;
        if (Input.GetAxis("Horizontal") > 0) turnDirection = 1;

        dir -= turnDirection;
        
        transform.eulerAngles = new Vector3(0, 0, turnSpeed * dir);
        transform.Translate(speed, speed, 0);
    }

    internal void growTail() {
        if (tail == null) {
            tail = Instantiate(tailprefab, transform.position, transform.rotation);
            tail.Initialize(states, offset);
        } else {
            tail.Spawntail();
        }
        tails++;
    }

    internal void Initialize(GameHandler _gameHandler, List<State> _states, int _offset) {
        gameHandler = _gameHandler;
        states = _states;
        offset = _offset;
    }

    internal int GetTails() {
        return tails;
    }
}
