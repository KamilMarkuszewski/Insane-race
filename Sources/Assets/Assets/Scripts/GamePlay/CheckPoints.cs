using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPoints : MonoBehaviour
{

    private Transform StartPoint;
    private Transform FinishPoint;
    private Transform Vehicle;
    CarControl carControl;

    float startCarTask = 0.0f;

    public IDictionary<int, Transform> CheckPoint = new Dictionary<int, Transform>();
    public IList<bool> CheckPointVisited = new List<bool>();
    public int lastCheckPointId = -1;
    GamePlayView gpView;

    public static CheckPoints GetInstance()
    {
        Transform obj = GameObject.Find("Terrain").transform;
        return obj.GetComponent(typeof(CheckPoints)) as CheckPoints;
    }


    // Use this for initialization
    void Start()
    {
        gpView = GamePlayView.GetInstance();

        StartPoint = GameObject.Find("StartPoint").transform;
        FinishPoint = GameObject.Find("FinishPoint").transform;
        Vehicle = GameObject.Find("Vehicle").transform;


        carControl = Vehicle.GetComponent(typeof(CarControl)) as CarControl;
        for (int i = 0; i < CheckPointVisited.Count; i++)
        {
            CheckPointVisited[i] = false;
        }
        lastCheckPointId = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (Vehicle != null && carControl != null)
            {

                gpView.model.timeOnRoad = Time.time;
                gpView.model.controllsEnabled = true;
                Vehicle.transform.position = StartPoint.transform.position;
                Vehicle.transform.rotation = StartPoint.transform.rotation;
                carControl.stop = true;
                carControl.stopAll = true;
                carControl.CurrentGear = 1;
                startCarTask = Time.time + 1.0f;
                lastCheckPointId = -1;
                for (int i = 0; i < CheckPointVisited.Count; i++)
                {
                    CheckPointVisited[i] = false;
                }
            }
        }

        if (startCarTask < Time.time && startCarTask > 0)
        {
            carControl.stop = false;
            startCarTask = 0.0f;
        }

        if (Input.GetKeyUp(KeyCode.Backspace) && gpView.model.controllsEnabled)
        {
            Transform lastCheckPointTr;
            if (lastCheckPointId == -1)
            {
                lastCheckPointTr = StartPoint;
                gpView.model.controllsEnabled = true;
                gpView.model.timeOnRoad = Time.time;
            }
            else
            {
                CheckPoint.TryGetValue(lastCheckPointId, out lastCheckPointTr); 
            }

            if (Vehicle != null && carControl != null && lastCheckPointTr != null)
            {
                Vehicle.transform.position = lastCheckPointTr.transform.position;
                Vehicle.transform.rotation = lastCheckPointTr.transform.rotation;
                if (lastCheckPointId> -1)  Vehicle.transform.Translate(new Vector3(0.0f, -10.0f, 0.0f));
                carControl.stop = true;
                carControl.stopAll = true;
                carControl.CurrentGear = 1;
                startCarTask = Time.time + 1.0f;
            }
        }




    }
}
