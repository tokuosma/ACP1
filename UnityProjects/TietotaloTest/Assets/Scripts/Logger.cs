using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

	// Use this for initialization
	void Start () {

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

        StartLogging();
	}
	
	// Update is called once per frame
	void Update () {
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
            sw.WriteLine(string.Format("[{0}] LOG START", timestamp));
            if(iaListener.Region != null)
            {
                sw.WriteLine(string.Format("[{0}] Region: {1}",timestamp, iaListener.Region.name));
            }

            while (isLogging)
            {
                timestamp = DateTime.Now.ToString(CultureInfo.CurrentUICulture);
                if(iaListener.Location != null)
                {
                    sw.WriteLine(string.Format("[{0}] IA Location: Lat:{1}, Long:{2}, Acc: {3}", timestamp, iaListener.Location.latitude, iaListener.Location.longitude, iaListener.Location.accuracy));
                }
                else
                {
                    sw.WriteLine(string.Format("[{0}] IA Location: NONE", timestamp));
                }
                sw.WriteLine(string.Format("[{0}] Player location: {1}", timestamp, player.transform.position.ToString()));
                sw.Flush();
                yield return new WaitForSecondsRealtime(LogDelay);
            }
            sw.WriteLine(string.Format("[{0}] LOG END", timestamp));
        }
    }

    public static GameObject StartLogger()
    {
        GameObject logger = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity);
        logger.AddComponent<Logger>();
        return logger;
    }
}
