using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using HotWheels;
using HotWheels.Map.MapReader;

public class MainMapElements : MonoBehaviour
{
    public static MainMapElements GetInstance()
    {
        Transform obj = GameObject.Find("GameObjects").transform;
        return obj.GetComponent(typeof(MainMapElements)) as MainMapElements;
    }

    string MainPrefabsPath = "Prefabs/MainMapObj/";
    string PrefabsExtension = ".prefab";

    public IDictionary<int, MapObject> objectsDict;
    public IList<GameObject> existingObjects;
    string[] MainObjectsNames = new string[] { 
        "Start", //1
        "Meta", //2
        "CheckPoint", //3
        "Floor1", //4
        "Wall1", //5
        "RampaSzeroka", //6
        "RampaMiniJump", //7
        "RampaMini1", //8
        "RampaMediumNiska", //9 
        "RampaMedium" //10
    };

    public enum MainObjectNames
    {
        Start = 1,
        Meta = 2,
        CheckPoint = 3,
        Floor1 = 4,
        Wall1 = 5,
        RampaSzeroka = 6,
        RampaMiniJump = 7,
        RampaMini1 = 8,
        RampaMediumNiska = 9,
        RampaMedium = 10

    };

    public MapObject GetMapObject(int id)
    {
        MapObject v;
        objectsDict.TryGetValue(id, out v);
        if (v != null) return v;
        else throw new MissingComponentException(id.ToString());
    }


    // Use this for initialization
    void Start()
    {
        existingObjects = new List<GameObject>();
        objectsDict = new Dictionary<int, MapObject>();
        var list = Resources.LoadAll(MainPrefabsPath);
        for (int i = 0; i<MainObjectsNames.Length; i++)
        {
            string elem = MainObjectsNames.ElementAt(i);
            GameObject obj = (GameObject)list.Where(el => el.name.Equals(elem)).First();
            var MapObj = new MapObject(i + 1, obj, elem);
            objectsDict.Add(MapObj.Id, MapObj);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
