// MoveTo.cs
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

/// <summary>
/// Class handles drawing the route to user agent's destination
/// </summary>
public class DrawPath : MonoBehaviour {

    public Material lineMaterial; 
    public float navLineOffsetY; 
	public bool AutoRedrawPath;
	public Text distance; // Text for displaying the remaining distance

    private Level level; 
    private NavMeshAgent agent;
	/// <summary>
	/// The nav line drawn.
	/// </summary>
	private GameObject navLine;

    void Start () {
        level = FindObjectOfType<Level>();
		agent = GetComponent<NavMeshAgent> ();
	}	

    /// <summary>
    /// Draws the path to user agent destination using line renderer
    /// </summary>
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
            //navLine.transform.position = transform.position;
			LineRenderer lr = navLine.GetComponent<LineRenderer> ();
			lr.positionCount = agent.path.corners.Length;
			lr.SetPositions (agent.path.corners);
		}
		// Update path length to UI
		distance.text = string.Format("{0:F1} m", GetPathLength());
	}

    /// <summary>
    /// Calculate the distance in meters to destination
    /// </summary>
    /// <returns>Distance (float) in meters</returns>
	public float GetPathLength(){
		if (agent != null && agent.hasPath) {
            float pathLength = 0.0f;
            Vector3 previous = transform.position;
            foreach (Vector3 corner in agent.path.corners)
            {
                pathLength += Vector3.Distance(previous, corner);
                previous = corner;
            }
			return pathLength * level.GetScalingFactor();
		} else {
			return 0;
		}
	}
	

}