using UnityEngine;
using System.Collections;
using System.Xml;
using HotWheels.Map.MapReader;
using System;

public class MapXmlReader : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Map m = ReadMap("Maps/Map1.xml");
        BuildMap(m);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void BuildMap(Map m)
    {
        MainMapElements MainMapElementsService = MainMapElements.GetInstance();
        CheckPoints CheckPointsService = CheckPoints.GetInstance();
        foreach (var item in m.mapElements)
        {
            //Debug.Log(item.Id + " " + item.x + " " + item.y + " " + item.z);
            var obj = MainMapElementsService.GetMapObject(item.Id);
            GameObject createdObj = Instantiate(obj.Obj, new Vector3(item.x, item.y, item.z), Quaternion.identity) as GameObject;
            createdObj.transform.Rotate(Vector3.up, item.rot);
            MainMapElementsService.existingObjects.Add(createdObj);
            if (item.Id == (int) MainMapElements.MainObjectNames.CheckPoint) 
            {
                Transform innerCheckPoint = createdObj.transform.FindChild("CheckPoint");
                CheckPointColider colider = innerCheckPoint.GetComponent(typeof(CheckPointColider)) as CheckPointColider;
                colider.number = CheckPointsService.CheckPointVisited.Count;
                CheckPointsService.CheckPoint.Add(colider.number, createdObj.transform);
                CheckPointsService.CheckPointVisited.Add(false);
            }
        }
    }


    private Map ReadMap(string path)
    {
        Map ret = new Map();


        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        var root = doc.FirstChild;

        foreach (XmlNode child in root.FirstChild.ChildNodes) 
        {
            switch (child.Name)
            { 
                case "MapName":
                    ret.Name = child.InnerText.ToString();
                    break;
                case "MinPlayers":
                    ret.MinPlayers = Int32.Parse(child.InnerText.ToString());
                    break;
                case "MaxPlayers":
                    ret.MaxPlayers = Int32.Parse(child.InnerText.ToString());
                    break;
                case "DifficultyLevel":
                    ret.DifficultyLevel = Int32.Parse(child.InnerText.ToString());
                    break;
            }
        }
        //Debug.Log(ret.Name + " " + ret.MinPlayers + " " + ret.MaxPlayers + " " + ret.DifficultyLevel + " ");

        foreach (XmlNode child in root.LastChild.ChildNodes)
        {
            MapElement elem = new MapElement();
            foreach (XmlNode myElement in child.ChildNodes)
            {
                switch (myElement.Name)
                {
                    case "Id":
                        elem.Id = Int32.Parse(myElement.InnerText.ToString());
                        break;
                    case "x":
                        elem.x = Int32.Parse(myElement.InnerText.ToString());
                        break;
                    case "z":
                        elem.z = Int32.Parse(myElement.InnerText.ToString());
                        break;
                    case "y":
                        elem.y = Int32.Parse(myElement.InnerText.ToString());
                        break;
                    case "rot":
                        elem.rot = Int32.Parse(myElement.InnerText.ToString());
                        break;
                }
            }
            //Debug.Log(elem.Id + " " + elem.x + " " + elem.y +  " " + elem.z + " " + elem.rot);
            ret.mapElements.Add(elem);
        }
        
        


        return ret;
    }

}
