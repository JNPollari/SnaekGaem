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
    private int turnrate = 1;

    private IEnumerator tailroutine;
    private float tailDelayTime = 0.15f;

    private Tail tail;
    private int headturn = 0;
    private int tailturn = 0;
        
    [SerializeField]
    private float speed = 2;

    private int score = 0;


    internal void SetGameHandler(GameHandler gh)
    {
        gamehandler = gh;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0) turnrate = -1;
        if (Input.GetAxis("Horizontal") > 0) turnrate = 1;

        if (Input.GetButtonDown("Jump"))
        {
            Spawntail();
        }

        dir -= turnrate;
        

        if (turnrate != headturn)
        {
            if (gamehandler != null) gamehandler.CreateTimestamp(turnrate);
            headturn = turnrate;
            tailroutine = Taildelay(turnrate);
            StartCoroutine(tailroutine);
        }

        if (tail != null)
        {
            tail.Turn(tailturn);
        }
        
        transform.eulerAngles = new Vector3(0, 0, 2 * dir);
        transform.Translate(0.1f, 0.1f, 0);


    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Snack") {
            score += 1;
            Instantiate(collision.gameObject, new Vector3(
                UnityEngine.Random.Range(-10, 10),
                UnityEngine.Random.Range(-10, 10),
                0),
                new Quaternion(0, 0, 0, 0));
            Destroy(collision.gameObject);
            Debug.Log("Snack eaten.");

            /*
            Instantiate(snack, new Vector3(
                UnityEngine.Random.Range(-10, 10),
                UnityEngine.Random.Range(-10, 10),
                0), new Quaternion(0, 0, 0, 0));
            */
        }
    }

    internal Quaternion GetRotation()
    {
        return transform.rotation;
    }

    internal Vector3 GetPosition()
    {
        return transform.position;
    }

    IEnumerator Taildelay(int axis)
    {
        yield return new WaitForSeconds(tailDelayTime);
        tailturn = axis;

    }

    private void Spawntail()
    {
        if (tail == null)
        {
            tail = Instantiate(tailprefab, transform.position, transform.rotation);
            tail.Initialize(dir, speed, tailDelayTime);
        } else
        {
            tail.Spawntail();
        }
    }




}
