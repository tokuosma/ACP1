using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Stores and updates ApiValues
/// </summary>
public class IaListener : MonoBehaviour {

    public static IaListener instance;
    public IndoorAtlas.Status status;
    public IndoorAtlas.Location location;
    public IndoorAtlas.Heading heading;
    public IndoorAtlas.Region region;
    public IndoorAtlas.Orientation orientation;

    private GameObject player;

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

    private void OnLevelWasLoaded(int level)
    {
        player = FindObjectOfType<FirstPersonController>().gameObject;

#if (UNITY_ANDROID && !UNITY_EDITOR)
        // Disable player controller for actual builds
        player.GetComponent<FirstPersonController>().enabled = false;
#endif
        
    }
    void onLocationChanged(string data)
    {
        IndoorAtlas.Location location = JsonUtility.FromJson<IndoorAtlas.Location>(data);
        this.location = location;
    }

    void onStatusChanged(string data)
    {
        status = JsonUtility.FromJson<IndoorAtlas.Status>(data);
    }

    void onHeadingChanged(string data)
    {
        heading = JsonUtility.FromJson<IndoorAtlas.Heading>(data);
    }

    void onOrientationChange(string data)
    {
        orientation = JsonUtility.FromJson<IndoorAtlas.Orientation>(data);
        Quaternion quaternion = orientation.getQuaternion();
        Quaternion rot = Quaternion.Inverse(new Quaternion(quaternion.x, quaternion.y, -quaternion.z, quaternion.w));
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)) * rot;
    }

    void onEnterRegion(string data)
    {
        region = JsonUtility.FromJson<IndoorAtlas.Region>(data);
        if (RegionManager.instance != null)
        {
            RegionManager.instance.LoadRegion(region.id);
        }
    }

    void onExitRegion(string data)
    {
        region = JsonUtility.FromJson<IndoorAtlas.Region>(data);
        if(RegionManager.instance != null)
        {
            RegionManager.instance.LoadWaitScreen();
        }
    }
}
