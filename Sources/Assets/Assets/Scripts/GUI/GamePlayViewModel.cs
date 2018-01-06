using System;

public class GamePlayViewModel
{
	public bool controllsEnabled = true;
	public float timeOnRoad = 0.0F;
	private float _lastRideTime= 0.0F;
	public float LastRideTime {
		get {return _lastRideTime;}
		set{
			if(value < bestTime) bestTime = value;
			_lastRideTime = value;
		}
	}


	public float czasZatrzymania = 0.0F;
	public float bestTime = 99999.0F;

}


