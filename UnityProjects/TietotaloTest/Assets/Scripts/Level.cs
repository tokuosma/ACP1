using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Storage class for level info
/// </summary>
public class Level : MonoBehaviour
{
    #region properties
    /// <summary>
    /// Width of the level (floorplan) in meters (Value set in editor)
    /// </summary>
    public float levelWidthMeter;
    /// <summary>
    /// Height of the level (floorplan) in meters (Value set in editor)
    /// </summary>
    public float levelHeightMeter;

    /// <summary>
    /// Width of the level (floorplan) in coordinate degrees
    /// </summary>
    private double levelWidthDegree;

    /// <summary>
    /// Height of the level (floorplan) in coordinate degrees
    /// </summary>
    private double levelHeightDegree;

    /// <summary>
    /// Minumum latitude value
    /// </summary>
    private double minLatitude;

    /// <summary>
    /// Minimum longitude value
    /// </summary>
    private double minLongitude;

    /// <summary>
    /// Width of the level in game units
    /// </summary>
    private float levelWidth;

    /// <summary>
    /// Height of the level in game units
    /// </summary>
    private float levelHeight;

    /// <summary>
    /// Minimum x-coordinate in game units
    /// </summary>
    private float minX;

    /// <summary>
    /// Minimum z-coordinate in game units
    /// </summary>
    private float minZ;

    /// <summary>
    /// Floor level y-coordinate
    /// </summary>
    private float yLevel;

    /// <summary>
    /// Maximum x-coordinate in game units
    /// </summary>
    private float maxX;

    /// <summary>
    /// Maximum z-coordinate in game units
    /// </summary>
    private float maxZ;
    private float scalingFactor;

    /// <summary>
    /// Scaling factor for converting the game units to meters [m/GU]
    /// </summary>
    public float GetScalingFactor()
    {
        return scalingFactor;
    }

    /// <summary>
    /// Scaling factor for converting the game units to meters [m/GU]
    /// </summary>
    private void SetScalingFactor(float value)
    {
        scalingFactor = value;
    }

    /// <summary>
    /// Dictionary containing all the corners
    /// </summary>
    private Dictionary<Corner.CornerType, Corner> corners;
    #endregion

    // Use this for initialization
    void Start()
    {
        InitLevel();
    }

    public void InitLevel()
    {
        // Add all corners to dict
        corners = new Dictionary<Corner.CornerType, Corner>();
        var cornersArray = FindObjectsOfType<Corner>();
        foreach (Corner corner in cornersArray)
        {
            corners.Add(corner.cornerType, corner);
        }

        // Calculate level dimensions and scale
        levelHeightDegree = corners[Corner.CornerType.NorthEast].latitude - corners[Corner.CornerType.SouthEast].latitude;
        levelWidthDegree = corners[Corner.CornerType.NorthEast].longitude- corners[Corner.CornerType.NorthWest].longitude;
        minLatitude = corners[Corner.CornerType.SouthWest].latitude;
        minLongitude = corners[Corner.CornerType.NorthWest].longitude;
        minX = corners[Corner.CornerType.SouthWest].transform.position.x;
        maxX = corners[Corner.CornerType.SouthEast].transform.position.x;
        minZ = corners[Corner.CornerType.SouthWest].transform.position.z;
        maxZ = corners[Corner.CornerType.NorthWest].transform.position.z;
        levelHeight = maxZ - minZ;
        levelWidth = maxX - minX;
        yLevel = corners[Corner.CornerType.NorthEast].transform.position.y + 1.75f;
        SetScalingFactor(levelWidthMeter / levelWidth);

#if UNITY_EDITOR
        Debug.Log("Position NE: " + GetLevelPosition(corners[Corner.CornerType.NorthEast].latitude, corners[Corner.CornerType.NorthEast].longitude));
        Debug.Log("Position NW: " + GetLevelPosition(corners[Corner.CornerType.NorthWest].latitude, corners[Corner.CornerType.NorthWest].longitude));
        Debug.Log("Position SE: " + GetLevelPosition(corners[Corner.CornerType.SouthEast].latitude, corners[Corner.CornerType.SouthEast].longitude));
        Debug.Log("Position SW: " + GetLevelPosition(corners[Corner.CornerType.SouthWest].latitude, corners[Corner.CornerType.SouthWest].longitude));
#endif
    }

    /// <summary>
    /// Converts given latitude and longitude values to game world coordinates
    /// </summary>
    /// <param name="latitude">Latitude [°]</param>
    /// <param name="longitude">Longitude [°]</param>
    /// <returns>Vector3</returns>
    public Vector3 GetLevelPosition(double latitude, double longitude)
    {

        var heightNorm = Mathf.Clamp((float)((latitude -minLatitude ) / levelHeightDegree), 0f, 1f);
        var widthNorm = Mathf.Clamp((float)((longitude - minLongitude ) / levelWidthDegree), 0f,1f);

        Vector3 position = new Vector3
        {
            x = (float)(minX + (levelWidth * widthNorm)),
            y = yLevel,
            z = (float)(minZ + (levelHeight * heightNorm))
        };

        return position;
    }
}
