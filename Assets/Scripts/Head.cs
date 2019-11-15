using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField]
    private Tail tailprefab;

    private float dir = 10.0f;
    private Vector3 pytdir3;

    private IEnumerator tailroutine;

    private Tail tail;
    private float headturn = 0;
    private float tailturn = 0;
        
    [SerializeField]
    private float speed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        pytdir3 = new Vector3(dir, dir, 0);
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Spawntail();
        }

        dir -= Input.GetAxis("Horizontal");

        if (Input.GetAxis("Horizontal") != headturn)
        {
            headturn = Input.GetAxis("Horizontal");
            tailroutine = Taildelay(Input.GetAxis("Horizontal"));
            StartCoroutine(tailroutine);
        }

        if (tailturn != 0 && tail != null)
        {
            tail.Turn(tailturn);
        }
        
        transform.eulerAngles = new Vector3(0, 0, 2* dir);

        pytdir3.x = dir;
        pytdir3.y = dir;
        transform.Translate(pytdir3 * Time.deltaTime * speed / dir);


    }

    IEnumerator Taildelay(float axis)
    {
        yield return new WaitForSeconds(1);
        tailturn = axis;

    }

    private void Spawntail()
    {
        if (tail == null)
        {
            tail = Instantiate(tailprefab, transform.position, Quaternion.identity);
            tail.Initialize(dir, speed);
        }
    }




}
