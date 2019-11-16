using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;

    private float dir = 00f;
    private Vector3 pytdir3;
    private float speed = 0.02f;
    private bool active = false;
    private IEnumerator activationroutine;
    private float tailDelayTime = 0.5f;

    private IEnumerator tailroutine;
    private Tail tail;
    private float thisturn = 0;
    private float tailturn = 0;

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

            pytdir3.x = dir;
            pytdir3.y = dir;
            transform.Translate(pytdir3 * Time.deltaTime * speed / dir);

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
    internal void Turn(float direction)
    {
        if (active) dir -= direction;

        if (direction != thisturn)
        {
            thisturn = direction;
            tailroutine = Taildelay(direction);
            StartCoroutine(tailroutine);
        }

    }

    IEnumerator Taildelay(float axis)
    {
        yield return new WaitForSeconds(tailDelayTime);
        tailturn = axis;

    }


    internal void Spawntail()
    {
        if (tail == null && active)
        {
            tail = Instantiate(tailprefab, transform.position, Quaternion.identity);
            tail.Initialize(dir, speed, tailDelayTime);
        }
        else if (active)
        {
            tail.Spawntail();
        }
    }
}
