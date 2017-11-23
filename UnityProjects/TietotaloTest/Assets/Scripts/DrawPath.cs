// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class DrawPath : MonoBehaviour {

	public Transform goal;
	private NavMeshAgent agent;
	private bool linesDrawn;
	private LinkedList<GameObject> lines;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination (goal.transform.position);
		linesDrawn = false;
		lines = new LinkedList<GameObject> ();
	}	

	void Update(){

		if (Input.GetKeyDown (KeyCode.Q)) {
			UpdateDrawnPath ();
		}
	}

	private void UpdateDrawnPath(){
		if (linesDrawn) {
			foreach (var line in lines) {
				GameObject.Destroy (line);
			}
			lines.Clear ();
			linesDrawn = false;
		}
		Vector3 previous = transform.position;
		foreach (var corner in agent.path.corners) {
			GameObject navLine = new GameObject ();
			lines.AddLast (navLine);
			navLine.transform.position = previous;
			LineRenderer lr = navLine.AddComponent<LineRenderer> ();
			lr.SetPositions (new Vector3[]{ previous, corner });
			previous = corner;	
			linesDrawn = true;
		}

	}


}