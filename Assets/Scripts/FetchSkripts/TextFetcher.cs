using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFetcher : MonoBehaviour
{
    private GameHandler gh;

    // Start is called before the first frame update
    void Start()
    {
        gh = transform.parent.parent.GetComponentInParent<GameHandler>();
        if (gh != null) gh.SetTextField(gameObject.GetComponent<Text>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
