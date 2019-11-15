using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float speed = 1;

    void Update()
    {
        gameObject.GetComponent<Transform>().Translate(Vector2.up * Time.deltaTime * speed);
    }
}
