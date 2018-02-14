using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Display and update user settings in settings menu
/// </summary>
public class SettingsManager : MonoBehaviour {

    private List<Toggle> toggles;

    private void Start()
    {
        toggles = new List<Toggle>(FindObjectsOfType<Toggle>());
        InitToggles();
    }

    /// <summary>
    /// Set initial toggle value and add listener
    /// </summary>
    private void InitToggles()
    {
        foreach (Toggle toggle in toggles)
        {
            bool value = PlayerPrefs.GetInt(toggle.name, 0) == 1;
            toggle.isOn = value;
            toggle.onValueChanged.AddListener(delegate { UpdateToggleSetting(toggle); });
        }
    }

    /// <summary>
    /// Save new value to player preferences
    /// </summary>
    /// <param name="sender"></param>
    public void UpdateToggleSetting(Toggle sender)
    {
        int value = sender.isOn ? 1 : 0;
        Debug.Log(value);
        PlayerPrefs.SetInt(sender.name, value);
        PlayerPrefs.Save();
    }
}
