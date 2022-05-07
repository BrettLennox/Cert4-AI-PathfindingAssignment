using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    #region Variables
    public NavMeshAgent agent;

    public GameObject waypointsParent;
    public List<Transform> waypoints = new List<Transform>();
    public int waypointIndex = 0;

    public float speed = 3f;

    private AIStateMachine _aiStateMachine => GetComponent<AIStateMachine>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        foreach (Transform transform in waypointsParent.transform) //creates a list out of all child transforms within the waypointsParent gameobject
        {
            waypoints.Add(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetWaypointIndex();
        if (_aiStateMachine.currentState == AIStates.Collect) //if the AIStateMachine state is set to Collect
        {
            MoveToTarget(waypoints[waypointIndex]);//performs MoveTo function with the target set to the waypoints list
        }
        else if (_aiStateMachine.currentState == AIStates.Loiter) //if the AIStateMachine state is set to Loiter
        {
            MoveToTarget(transform); //performs MoveTo function with the target set to self as to not move
        }
    }

    private void ResetWaypointIndex()
    {
        //resets the waypointIndex back to 0 if it goes above the size of the list
        if (waypointIndex > waypoints.Count - 1)
        {
            waypointIndex = 0;
        }
    }

    private void MoveToTarget(Transform target)
    {
        if (target != this.transform)
        {
            if (Vector3.Distance(transform.position, target.position) >= 1f) //if the distance between the AI and the target are above or equal to 1f
            {
                agent.destination = target.position; //set the NavMeshAgent destination to the targets position
            }
            else //if the distance between the AI and the target is below 1f
            {
                waypointIndex++; //increment the waypointIndex value by 1
            }
        }
    }
}
