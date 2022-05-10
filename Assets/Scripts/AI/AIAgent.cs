using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AIStateMachine))]
public class AIAgent : MonoBehaviour
{
    #region Variables
    [SerializeField] private NavMeshAgent _agent => GetComponent<NavMeshAgent>();

    [SerializeField] private GameObject waypointsParent;
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] private int _waypointIndex = 0;

    [SerializeField] private Transform _key;
    public bool _keyCollected;
    [SerializeField] private Animator _animator => GetComponentInChildren<Animator>();
    #endregion
    #region Properties
    public int WaypointIndex { get => _waypointIndex; }
    public Transform Key { get => _key; }
    public List<Transform> Waypoints { get => _waypoints; }
    public bool KeyCollected { get => _keyCollected;}
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        foreach (Transform transform in waypointsParent.transform) //creates a list out of all child transforms within the waypointsParent gameobject
        {
            _waypoints.Add(transform);
        }
        _key = GameObject.Find("Key").transform;
    }

    private void Update()
    {
        AnimationState();
    }

    public void MoveToTarget(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    public bool CheckPath()
    {
        if (_agent.path.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        return true;
    }

    private void AnimationState() => _animator.SetBool("isWalking", _agent.velocity != Vector3.zero);
}
