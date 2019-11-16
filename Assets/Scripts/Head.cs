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
    private int tails = 4;
    private int offset;

    private List<State> states;


    internal void SetGameHandler(GameHandler gh)
    {
        gameHandler = gh;
    }

    void Start()
    {

    }

    void FixedUpdate() { 
        states.Insert(0, new State(transform.position, transform.rotation));
        Debug.Log(states);
    }

    
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0) turnDirection = -1;
        if (Input.GetAxis("Horizontal") > 0) turnDirection = 1;

        if (Input.GetButtonDown("Jump"))
        {
            growTail();
        }

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
    }

    internal void Initialize(GameHandler _gameHandler, List<State> _states, int _offset) {
        gameHandler = _gameHandler;
        states = _states;
        offset = _offset;
    }

}
