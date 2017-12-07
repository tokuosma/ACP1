using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class valikko : MonoBehaviour
{

    List<string> kohteet = new List<string> { "yksi", "kaksi", "kolme" };

    public Dropdown dropdown;
    public Text selectedName;


    public void Dropdown_IndexChanged(int valinta)
    {
        selectedName.text = kohteet[valinta];
    }

    private void Start()
    {
        PopulateList();
    }

	private void Update(){
		if (Input.GetButtonDown("PreviousDestination")) {
			if(dropdown.value -1 < 0){
				// Cycle back to last option
				dropdown.value = dropdown.options.Count - 1;
			}
			else{dropdown.value -= 1;}
		}
		if(Input.GetButtonDown("NextDestination")){
			if(dropdown.value + 1 >= dropdown.options.Count){
				dropdown.value = 0;
			}
			else{
				dropdown.value += 1;
			}
		}
		Debug.Log(dropdown.value);
	}

    void PopulateList()
    {  
        dropdown.AddOptions(kohteet);
    }

}
