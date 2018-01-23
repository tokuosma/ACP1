using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

    public float positionUpdateDelay;
    public float orientationUpdateDelay;

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
        GetComponent<FirstPersonController>().enabled = false;
        StartCoroutine("UpdatePosition");
        StartCoroutine("UpdateCameraOrientation");
#endif
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
            if (navMeshAgent.hasPath) {
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
