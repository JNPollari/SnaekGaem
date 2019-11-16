using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private Head snakehead;

    private 


    // Start is called before the first frame update
    void Start()
    {
        snakehead = Instantiate(snakehead, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
