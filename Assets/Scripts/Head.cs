using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    
    private float dir = 10.0f;
    private Vector3 pytdir3;

    [SerializeField]
    private Transform trans;
    
    [SerializeField]
    private float speed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        pytdir3 = new Vector3(dir, dir, 0);
    }

    
    void Update()
    {
        dir -= Input.GetAxis("Horizontal");
        transform.eulerAngles = new Vector3(0, 0, 2* dir);

        pytdir3.x = dir;
        pytdir3.y = dir;
        transform.Translate(pytdir3 * Time.deltaTime * speed / dir);


    }
}
