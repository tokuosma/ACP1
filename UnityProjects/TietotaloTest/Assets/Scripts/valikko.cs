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

	public void Dropdown_IndexChanged(int valinta)
    {
		
        selectedName.text = kohteet[valinta].Name;
		playerNavAgent.GetComponent<NavMeshAgent> ().SetDestination (kohteet [valinta].transform.position);
    }

    private void Start()
    {
		kohteet = new List<Destination> ();
		foreach (Destination destination in GameObject.FindObjectsOfType<Destination>()) {
			kohteet.Add (destination);
		}
        PopulateList();
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
