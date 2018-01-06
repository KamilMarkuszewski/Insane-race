using UnityEngine;
using System.Collections;
using System.Reflection;

public class GamePlayView : MonoBehaviour {

	public static GamePlayView GetInstance()
	{
		Transform obj = GameObject.Find("VehicleCamera").transform;
		return obj.GetComponent(typeof(GamePlayView)) as GamePlayView;
	}

	
	
	private Transform vehicle;
	private CarControl carControl;
	public GamePlayViewModel model;

	// Use this for initialization
	void Start () {
		vehicle = GameObject.Find("Vehicle").transform;
		carControl = vehicle.GetComponent(typeof(CarControl)) as CarControl;
		model = new GamePlayViewModel();
		model.timeOnRoad = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(carControl == null)return;

		float pseudoTeraz = Time.time;
		if(!model.controllsEnabled) pseudoTeraz = model.czasZatrzymania;
		int szerokosc =  Screen.width - 200;
		int wysokosc = (Screen.height /4);
		GUI.Label(new Rect(szerokosc, wysokosc, 200, 25), "Bieg: " + (carControl.CurrentGear + 1));
		wysokosc = wysokosc + 10 +25;
		GUI.Label(new Rect(szerokosc, wysokosc, 200, 25), "Predkosc: " + ((int)carControl.Speed));
		wysokosc = wysokosc + 10 +25;
		GUI.Label(new Rect(szerokosc, wysokosc, 200, 25), "Czas: " + (int)(pseudoTeraz - model.timeOnRoad));
		wysokosc = wysokosc + 10 +25;
		GUI.Label(new Rect(szerokosc, wysokosc, 200, 25), "Ostatni przejazd: " + (int)( model.LastRideTime));
		wysokosc = wysokosc + 10 +25;
		GUI.Label(new Rect(szerokosc, wysokosc, 200, 25), "Najlepszy przejazd: " + ( ( model.bestTime == 99999.0F)? "-" : ((int)( model.bestTime)).ToString()));
		wysokosc = wysokosc + 10 +25;
	}
}
