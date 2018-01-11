using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaListener : MonoBehaviour {

    public static IaListener instance;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void onLocationChanged(string data)
    {
        IndoorAtlas.Location location = JsonUtility.FromJson<IndoorAtlas.Location>(data);
        Debug.Log("onLocationChanged " + location.latitude + ", " + location.longitude);

        //double posX = ((location.longitude - longitudeSW) / xMax);
        //double posY = ((location.latitude - latitudeSW) / yMax);

        //xmark.UpdatePosition(posX, posY);

        //locationText.text = string.Format("Long: {0}, Lat: {1}", location.longitude, location.latitude);
        //accuracyText.text = string.Format("Accuracy: {0} m", location.accuracy);
    }

    void onStatusChanged(string data)
    {
        IndoorAtlas.Status serviceStatus = JsonUtility.FromJson<IndoorAtlas.Status>(data);
        //statusText.text = Enum.GetName(typeof(IndoorAtlas.Status.ServiceStatus), serviceStatus.status);
        Debug.Log("onStatusChanged " + serviceStatus.status);
    }

    void onHeadingChanged(string data)
    {
        IndoorAtlas.Heading heading = JsonUtility.FromJson<IndoorAtlas.Heading>(data);
        Debug.Log("onHeadingChanged " + heading.heading);
    }

    void onOrientationChange(string data)
    {
        Quaternion orientation = JsonUtility.FromJson<IndoorAtlas.Orientation>(data).getQuaternion();
        Quaternion rot = Quaternion.Inverse(new Quaternion(orientation.x, orientation.y, -orientation.z, orientation.w));
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)) * rot;
    }

    void onEnterRegion(string data)
    {
        IndoorAtlas.Region region = JsonUtility.FromJson<IndoorAtlas.Region>(data);
        string text = "onEnterRegion " + region.name + ", " + region.type + ", " + region.id + " at " + region.timestamp;
        //regionText.text = region.name;

        Debug.Log(text);
    }

    void onExitRegion(string data)
    {
        IndoorAtlas.Region region = JsonUtility.FromJson<IndoorAtlas.Region>(data);
        Debug.Log("onExitRegion " + region.name + ", " + region.type + ", " + region.id + " at " + region.timestamp);
    }
}
