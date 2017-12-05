﻿// MoveTo.cs
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class DrawPath : MonoBehaviour {

	public Transform goal;
    public Material lineMaterial;
    public float navLineOffsetY;
    public Text distance;

    private NavMeshAgent agent;
	private bool linesDrawn;
	private LinkedList<GameObject> lines;
    private float pathLength;

    

    void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination (goal.transform.position);
		linesDrawn = false;
		lines = new LinkedList<GameObject> ();
        pathLength = 0;
	}	

	void Update(){

			UpdateDrawnPath ();
        
    }

	private void UpdateDrawnPath(){
		if (linesDrawn) {
			foreach (var line in lines) {
				GameObject.Destroy (line);
			}
			lines.Clear ();
			linesDrawn = false;
            pathLength = 0;
            

        }
		Vector3 previous = transform.position;
		foreach (var corner in agent.path.corners) {
            pathLength += Vector3.Distance(previous, corner);
            Vector3 navLineOffset = new Vector3(0, navLineOffsetY, 0);
			GameObject navLine = new GameObject ();
			lines.AddLast (navLine);
			navLine.transform.position = previous  + navLineOffset;
            LineRenderer lr = navLine.AddComponent<LineRenderer> ();
            lr.material = lineMaterial;
            lr.textureMode = LineTextureMode.Tile;
            lr.startWidth = 1f;
            lr.endWidth = 1f;
			lr.SetPositions (new Vector3[]{ previous + navLineOffset, corner + navLineOffset });
			previous = corner;	
			linesDrawn = true;
            //Debug.Log(pathLength);
            distance.text = pathLength.ToString();
        }

	}



}