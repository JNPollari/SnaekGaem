using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private GameHandler gamehandler;


    internal void SetGameHandler(GameHandler gh){
	gamehandler = gh;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNomNom() {
	GetComponent<AudioSource>().Play();
    }
}
