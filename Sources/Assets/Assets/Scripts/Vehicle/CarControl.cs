using UnityEngine;
using System.Collections;
using System;


public class CarControl : MonoBehaviour
{
    public WheelCollider Wheel_FL;
    public WheelCollider Wheel_FR;
    public WheelCollider Wheel_RL;
    public WheelCollider Wheel_RR;
    public float[] GearRatio;
    public float[] GearMaxSpeed;
    public int CurrentGear = 0;
    public float EngineTorque = 300.0F;
    public float MaxEngineRPM = 1500.0F;
    public float MinEngineRPM = 200.0F;

    public float RMinSpeed = -50.0F;
    public float SteerAngle = 10.0F;
    public bool controlsEnabled = false;
    public Transform COM;
    public float Speed;
    public float maxSpeed = 250.0F;
    public AudioSource skidAudio;
    public float EngineRPM = 0.0F;
    private float motorInput;

    public bool stop = false;
    public bool stopAll = false;

    public int MaxGears = 6;

    // Use this for initialization
    void Start()
    {
        rigidbody.centerOfMass = new Vector3(COM.localPosition.x * transform.localScale.x, COM.localPosition.y * transform.localScale.y, COM.localPosition.z * transform.localScale.z);
        GearRatio = new float[MaxGears];
        GearRatio[0] = 2.31F;
        GearRatio[1] = 2.71F;
        GearRatio[2] = 1.88F;
        GearRatio[3] = 1.41F;
        GearRatio[4] = 1.13F;
        GearRatio[5] = 0.93F;

        GearMaxSpeed = new float[MaxGears];
        GearMaxSpeed[0] = 150.0F;
        GearMaxSpeed[1] = 170.0F;
        GearMaxSpeed[2] = 180.0F;
        GearMaxSpeed[3] = 200.0F;
        GearMaxSpeed[4] = 220.0F;
        GearMaxSpeed[5] = 250.0F;

        SteerAngle = 10.0F;
        StopAll();
        stop = false;
    }

    void StopAll()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.rotation = transform.rotation;
        rigidbody.inertiaTensorRotation = transform.rotation;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.drag = 0;
        rigidbody.angularDrag = 0;
        EngineRPM = 0;
        Speed = 0;
        audio.pitch = 0;
        Wheel_RL.brakeTorque = 0;
        Wheel_RR.brakeTorque = 0;
        Wheel_FL.motorTorque = 0;
        Wheel_FR.motorTorque = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (stop)
        {
            if (stopAll) { StopAll(); }
            else
            {
                rigidbody.drag = rigidbody.velocity.magnitude / 100;
                audio.pitch = audio.pitch - (audio.pitch / 25.0f);
            }
            return;
        }
        stopAll = false;
        Speed = rigidbody.velocity.magnitude * 3.6f;

        EngineRPM = (Wheel_FL.rpm + Wheel_FR.rpm) / 2 * GearRatio[CurrentGear];

        ShiftGears();

        //Input For MotorInput.
        motorInput = Input.GetAxis("Vertical");

        //Audio
        audio.pitch = Mathf.Abs(EngineRPM / MaxEngineRPM) + 1.0F;
        if (audio.pitch > 2.0)
        {
            audio.pitch = 2.0F;
        }

        //Is The Car Enabled "Is The Player In The Car"
        //If The Player Is In The Car Do These.
        if (controlsEnabled)
        {
            float mnoznik = 1.0F;
            float mnoznikBieg = 1.0F;
            if (Input.GetAxis("Horizontal") != 0) mnoznik = 0.7F - (Math.Abs(Input.GetAxis("Horizontal") * 1.8F) * (Speed / maxSpeed));
            mnoznikBieg = 1.0F * ((float)((MaxGears * 1.8) - CurrentGear + 1) / (float)(MaxGears * 1.5));
            //Steering
            Wheel_FL.steerAngle = SteerAngle * Input.GetAxis("Horizontal");
            Wheel_FR.steerAngle = SteerAngle * Input.GetAxis("Horizontal");

            //HandBrake
            if (Input.GetKey(KeyCode.Space))
            {
                Wheel_FL.motorTorque = 0;
                Wheel_FR.motorTorque = 0;
                Wheel_FL.brakeTorque = 100;
                Wheel_FR.brakeTorque = 100;

                Wheel_RL.motorTorque = 0;
                Wheel_RR.motorTorque = 0;
                Wheel_RL.brakeTorque = 200;
                Wheel_RR.brakeTorque = 200;
                mnoznik = 0.0F;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                ;
                Wheel_FL.brakeTorque = 0;
                Wheel_FR.brakeTorque = 0;
            }

            //Speed Limiter / Engine Input.
            if (Speed > maxSpeed || Speed > GearMaxSpeed[CurrentGear] || mnoznik == 0.0F)
            {
                Wheel_FL.motorTorque = 0;
                Wheel_FR.motorTorque = 0;
            }
            else
            {
                if (motorInput >= 0 || EngineRPM > 0)
                {
                    Wheel_FL.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical") * mnoznik * mnoznikBieg;
                    Wheel_FR.motorTorque = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical") * mnoznik * mnoznikBieg;
                }
                else
                {
                    float speed = EngineTorque / GearRatio[CurrentGear] * Input.GetAxis("Vertical");
                    if (EngineRPM < -1000.0f && Input.GetAxis("Vertical") < 0) speed = RMinSpeed;
                    Wheel_FL.motorTorque = speed;
                    Wheel_FR.motorTorque = speed;
                }
            }

        }

        if (motorInput <= 0)
        {
            Wheel_RL.brakeTorque = 30;
            Wheel_RR.brakeTorque = 30;
        }
        else if (motorInput >= 0)
        {
            Wheel_RL.brakeTorque = 0;
            Wheel_RR.brakeTorque = 0;
        }

        //SkidAudio.
        WheelHit CorrespondingGroundHit;
        Wheel_RR.GetGroundHit(out CorrespondingGroundHit);
        if (Mathf.Abs(CorrespondingGroundHit.sidewaysSlip) > 10 || Input.GetKey(KeyCode.Space))
        {
            skidAudio.enabled = true;
        }
        else
        {
            skidAudio.enabled = false;
        }
    }

    private float lastGearShift = 0.0f;
    public void ShiftGears()
    {

        if (lastGearShift + 2.0F < Time.time)
        {
            if (EngineRPM >= MaxEngineRPM)
            {
                int AppropriateGear = CurrentGear;

                for (int i = 0; i < GearRatio.Length; i++)
                {
                    if (Wheel_FL.rpm * GearRatio[i] < MaxEngineRPM)
                    {
                        AppropriateGear = i;
                        break;
                    }
                }
                if (CurrentGear != AppropriateGear && CurrentGear < MaxGears - 1)
                {
                    lastGearShift = Time.time;
                    CurrentGear ++;
                }      
            }
        }

        if (EngineRPM <= MinEngineRPM)
        {
            int AppropriateGear = CurrentGear;
            for (int j = GearRatio.Length - 1; j >= 0; j--)
            {
                if (Wheel_FL.rpm * GearRatio[j] > MinEngineRPM)
                {
                    AppropriateGear = j;
                    break;
                }
            }
            if (CurrentGear > AppropriateGear && CurrentGear > 0)
            {
                CurrentGear = AppropriateGear;
            }
        }
    }

}


