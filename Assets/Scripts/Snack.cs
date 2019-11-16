using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snack : MonoBehaviour
{
    private SoundController soundcontroller;

    public void SetSoundController(sc) {
	soundcontroller = cs;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        soundcontroller.PlayNomNom();
	Debug.Log("Fuuck");
    }
}
