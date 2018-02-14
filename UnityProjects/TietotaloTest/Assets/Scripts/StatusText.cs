using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display IA-status in the wait_screen
/// </summary>
public class StatusText : MonoBehaviour {
    private Text statusText;
	// Use this for initialization
	void Start () {
        statusText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        statusText.text = string.Format("Status: {0}", IaListener.Instance != null && IaListener.Instance.Status != null? Enum.GetName(typeof(IndoorAtlas.Status.ServiceStatus),IaListener.Instance.Status.status) : "NOT INITIALIZED");
	}
}
