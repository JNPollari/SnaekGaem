using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;

    private float dir = 0f;
    private Vector3 pytdir3;
    private float turnrate = 1;

    private IEnumerator tailroutine;
    private float tailDelayTime = 0.15f;

    private Tail tail;
    private float headturn = 0;
    private float tailturn = 0;
        
    [SerializeField]
    private float speed = 30;
    private float turnRateModifier = 0.015f;

    // Start is called before the first frame update
    void Start()
    {
        pytdir3 = new Vector3(dir, dir, 0);
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
        if (dir != 0) transform.Translate(pytdir3 * Time.deltaTime * speed * speed / dir);

        Debug.Log(turnrate);


    }

    IEnumerator Taildelay(float axis)
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
