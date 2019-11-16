using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    private Head snakehead;

    [SerializeField]
    private PastHead pastsnake;

    private Vector3 snpos;
    private Quaternion snrot;
    private float spawntime;
    private bool sent = false;
    private bool spawnsaved = false;


    private float[] timestamps;
    private int[] directions;
    private int nextTimestamp = 0;
    private int timestamplimit = 95;
    private int lengthIncrease = 100;

    internal int Getdirection(float t)
    {
        int i = 0;
        while (timestamps[i] < t) i++;
        return directions[i];
    }


    // Start is called before the first frame update
    void Start()
    {
        directions = new int[100];
        timestamps = new float[100];

        snakehead = Instantiate(snakehead, transform.position, transform.rotation);
        snakehead.SetGameHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > 5 && !spawnsaved)
        {
            spawnsaved = true;
            snpos = snakehead.GetPosition();
            snrot = snakehead.GetRotation();
            spawntime = Time.time;
        }

        if (Time.time > 10 && !sent)
        {
            sent = true;
            pastsnake = Instantiate(pastsnake, snpos, snrot);
            pastsnake.Initialize(this, spawntime);
        }
        
    }

    public void CreateTimestamp(int direction)
    {
        timestamps[nextTimestamp] = Time.time;
        directions[nextTimestamp] = direction;

        Debug.Log("Timestamp created at " + timestamps[nextTimestamp] + " with direction of " + directions[nextTimestamp] + ", Index = " + nextTimestamp + ", Listlength = " + directions.Length);

        nextTimestamp++;
        if (nextTimestamp > timestamplimit)
        {
            timestamplimit = timestamplimit + lengthIncrease;
            ExpandLists();
        }
    }

    private void ExpandLists()
    {
        float[] temptimes = new float[timestamps.Length + lengthIncrease];
        for (int i = 0; i < timestamps.Length; i++)
        {
            timestamps[i] = temptimes[i];
        }
        timestamps = temptimes;

        int[] tempdirs = new int[directions.Length + lengthIncrease];
        for (int i = 0; i < directions.Length; i++)
        {
            directions[i] = tempdirs[i];
        }
        directions = tempdirs;
    }
}
