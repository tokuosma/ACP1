using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Represents the user agent in the scene. Updates position and camera rotation.
/// </summary>
public class Player : MonoBehaviour {

    /// <summary>
    /// Delay in seconds after each position update
    /// </summary>
    public float positionUpdateDelay = 1.0f;
    /// <summary>
    /// If set to false, position is not updated in scene.
    /// </summary>
    public bool positionUpdateActive = true;
    /// <summary>
    /// Delay in seconds after each camera rotation update
    /// </summary>
    public float orientationUpdateDelay = 0.0167f;

    private Level level;
    private NavMeshAgent navMeshAgent;
    private DrawPath drawPath;

	// Use this for initialization
	void Start () {
        level = FindObjectOfType<Level>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        drawPath = GetComponentInChildren<DrawPath>();
#if ((UNITY_ANDROID || UNITY_IOS)  && !UNITY_EDITOR)
        // Disable player controller for actual builds
        //GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine("UpdateCameraOrientation");
        if (positionUpdateActive)
        {
            StartCoroutine("UpdatePosition");
        }
#endif
        StartCoroutine("UpdatePath");
    }

    private void Update()
    {

    }

    /// <summary>
    /// Coroutine for updating player position
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdatePosition()
    {
        for (; ; )
        {
            if (IaListener.Instance.Location != null)
            {
                Vector3 position = FindObjectOfType<Level>().GetLevelPosition(IaListener.Instance.Location.latitude, IaListener.Instance.Location.longitude);
                transform.position = position;
            }
            yield return new WaitForSeconds(positionUpdateDelay); // Set delay in editor
        }
    }

    /// <summary>
    /// Coroutine for updating drawn path
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdatePath()
    {
        for(; ; )
        {
            if (navMeshAgent.hasPath)
            {
                drawPath.UpdateDrawnPath();
            }
            yield return new WaitForSeconds(positionUpdateDelay); // Set delay in editor

        }
    }

    /// <summary>
    /// Coroutine for updating camera orientation
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateCameraOrientation()
    {
        for (; ; )
        {
            if (IaListener.Instance.Location != null)
            {
                Quaternion quaternion = IaListener.Instance.Orientation.getQuaternion();
                Quaternion rot = Quaternion.Inverse(new Quaternion(quaternion.x, quaternion.y, -quaternion.z, quaternion.w));
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f)) * rot;
            }
            yield return new WaitForSeconds(orientationUpdateDelay); // Set delay in editor
        }
    }
}
