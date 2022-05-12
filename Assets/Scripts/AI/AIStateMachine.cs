using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{

    #region Variables
    private AIStates currentState;
    private AIAgent _aiAgent => GetComponent<AIAgent>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        SetStartState();
        NextState();
    }

    private void SetStartState()
    {
        switch (_aiAgent.AITypes) //sets the currentState depending on what the AIType is
        {
            case AITypes.Collector:
                currentState = AIStates.CollectCollectible;
                break;
            case AITypes.Patroller:
                currentState = AIStates.Patrol;
                break;
        }
    }

    private void NextState() //starts the set coroutine based on the currentState set
    {
        switch (currentState)
        {
            case AIStates.Loiter:
                StartCoroutine(LoiterRoutine());
                break;
            case AIStates.CollectCollectible:
                StartCoroutine(CollectCollectibleRoutine());
                break;
            case AIStates.CollectKey:
                StartCoroutine(CollectKeyRoutine());
                break;
            case AIStates.Patrol:
                StartCoroutine(PatrolRoutine());
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

    private IEnumerator CollectCollectibleRoutine()
    {
        while (currentState == AIStates.CollectCollectible) //while current state is set to AIStates CollectCollectible
        {
            if (_aiAgent.Collectibles[0] != null) //if Waypoints list has active elements in it
            {
                _aiAgent.MoveToTarget(_aiAgent.Collectibles[_aiAgent.CollectibleIndex]); //sets the MoveToTarget to be the waypoints list at index waypointsIndex
                if (!_aiAgent.CheckPath()) //if AIAgent NavMeshPath is unreachable (based on CheckPath function in AIAgent)
                {
                    currentState = AIStates.CollectKey; //switch to CollectKey state
                }
            }
            else //if Waypoints list is null
            {
                currentState = AIStates.Loiter; //switch to Loiter state
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator CollectKeyRoutine()
    {
        while (currentState == AIStates.CollectKey) //while current state is set to AIStates CollectKey
        {
            if (_aiAgent.Key != null)
            {
                _aiAgent.MoveToTarget(_aiAgent.Key); //sets the MoveToTarget to be the key
            }
            if (_aiAgent.KeyCollected)
            {
                currentState = AIStates.CollectCollectible; //switch to CollectCollectible state
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator PatrolRoutine()
    {
        while (currentState == AIStates.Patrol) //while the current state is set to AIStates Patrol
        {
            Debug.Log("TEST");
            _aiAgent.MoveToTarget(_aiAgent.Waypoints[_aiAgent.WaypointIndex]); //sets the MoveToTarget to be the Waypoints list at WaypointIndex
            yield return null;
        }
        NextState();
    }
}
[System.Serializable]
public enum AIStates
{
    Loiter,
    CollectCollectible,
    CollectKey,
    Patrol
}