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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Initialize(GameHandler _gameHandler) {
        gameHandler = _gameHandler;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Debug.Log("Snack eaten.");
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
