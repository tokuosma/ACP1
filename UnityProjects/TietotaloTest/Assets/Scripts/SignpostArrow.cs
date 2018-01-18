using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignpostArrow : MonoBehaviour {
    public Transform target;
    
	// Use this for initialization
	void Start () {
        transform.LookAt(target);
    }
	

}
