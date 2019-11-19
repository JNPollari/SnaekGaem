using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailSprite : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private int endalpha;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.GetComponentInParent<Tail>().SetSprite(this);
    }

    internal void FadeIn(int offset)
    {
        endalpha = 100 - offset;
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

        for (int i = 0; i < endalpha; i++)
        {
            yield return new WaitForSeconds(0.02f);
            tmp.a = 0.01f * i;
            spriteRenderer.color = tmp;
        }
    }
}
