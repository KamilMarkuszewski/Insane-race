using UnityEngine;
using System.Collections;
using System;


public class Init : MonoBehaviour
{

    GameObject vehicleCam;
    Transform vehicleCameraTarget;
    GameObject vehicle;
    GameObject StartPoint;

    GameObject Player;
    Camera mainCamera;

    private bool temp = false;


    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        StartPoint = GameObject.Find("StartPoint");
        vehicleCam = GameObject.Find("VehicleCamera");
        vehicle = GameObject.Find("Vehicle");
        mainCamera = Camera.main;
        mainCamera.enabled = false;

        AudioListener listener = vehicleCam.GetComponent(typeof(AudioListener)) as AudioListener;
        listener.enabled = false;
        mainCamera.enabled = true;
        listener.enabled = true;
        vehicle.transform.position = StartPoint.transform.position;
        vehicle.transform.rotation = StartPoint.transform.rotation;
    }
}
