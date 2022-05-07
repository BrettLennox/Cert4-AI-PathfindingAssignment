using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    #region Variables
    public NavMeshAgent agent;
    public Transform waypoint;

    public float speed = 3f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(agent.destination);

        if (Vector3.Distance(transform.position, waypoint.position) > 0.1f)
        {
            agent.destination = waypoint.position;
        }
        else { agent.destination = Vector3.zero; }
    }
}
