using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class valikko : MonoBehaviour
{
	List<Destination> kohteet;

	public GameObject playerNavAgent;
    public Dropdown dropdown;
    public Text selectedName;
    private ArrowLookAt arrowLookAt;

    ///<summary>Sets new destination when Dropdown index "valinta" is changed. </summary>
    ///<param name="valinta">Dropdown index</param>
	public void Dropdown_IndexChanged(int valinta)
    {
        selectedName.text = kohteet[valinta].Name;
		playerNavAgent.GetComponent<NavMeshAgent> ().SetDestination (kohteet [valinta].transform.position);
        arrowLookAt.target = kohteet[valinta].transform;
    }

    private void Start()
    {
        playerNavAgent = FindObjectOfType<Player>().transform.GetComponentInChildren<NavMeshAgent>().gameObject;

		kohteet = new List<Destination> ();
		foreach (Destination destination in GameObject.FindObjectsOfType<Destination>()) {
			kohteet.Add (destination);
		}
        PopulateList();

        arrowLookAt = FindObjectOfType<ArrowLookAt>();
    }

	private void Update(){
		if (Input.GetButtonDown("PreviousDestination")) {
			dropdown.Hide ();
			if(dropdown.value -1 < 0){
				// Cycle back to last option
				dropdown.value = dropdown.options.Count - 1;
			}
			else{dropdown.value -= 1;}
		}
		if(Input.GetButtonDown("NextDestination")){
			dropdown.Hide ();
			if(dropdown.value + 1 >= dropdown.options.Count){
				dropdown.value = 0;
			}
			else{
				dropdown.value += 1;
			}
		}
	}

    void PopulateList()
    {  
		List<string> names = new List<string> ();
		foreach (Destination destination in kohteet) {
			names.Add (destination.Name);
		}
		dropdown.AddOptions (names);
    }

}
