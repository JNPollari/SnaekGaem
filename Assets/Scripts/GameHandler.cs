using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private Head snakehead;

    [SerializeField]
    private PastHead pastsnake;

    [SerializeField]
    private Snack snackprefab;    

    private List<State> states;


    // Start is called before the first frame update
    void Start()
    {
        states = new List<State>();

        snakehead = Instantiate(snakehead, transform.position, Quaternion.identity);
        //snakehead.Initialize(this, states);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
