using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private Head snakehead;

    [SerializeField]
    private ReverseHead reverseSnake;

    [SerializeField]
    private Snack snackprefab;    

    private List<State> states;

    private int score = 0;
    [SerializeField]
    private int offset = 5; // Offset between tail pieces.

    private float sceneWidth = 38;
    private float sceneHeight = 16;

    // Start is called before the first frame update
    void Start()
    {
        states = new List<State>();
        snakehead = Instantiate(snakehead, transform.position, Quaternion.identity);
        snakehead.Initialize(this, states, offset);
        StartCoroutine("FirstSnack");
    }

    IEnumerator FirstSnack() {
        yield return new WaitForSeconds(5);
        createSnack();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void incrementScore(int inc) {
        score += inc;
    }

    internal void createSnack() {
        Vector3 tailpos = snakehead.GetTailpos();
        float distance = 0;
        Vector3 locat = new Vector3(0, 0, 0);

        while (distance < 20)
        {
            locat = new Vector3(UnityEngine.Random.Range(-sceneWidth, sceneWidth),
                UnityEngine.Random.Range(-sceneHeight, sceneHeight),
                0);

            distance = Mathf.Abs(tailpos.x - locat.x) + Mathf.Abs(tailpos.y - locat.y);
        }

        Snack _snack = Instantiate(
            snackprefab, locat, new Quaternion(0, 0, 0, 0));
        _snack.Initialize(this, states[0], snakehead.GetTails());

    }

    internal void createReverseSnake(State state, int stateCount, int tails) {
        ReverseHead _reverseSnake = Instantiate(reverseSnake, state.GetPosition(), state.GetRotation());
        _reverseSnake.Initialize(states, state, stateCount + 5 * snakehead.GetTails(), offset, tails);
    }
}
