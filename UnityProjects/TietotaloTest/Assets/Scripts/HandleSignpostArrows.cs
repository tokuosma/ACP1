using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine;

public class HandleSignpostArrows : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject signpostArrow;


    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.path.corners);
        if (Input.GetButtonDown("DrawPath"))
        {
            UpdateSignposts();

        }
    }
    public void UpdateSignposts()
    {
        if (true)
        {
            
            for (int i = 0; i <= agent.path.corners.Length - 1; i++)
            {
                Debug.Log("Hello" + i.ToString());
                if (i < agent.path.corners.Length - 1)
                {

                    Instantiate(signpostArrow, agent.path.corners[i] + new Vector3(0f, 1f, 0f), Quaternion.identity);
                    //signpostArrow = new GameObject("SignpostArrow" + i.ToString());
                    signpostArrow.GetComponent<SignpostArrow>().target.position = agent.path.corners[i + 1];

                }
                else
                {
                    signpostArrow = new GameObject("SignpostArrow" + i.ToString());
                    signpostArrow.GetComponent<SignpostArrow>().target.position = agent.path.corners[i];
                }
                
            }
        }

        else
        {

        }

    }
}
