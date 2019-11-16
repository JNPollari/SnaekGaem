using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;
    private GameHandler gamehandler;
    
    [SerializeField]
    private GameObject snack;
    
    private float dir = 0;
    private int turnDirection = 1;

    private IEnumerator tailroutine;
    private float tailDelayTime = 0.05f;

    private Tail tail;
    private int headturn = 0;
    private int tailturn = 0;
        
    [SerializeField]
    private float speed;
    [SerializeField]
    private float turnSpeed;

    private int score = 0;
    private int tails = 4;

    private List<State> states;


    internal void SetGameHandler(GameHandler gh)
    {
        gamehandler = gh;
    }

    // Start is called before the first frame update
    void Start()
    {
        states = new List<State>();
        // tail = Instantiate(tailprefab, transform.position, transform.rotation);
        // tail.Initialize(states, 5);
        // for (int i=0; i < tails - 1; i++) {
        //     tail.Spawntail();
        // }
        
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
            tail.Initialize(states, 5);
        } else {
            tail.Spawntail();
        }
    }


}
