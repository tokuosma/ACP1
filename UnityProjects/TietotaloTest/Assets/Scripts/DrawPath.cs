// MoveTo.cs
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class DrawPath : MonoBehaviour {

	public Transform goal;
    public Material lineMaterial;
    public float navLineOffsetY;
	public bool AutoRedrawPath;
	public Text distance;

    private NavMeshAgent agent;
	/// <summary>
	/// The nav line drawn.
	/// </summary>
	private GameObject navLine;
    private float pathLength;

    void Start () {
		agent = GetComponent<NavMeshAgent> ();
		agent.SetDestination (goal.transform.position);
        pathLength = 0;
	}	

	void Update(){

		if (Input.GetButtonDown("DrawPath") || AutoRedrawPath) {
			UpdateDrawnPath ();
		}
    }

	public void UpdateDrawnPath(){
		if (navLine == null) {
			// Insantiate a new nav line game object and draw initial path
			navLine = new GameObject ("NavLine");
			navLine.transform.position = transform.position;
			navLine.transform.rotation = Quaternion.Euler(90,0,0);
			LineRenderer lr = navLine.AddComponent<LineRenderer> ();
			lr.material = lineMaterial;
			lr.textureMode = LineTextureMode.Tile;
			lr.alignment = LineAlignment.Local;
			lr.useWorldSpace = true;
			lr.startWidth = 1f;
			lr.endWidth = 1f;
			lr.startColor = Color.cyan;
			lr.endColor = Color.red;
			lr.positionCount = agent.path.corners.Length;
			lr.SetPositions (agent.path.corners);
		} else {
			// Update existinging nav line
			LineRenderer lr = navLine.GetComponent<LineRenderer> ();
			lr.positionCount = agent.path.corners.Length;
			lr.SetPositions (agent.path.corners);
		}
		// Update path length to UI
		distance.text = GetPathLength ().ToString ();
	}

	public float GetPathLength(){
		if (agent != null && agent.hasPath) {
			float pathLength = 0.0f;
			Vector3 previous = transform.position;
			foreach (Vector3 corner in agent.path.corners) {
				pathLength += Vector3.Distance (previous, corner);
				previous = corner;
			}
			return pathLength;
		} else {
			return 0;
		}
	}
	

}