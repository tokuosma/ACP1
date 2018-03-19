using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Logger : MonoBehaviour {


    /// <summary>
    /// Time to wait in seconds after writing each log entry
    /// </summary>
    public float LogDelay = 0.5f;

    private IaListener iaListener;
    private Player player;
    private Coroutine coroutine;
    private bool isLogging;
    private bool hasRegion;
    public Text logStatusText;
    public Button button;
    public Text buttonText;

	// Use this for initialization
	void Start () {

        if(!(PlayerPrefs.GetInt("EnableLogging", 0) == 1))
        {
            Destroy(gameObject);
            return;
        }

        iaListener = FindObjectOfType<IaListener>();
        if (iaListener == null)
        {
            Debug.Log("IA listener not found in scene. Logging disabled");
            return;
        }

        player = FindObjectOfType<Player>();
        if (player == null)
        {
            Debug.Log("Player not found in scene. Exiting...");
            return;
        }

        logStatusText.text = "";
        buttonText.text = "START LOGGING";
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    public void ToggleLogging()
    {
        StartCoroutine("DoToggleLogging");
    }

    private IEnumerator DoToggleLogging()
    {
        button.interactable = false;
        if (isLogging)
        {
            EndLogging();
            yield return new WaitWhile(() => coroutine != null);
            buttonText.text = "START LOGGING";
            logStatusText.text = "**LOGGING STOPPED**";
            button.interactable = true;
        }
        else
        {
            StartLogging();
            yield return new WaitWhile(() => coroutine == null);
            buttonText.text = "STOP LOGGING";
            logStatusText.text = "**LOGGING ACTIVE**";
            button.interactable = true;
        }
    }

    public void StartLogging()
    {
        isLogging = true;
        coroutine = StartCoroutine("WriteLog");
    }

    public void EndLogging()
    {
        isLogging = false;
    }

    public IEnumerator WriteLog()
    {
        string logName = DateTime.Now.ToString("dd.MM.yy-HH.mm.ss", CultureInfo.CurrentUICulture) + ".txt";
        string filePath = Path.Combine(Application.persistentDataPath, logName);

        Debug.Log(filePath);

        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        using (StreamWriter sw = new StreamWriter(fs))
        {
            string timestamp = DateTime.Now.ToString(CultureInfo.CurrentUICulture);
            sw.WriteLine(string.Format("{0}\tLOG START", timestamp));
            if(iaListener.Region != null)
            {
                sw.WriteLine(string.Format("{0}\tRegion: {1}",timestamp, iaListener.Region.name));
            }

            while (isLogging)
            {
                timestamp = DateTime.Now.ToString(CultureInfo.CurrentUICulture);
                if(iaListener.Location != null)
                {
                    sw.WriteLine(string.Format("{0}\tIA LOCATION\t{1}\t{2}\t{3}", timestamp, iaListener.Location.latitude, iaListener.Location.longitude, iaListener.Location.accuracy));
                }
                else
                {
                    sw.WriteLine(string.Format("{0}\tIA LOCATION\t\t\t", timestamp));
                }
                sw.WriteLine(string.Format("{0}\tPLAYER LOCATION\t{1}\t{2}\t{3}", timestamp, player.transform.position.x, player.transform.position.y, player.transform.position.z));
                sw.Flush();
                yield return new WaitForSecondsRealtime(LogDelay);
            }
            sw.WriteLine(string.Format("{0}\tLOG END", timestamp));
            coroutine = null;
        }
    }
}
