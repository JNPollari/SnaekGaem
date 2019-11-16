using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;

    private float dir = 00f;
    private Vector3 pytdir3;
    private float speed;
    private bool active = false;
    private IEnumerator activationroutine;
    private float tailDelayTime = 0.5f;

    private IEnumerator tailroutine;
    private Tail tail;
    private int thisturn = 0;
    private int tailturn = 0;

    // Initialize should be calles as the tailpiece is first created
    public void Initialize(float direction, float movespeed, float delay)
    {

        dir = direction;
        speed = movespeed;
        tailDelayTime = delay;
        activationroutine = Startdelay(delay);
        StartCoroutine(activationroutine);


    }

    IEnumerator Startdelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        active = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (active)
        {
            transform.eulerAngles = new Vector3(0, 0, 2 * dir);
            transform.Translate(0.1f, 0.1f, 0.1f);

            if (tail != null)
            {
                tail.Turn(tailturn);
            }
        }
        
    }

    /// <summary>
    /// Turns tail similary as head is turned via buttons
    /// </summary>
    /// <param name="right">wether tail should turn right. False turns left</param>    
    internal void Turn(int direction)
    {
        if (active) dir -= direction;

        if (direction != thisturn) // set delaycoroutine for tailmovement
        {
            thisturn = direction;
            tailroutine = Taildelay(direction);
            StartCoroutine(tailroutine);
        }

    }

    IEnumerator Taildelay(int axis)
    {
        yield return new WaitForSeconds(tailDelayTime);
        tailturn = axis;

    }


    internal void Spawntail()
    {
        if (tail == null && active)
        {
            tail = Instantiate(tailprefab, transform.position, transform.rotation);
            tail.Initialize(dir, speed, tailDelayTime);
        }
        else if (active)
        {
            tail.Spawntail();
        }
    }
}
