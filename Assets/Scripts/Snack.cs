using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snack : MonoBehaviour
{
    private GameHandler gameHandler;
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip audioClip;
    private State state;
    private int stateCount = 0;
    private int tails;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        stateCount++;
    }

    internal void Initialize(GameHandler _gameHandler, State _state, int _tails) {
        gameHandler = _gameHandler;
        state = _state;
        tails = _tails;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Snack eaten.");
            gameHandler.createReverseSnake(state, stateCount, tails);
            gameHandler.incrementScore(1);
            gameHandler.createSnack();
            audioSource.PlayOneShot(audioClip);
            Destroy(gameObject.GetComponent<SpriteRenderer>());
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            Destroy(gameObject, 5);
            collision.gameObject.GetComponent<Head>().growTail();
        }
    }
}
