using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles switching scenes based on active region when IA listener is active. 
/// </summary>
public class RegionManager : MonoBehaviour {

    /// <summary>
    /// If set to false, region is not updated 
    /// </summary>
    public bool regionUpdateEnabled = true;
    /// <summary>
    /// Contains all region id's and related scene names
    /// </summary>
    private Dictionary<string, string> regionDictionary;

    private void Awake()
    {
        InitDictionary();
    }
    // Use this for initialization
    void Start () {
        
        if(SceneManager.GetActiveScene().name == "wait_screen")
        {
            // Load previous scene on entering wait screen if it exists
            if (IaListener.Instance.Region != null && IaListener.Instance.Region.type != IndoorAtlas.Region.Type.Unknown)
            {
                LoadRegion(IaListener.Instance.Region.id);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    /// <summary>
    /// Initialize dictionary
    /// </summary>
    private void InitDictionary()
    {
        regionDictionary = new Dictionary<string, string>
        {
            { "3da8af39-9a0b-4d00-9b47-b9149671aa87", "kalervontie_3hk" },
            { "ed5143fc-b9e4-41cf-804c-22c86ca9afe1", "" }, // Tellus, scene puuttuu
            { "5f0aac9d-c2bb-4dfc-9418-daeadfc8ad44", "tietotalo_3krs" }
        };

    }

    /// <summary>
    /// Load scene with matching regionId
    /// </summary>
    /// <param name="regionId">Region id obtained from IndoorAtlas.Region object</param>
    public void LoadRegion(string regionId)
    {
        if (regionUpdateEnabled)
        {
            string sceneName = regionDictionary[regionId];
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.Log(string.Format("No scene with id '{0}' found in the region dictionary! ", regionId));
            }
            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// Load wait screen
    /// </summary>
    public void LoadWaitScreen()
    {
        SceneManager.LoadScene("wait_screen");
    }

}
