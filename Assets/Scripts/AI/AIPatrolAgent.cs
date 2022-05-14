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
        foreach (Transform transform in _waypointsParent.transform)
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
        switch (_currentState)
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
            
            MoveToTarget(this.transform);
            if (_waypoints != null && CheckPath())
            {
                _currentState = AIPatrolStates.Patrol;
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
            if(distance > 0.8f)
            {
                if (!CheckPath())
                {
                    _currentState = AIPatrolStates.Loiter;
                }
                MoveToTarget(_waypoints[_waypointIndex]);
            }
            else
            {
                _waypointIndex++;
            }
            yield return null;
        }
        NextState();
    }

    private void ResetWaypointIndex()
    {
        if(_waypointIndex > _waypoints.Count - 1)
        {
            _waypointIndex = 0;
        }
    }

    public enum AIPatrolStates
    {
        Loiter,
        Patrol
    }
}
