using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AIStateMachine))]
public class AIAgentOld : MonoBehaviour
{
    #region Variables
    [SerializeField] private AITypes _aITypes;

    [Header("Collectible Data")]
    [SerializeField] private GameObject _collectiblesParent;
    private List<Transform> _collectibles = new List<Transform>();
    private int _collectiblesIndex = 0;

    [Header("Key Data")]
    [SerializeField] private Transform _key;
    public bool _keyCollected;

    [Header("Patrol Waypoints Data")]
    [SerializeField] private GameObject _waypointsParent;
    private List<Transform> _waypoints = new List<Transform>();
    private int _waypointIndex = 0;

    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();
    private Animator _animator => GetComponentInChildren<Animator>();
    #endregion
    #region Properties
    public AITypes AITypes { get => _aITypes; }
    public List<Transform> Collectibles { get => _collectibles; }
    public int CollectibleIndex { get => _collectiblesIndex; }
    public Transform Key { get => _key; }
    public bool KeyCollected { get => _keyCollected; set => _keyCollected = value; }
    public List<Transform> Waypoints { get => _waypoints; }
    public int WaypointIndex { get => _waypointIndex; }
    #endregion


    private void Awake()
    {
        switch (_aITypes) //passes info to CreateList function depending on what AIType is set
        {
            case AITypes.Collector:
                CreateList(_collectiblesParent, _collectibles);
                break;
            case AITypes.Patroller:
                CreateList(_waypointsParent, _waypoints);
                break;
        }
    }

    private void CreateList(GameObject listParent, List<Transform> list)
    {
        foreach (Transform transform in listParent.transform) //creates a list out of all child transforms within the listParent gameobject
        {
            list.Add(transform);
        }
    }

    private void Update()
    {
        ResetWaypointIndex();
        AnimationState();
    }

    private void ResetWaypointIndex() //if the waypointsIndex goes above the length of the waypoints list set it to 0
    {
        if (_waypointIndex > _waypoints.Count - 1)
        {
            _waypointIndex = 0;
        }
    }

    public void MoveToTarget(Transform target) //sets the MoveToTarget target to be the passed in Transform. Function changes depending on AIType
    {
        if (_aITypes == AITypes.Collector) //if Collector just set the Agent Destination to the passed in transform
        {
            _agent.SetDestination(target.position);
        }
        else if (_aITypes == AITypes.Patroller) //if Patroller sets the Agent Destination to the passed in transform but if it gets close enough it will increment waypointIndex
        {
            float distance = Vector3.Distance(transform.position, _waypoints[_waypointIndex].position);
            if (distance >= 0.8f)
            {
                _agent.SetDestination(target.position);
            }
            else
            {
                _waypointIndex++;
            }
        }
    }

    public bool CheckPath() //returns true or false depending on whether the Agent Path Status can be completed
    {
        if (_agent.path.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        return true;
    }

    private void AnimationState() => _animator.SetBool("isWalking", _agent.velocity != Vector3.zero); //sets the Animator Bool "isWalking" based on the Agent velocity
}
[System.Serializable]
public enum AITypes
{
    Collector,
    Patroller,
}
