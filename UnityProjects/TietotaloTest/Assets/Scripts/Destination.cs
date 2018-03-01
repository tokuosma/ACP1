using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container for destination info
/// </summary>
public class Destination : MonoBehaviour {

	public string Name; 
	public string Description;
    private bool isActive;
    public GameObject DestinationArrowPrefab;
    private GameObject destinationArrow;

    private void Start()
    {
        isActive = false;
    }

    public void SetActive()
    {
        isActive = true;
        destinationArrow = Instantiate(DestinationArrowPrefab, transform.position + 5 * Vector3.up, Quaternion.Euler(new Vector3(90,0,0)));
    }

    public void Deactivate()
    {
        isActive = false;
        if(destinationArrow != null)
        {
            Destroy(destinationArrow);
        }
    }

}
