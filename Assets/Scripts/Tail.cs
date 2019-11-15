using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    private float dir = 10.0f;
    private Vector3 pytdir3;
    private float speed = 0.02f;

    private Tail tail;

    // Initialize should be calles as the tailpiece is first created
    public void Initialize(float direction, float speed)
    {
        dir = direction;
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, 2 * dir);

        pytdir3.x = dir;
        pytdir3.y = dir;
        transform.Translate(pytdir3 * Time.deltaTime * speed / dir);
    }

    /// <summary>
    /// Turns tail similary as head is turned via buttons
    /// </summary>
    /// <param name="right">wether tail should turn right. False turns left</param>    
    internal void Turn(float direction)
    {
        dir -= direction;
    }
}
