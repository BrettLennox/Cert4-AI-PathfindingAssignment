using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolAgent : AIAgent
{
    #region Variables
    [Header("Waypoint Data")]
    [SerializeField] private Transform _waypointsParent;
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] private int _waypointIndex = 0;

    private AIPatrolStates _currentState;

    private void Awake()
    {
        foreach (Transform transform in _waypointsParent.transform) //sets the waypoints List from the transforms within the waypointsParent gameobject
        {
            _waypoints.Add(transform);
        }
    }

    private void Start()
    {
        NextState();
    }

    private void NextState()
    {
        switch (_currentState) //StartsCoroutine depending on currentStated
        {
            case AIPatrolStates.Loiter:
                StartCoroutine(LoiterRoutine());
                break;
            case AIPatrolStates.Patrol:
                StartCoroutine(PatrolRoutine());
                break;
            default:
                break;
        }
    }

    #endregion
    protected override void Update()
    {
        base.Update();
        ResetWaypointIndex();
    }

    private IEnumerator LoiterRoutine()
    {
        while (_currentState == AIPatrolStates.Loiter)
        {
            
            MoveToTarget(this.transform); //sets the MoveToTarget to be this gameobject
            if (_waypoints != null) //if waypoints is not null
            {
                _currentState = AIPatrolStates.Patrol; //sets currentState to Patrol
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator PatrolRoutine()
    {
        while (_currentState == AIPatrolStates.Patrol)
        {
            float distance = Vector3.Distance(this.transform.position, _waypoints[_waypointIndex].transform.position);
            if(distance > 0.8f) //if the distance from this and waypoints[waypointIndex] is greater then 0.8f
            {
                if (!CheckPath()) //if Checkpath returns false
                {
                    _currentState = AIPatrolStates.Loiter; //sets currentState to Loiter
                }
                MoveToTarget(_waypoints[_waypointIndex]); //sets MoveToTarget to waypoints[waypointsIndex]
            }
            else //if the distance is not greater than 0.8f
            {
                _waypointIndex++; //increment waypointIndex
            }
            yield return null;
        }
        NextState();
    }

    private void ResetWaypointIndex()
    {
        if(_waypointIndex > _waypoints.Count - 1) //if waypointIndex is greater than waypoints count -1
        {
            _waypointIndex = 0; //sets the waypointIndex to 0
        }
    }

    public enum AIPatrolStates
    {
        Loiter,
        Patrol
    }
}
