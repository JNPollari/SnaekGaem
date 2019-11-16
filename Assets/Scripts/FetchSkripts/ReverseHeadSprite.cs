using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseHeadSprite : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.GetComponentInParent<ReverseHead>().SetSprite(this);
        StartCoroutine("ColoFadeIn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void FadeOut()
    {
        StartCoroutine("ColoFadeOut");
    }

    IEnumerator ColoFadeIn()
    {
        Color tmp = spriteRenderer.color;

        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.02f);
            tmp.a = 0.02f * i;
            spriteRenderer.color = tmp;
        }
    }

    IEnumerator ColoFadeOut()
    {
        Color tmp = spriteRenderer.color;

        for (int i = 50; i > 0; i--)
        {
            yield return new WaitForSeconds(0.02f);
            tmp.a = 0.02f * i;
            spriteRenderer.color = tmp;
        }
        Destroy(gameObject);
    }
}
