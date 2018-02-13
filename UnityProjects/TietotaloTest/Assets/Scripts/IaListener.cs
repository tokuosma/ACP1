using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Stores and updates the latest API-values.
/// </summary>
public class IaListener : MonoBehaviour {

    #region fields & properties
    /// <summary>
    /// 
    /// </summary>
    public static IaListener Instance;
    public IndoorAtlas.Status Status { get; private set; }
    public IndoorAtlas.Location Location { get; private set; }
    public IndoorAtlas.Heading Heading { get; private set; }
    public IndoorAtlas.Region Region { get; private set; }
    public IndoorAtlas.Orientation Orientation { get; private set; }
    #endregion

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }   

#pragma warning disable IDE1006
    void onLocationChanged(string data)

    {
        IndoorAtlas.Location location = JsonUtility.FromJson<IndoorAtlas.Location>(data);
        this.Location = location;
    }

    void onStatusChanged(string data)
    {
        Status = JsonUtility.FromJson<IndoorAtlas.Status>(data);
    }

    void onHeadingChanged(string data)
    {
        Heading = JsonUtility.FromJson<IndoorAtlas.Heading>(data);
    }

    void onOrientationChange(string data)
    {
        Orientation = JsonUtility.FromJson<IndoorAtlas.Orientation>(data);
    }

    void onEnterRegion(string data)
    {
        Region = JsonUtility.FromJson<IndoorAtlas.Region>(data);
        RegionManager regionManager = FindObjectOfType<RegionManager>();
        if (regionManager != null)
        {
            regionManager.LoadRegion(Region.id);
        }
    }

    void onExitRegion(string data)
    {
        Region = JsonUtility.FromJson<IndoorAtlas.Region>(data);
        RegionManager regionManager = FindObjectOfType<RegionManager>();
        if (regionManager != null)
        {
            regionManager.LoadWaitScreen();
        }
    }
#pragma warning restore
}
