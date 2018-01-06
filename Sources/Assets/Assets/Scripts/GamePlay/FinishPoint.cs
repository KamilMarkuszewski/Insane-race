using UnityEngine;
using System.Collections;



public class FinishPoint : MonoBehaviour {

	Transform spawnPoint;
	private Transform vehicle;
	CarControl carControl;
	GamePlayView gpView;

	// Use this for initialization
	void Start () {
		vehicle = GameObject.Find("Vehicle").transform;
		carControl = vehicle.GetComponent(typeof(CarControl)) as CarControl;
        gpView = GamePlayView.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {

		gpView.model.LastRideTime = Time.time - gpView.model.timeOnRoad;
		gpView.model.czasZatrzymania = Time.time;
		gpView.model.controllsEnabled = false;
		carControl.stop = true;
		carControl.EngineRPM = 0;	
		carControl.Wheel_FL.motorTorque = 0;
		carControl.Wheel_FR.motorTorque = 0;
		carControl.Wheel_RL.brakeTorque = 100;
		carControl.Wheel_RR.brakeTorque = 100;
		if(Random.Range(0,1) == 0){
			carControl.Wheel_FL.steerAngle = 15.0F * -1.0F;
			carControl.Wheel_FR.steerAngle = 15.0F * -1.0F;
		}else{
			carControl.Wheel_FL.steerAngle = 15.0F * 1.0F;
			carControl.Wheel_FR.steerAngle = 15.0F * 1.0F;
		}
		carControl.stop = true;
	}
}
