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

    void PopulateList()
    {  
        dropdown.AddOptions(kohteet);
    }



}
