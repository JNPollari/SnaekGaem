using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastHead : MonoBehaviour
{
    private float birthTime;
    private float actStartTime;
    private float actiontime;

    [SerializeField]
    private Tail tailprefab;
    private GameHandler gamehandler;

    private float dir = 0f;
    private Vector3 pytdir3;
    private int turnrate = 1;

    private IEnumerator tailroutine;
    private float tailDelayTime = 0.15f;

    private Tail tail;
    private int headturn = 0;
    private int tailturn = 0;

    [SerializeField]
    private float speed = 0.03f;


    /// <summary>
    /// Set all initial variables upon creation
    /// </summary>
    /// <param name="gh">reference to the gamehandler</param>
    /// <param name="time">time, which to spawn</param>
    internal void Initialize(GameHandler gh, float time)
    {
        gamehandler = gh;
        birthTime = time;
        actStartTime = Time.time;
        turnrate = gh.Getdirection(birthTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        pytdir3 = new Vector3(dir, dir, 0);
    }


    void Update()
    {
        actiontime = actStartTime - Time.time;
        turnrate = gamehandler.Getdirection(actiontime);
        Debug.Log(turnrate + " = turnrate, " + dir + " = dir" );

        dir -= turnrate;

        if (turnrate != headturn)
        {
            headturn = turnrate;
            tailroutine = Taildelay(turnrate);
            StartCoroutine(tailroutine);
        }

        if (tail != null)
        {
            tail.Turn(tailturn);
        }

        transform.eulerAngles = new Vector3(0, 0, 2 * dir);

        pytdir3.x = dir;
        pytdir3.y = dir;
        if (dir != 0) transform.Translate(pytdir3 * Time.deltaTime * speed / dir);


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
        }
        else
        {
            tail.Spawntail();
        }
    }




}
