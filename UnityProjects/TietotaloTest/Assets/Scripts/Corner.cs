using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container class for map corner point
/// </summary>
public partial class Corner : MonoBehaviour {

    public CornerType cornerType; // NW,NE,SW or SE
    public double latitude;     // Longitude in degrees.
    public double longitude;    // Latitude in degrees.
}