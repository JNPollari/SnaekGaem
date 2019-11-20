using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private Head snakehead = null;

    [SerializeField]
    private ReverseHead reverseSnake = null;

    [SerializeField]
    private Snack snackprefab = null;    

    private List<State> states;

    private float startTime = 0;

    private Text textField;

    internal void SetTextField(Text text)
    {
        textField = text;
    }

    private int snacksEaten = 0;
    private int score = 0;
    private int shadowSnakes = 0;
    [SerializeField]
    private int offset = 5; // Offset between tail pieces.

    private float sceneWidth = 30;
    private float sceneHeight = 16;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Application.targetFrameRate = 60;
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
        if (Input.GetButtonDown("Cancel")) SceneManager.LoadScene("menuscene");
    }
    
    internal void incrementScore() {
        snacksEaten++;
        score += 1 + shadowSnakes;
        //textField.text = "Score " + snacksEaten.ToString();
        textField.text = "Snacks " + snacksEaten + ", Score " + score.ToString();
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

    internal void DecrementShadows(bool delay)
    {
        if (delay)
        {
            StartCoroutine("ShadowDecrementator");
        } else shadowSnakes--;

    }

    IEnumerator ShadowDecrementator()
    {
        yield return new WaitForSeconds(2);
        shadowSnakes--;
    }

    internal void createReverseSnake(State state, int stateCount, int tails) {
        ReverseHead _reverseSnake = Instantiate(reverseSnake, state.GetPosition(), state.GetRotation());
        shadowSnakes++;
        _reverseSnake.Initialize(states, state, stateCount + 5 * snakehead.GetTails(), startTime, offset, tails, this);
    }
}
