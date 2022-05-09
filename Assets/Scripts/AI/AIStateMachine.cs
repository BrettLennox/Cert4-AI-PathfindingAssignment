using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{

    #region Variables
    public AIStates currentState;
    private AIAgent _aiAgent => GetComponent<AIAgent>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        NextState();
    }

    private void NextState()
    {
        switch (currentState)
        {
            case AIStates.Loiter:
                StartCoroutine(LoiterRoutine());
                break;
            case AIStates.Collect:
                StartCoroutine(CollectRoutine());
                break;
            case AIStates.Check:
                StartCoroutine(CheckRoutine());
                break;
        }
    }

    private IEnumerator LoiterRoutine()
    {
        while (currentState == AIStates.Loiter) //while current state is set to AIStates Loiter
        {
            _aiAgent.MoveToTarget(this.transform); //sets the MoveToTarget to be itself
            yield return null;
        }
        NextState();
    }

    private IEnumerator CollectRoutine()
    {
        while (currentState == AIStates.Collect) //while current state is set to AIStates Collect
        {
            _aiAgent.MoveToTarget(_aiAgent.waypoints[_aiAgent.WaypointIndex]); //sets the MoveToTarget to be the waypoints list at index waypointsIndex
            if (!_aiAgent.CheckPath()) //if the path is unreachable
            {
                currentState = AIStates.Check; //switch to the Check state
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator CheckRoutine() //while current state is set to AIStates Check
    {
        while (currentState == AIStates.Check)
        {
            _aiAgent.MoveToTarget(transform);
            Debug.Log("CHECK");
            yield return null;
        }
        NextState();
    }
}
[System.Serializable]
public enum AIStates
    {
        Loiter,
        Collect,
        Check
    }