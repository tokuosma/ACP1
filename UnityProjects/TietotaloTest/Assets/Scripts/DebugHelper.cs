using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A helper object for printing debug text on to the screen
/// </summary>
public class DebugHelper : MonoBehaviour {
    private GameObject player;
    private IaListener iaListener;
    private Text debugText;
    // Use this for initialization
    void Start () {
        if(PlayerPrefs.GetInt("ShowDebugText",0) == 1)
        {
            iaListener = IaListener.Instance;
            player = FindObjectOfType<Player>().gameObject;
            debugText = GetComponent<Text>();
        }
        else
        {
            // Disable debug text if not enabled in settings
            gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        debugText.text = FormatDebugText();
    }

    private string FormatDebugText()
    {
        StringBuilder sb = new StringBuilder();
#if (UNITY_ANDROID && !UNITY_EDITOR)
        sb.AppendLine("Status: " + Enum.GetName(typeof(IndoorAtlas.Status.ServiceStatus), iaListener.Status.status));
        sb.AppendLine("Region: " + iaListener.Region.name);
        sb.AppendLine("Location: Lat: " + iaListener.Location.latitude + ", Long: " + iaListener.Location.longitude);
        sb.AppendLine(string.Format("Heading: {0:F4}", iaListener.Heading.heading));
        sb.AppendLine(string.Format("Orientation: x: {0:F4}, y: {1:F4}, z: {2:F4}, w: {3:F2}", iaListener.Orientation.x, iaListener.Orientation.y, iaListener.Orientation.z, iaListener.Orientation.w));
        sb.AppendLine(string.Format("Player position: x: {0:F4}, y: {1:F4}, z: {2:F4}", player.transform.position.x, player.transform.position.y, player.transform.position.z));
        sb.AppendLine(string.Format("Camera rotation: x: {0:F4}, y: {1:F4}, z: {2:F4}", Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z));
#endif
        return sb.ToString();
    }
}
