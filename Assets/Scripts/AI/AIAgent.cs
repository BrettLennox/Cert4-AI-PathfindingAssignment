using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    #region Variables
    public NavMeshAgent agent;

    public GameObject waypointsParent;
    public List<Transform> waypoints = new List<Transform>();
    private int waypointIndex = 0;
    #endregion
    #region Properties
    public int WaypointIndex { get => waypointIndex; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        foreach (Transform transform in waypointsParent.transform) //creates a list out of all child transforms within the waypointsParent gameobject
        {
            waypoints.Add(transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetWaypointIndex();
    }

    private void ResetWaypointIndex()
    {
        //resets the waypointIndex back to 0 if it goes above the size of the list
        if (waypointIndex > waypoints.Count - 1)
        {
            waypointIndex = 0;
        }
    }

    public void MoveToTarget(Transform target)
    {
        if (target != this.transform)
        {
            if (Vector3.Distance(transform.position, target.position) >= 1f) //if the distance between the AI and the target are above or equal to 1f
            {
                agent.SetDestination(target.position); //set the NavMeshAgent destination to the targets position
            }
            else //if the distance between the AI and the target is below 1f
            {
                waypointIndex++; //increment the waypointIndex value by 1
            }
        }
        else agent.SetDestination(target.position);
    }

    public bool CheckPath()
    {
        if (agent.path.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        return true;
    }
}
