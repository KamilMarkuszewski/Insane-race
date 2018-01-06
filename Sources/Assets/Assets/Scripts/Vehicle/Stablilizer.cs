using UnityEngine;
using System.Collections;

public class Stablilizer : MonoBehaviour
{
    public WheelCollider WheelL;
    public WheelCollider WheelR;
    public WheelCollider WheelLFront;
    public WheelCollider WheelRFront;
    public float AntiRoll = 5000.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
       
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;
        float travelLfront = 1.0f;
        float travelRfront = 1.0f;

        var groundedL = WheelL.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-WheelL.transform.InverseTransformPoint(hit.point).y - WheelL.radius) / WheelL.suspensionDistance;

        var groundedR = WheelR.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-WheelR.transform.InverseTransformPoint(hit.point).y - WheelR.radius) / WheelR.suspensionDistance;

        var groundedLfront = WheelLFront.GetGroundHit(out hit);
        if (groundedLfront)
            travelLfront = (-WheelLFront.transform.InverseTransformPoint(hit.point).y - WheelLFront.radius) / WheelLFront.suspensionDistance;

        var groundedRfront = WheelRFront.GetGroundHit(out hit);
        if (groundedRfront)
            travelRfront = (-WheelRFront.transform.InverseTransformPoint(hit.point).y - WheelRFront.radius) / WheelRFront.suspensionDistance;


        if (!groundedRfront && !groundedLfront && !groundedR && groundedL) 
        {
            transform.rotation.SetEulerRotation(transform.rotation.x * 1 / 10, transform.rotation.y * 1 / 10, transform.rotation.z * 1 / 10);
        }
    }
}

