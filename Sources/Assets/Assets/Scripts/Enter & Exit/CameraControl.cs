using UnityEngine;
using System.Collections;
using System;


public class CameraControl : MonoBehaviour
{
    float maxRayDistance = 2.0f;
    LayerMask layerMask;
    GUISkin mySkin;
    bool showGui = false;

    void Update()
    {
        Vector3 direction = gameObject.transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        Vector3 position = transform.position;
        if (Physics.Raycast(position, direction, out hit, maxRayDistance, layerMask.value))
        {
            showGui = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject target = hit.collider.gameObject;
                target.BroadcastMessage("Action");
            }
        }
        else
        {
            showGui = false;
        }
    }

    void OnGUI()
    {
        GUI.skin = mySkin;
        if (showGui)
        {
            GUI.Label(new Rect(Screen.width - (Screen.width / 1.7f), Screen.height - (Screen.height / 1.4f), 800, 100), "Press key >>E<< to Use");
        }
    }

}