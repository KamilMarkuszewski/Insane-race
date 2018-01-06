using UnityEngine;
using System.Collections;

using System;

public class CheckPointColider : MonoBehaviour
{
    public int number;
    private Transform MainTerrain;
    CheckPoints cp;

    // Use this for initialization
    void Start()
    {
        MainTerrain = GameObject.Find("Terrain").transform;
        cp = MainTerrain.GetComponent(typeof(CheckPoints)) as CheckPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("CheckPoint " + number);
        if (cp.CheckPointVisited[number] == false)
        {
            cp.CheckPointVisited[number] = true;
            if (number > cp.lastCheckPointId) cp.lastCheckPointId = number;
            AudioSource audios = this.gameObject.GetComponent(typeof(AudioSource)) as AudioSource;
            audios.enabled = true;
            audios.Play();
        }
    }
}


