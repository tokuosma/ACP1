using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DebugHelper : MonoBehaviour {
    public GameObject player;

    private IaListener iaListener;
    public Text debugText;
    // Use this for initialization
    void Start () {
        iaListener = FindObjectOfType<IaListener>();
	}
	
	// Update is called once per frame
	void Update () {
        debugText.text = FormatDebugText();
    }

    private string FormatDebugText()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Status: " + Enum.GetName(typeof(IndoorAtlas.Status.ServiceStatus), iaListener.status.status));
        sb.AppendLine("Region: " + iaListener.region.name);
        sb.AppendLine("Location: Lat: " + iaListener.location.latitude + ", Long: " + iaListener.location.longitude);
        sb.AppendLine(string.Format("Heading: {0:F4}", iaListener.heading.heading));
        sb.AppendLine(string.Format("Orientation: x: {0:F4}, y: {1:F4}, z: {2:F4}, w: {3:F2}", iaListener.orientation.x, iaListener.orientation.y, iaListener.orientation.z, iaListener.orientation.w));
        sb.AppendLine(string.Format("Player position: x: {0:F4}, y: {1:F4}, z: {2:F4}", player.transform.position.x, player.transform.position.y, player.transform.position.z));
        sb.AppendLine(string.Format("Player rotation: x: {0:F4}, y: {1:F4}, z: {2:F4}", player.transform.eulerAngles.x, player.transform.eulerAngles.y, player.transform.eulerAngles.z));
        sb.AppendLine(string.Format("Camera rotation: x: {0:F4}, y: {1:F4}, z: {2:F4}", Camera.main.transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, Camera.main.transform.localEulerAngles.z));
        return sb.ToString();
    }
}
