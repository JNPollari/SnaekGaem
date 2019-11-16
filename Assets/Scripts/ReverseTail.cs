using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseTail : MonoBehaviour
{
    [SerializeField]
    private ReverseTail tailprefab;

    private float dir = 00f;
    private ReverseTail tail;
    private List<State> states;
    private int offset;
    private State currentState;
    private int lifeTime;

    // Initialize should be calles as the tailpiece is first created
    public void Initialize(List<State> _states, int _offset, int _lifeTime)
    {
        states = _states;
        offset = _offset;
        lifeTime = _lifeTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        offset += 2;
        lifeTime--;
        if (lifeTime == 0) {
            Destroy(gameObject);
        }
        currentState = states[offset];
        transform.position = currentState.GetPosition();
        transform.rotation = currentState.GetRotation();
    }


    internal void Spawntail()
    {
        if (tail == null)
        {
            tail = Instantiate(tailprefab, transform.position, transform.rotation);
            tail.Initialize(states, offset - 5, lifeTime);
        }
        else
        {
            tail.Spawntail();
        }
    }
}

