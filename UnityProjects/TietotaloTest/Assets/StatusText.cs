using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusText : MonoBehaviour {

    private Text text;
	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = string.Format("IA-Status: {0}", Enum.GetName(typeof(IndoorAtlas.Status.ServiceStatus), IaListener.Instance.Status.status));
	}
}
