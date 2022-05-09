using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{

    #region Variables
    public AIStates currentState;
    private AIAgent _aiAgent => GetComponent<AIAgent>();
    private DoorBehaviour door => GetComponent<DoorBehaviour>();
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
            case AIStates.CollectCollectible:
                StartCoroutine(CollectCollectibleRoutine());
                break;
            case AIStates.CollectKey:
                StartCoroutine(CollectKeyRoutine());
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
            _aiAgent.MoveToTarget(_aiAgent.waypoints[_aiAgent.WaypointIndex]); //sets the MoveToTarget to be the waypoints list at index waypointsIndex
            if (!_aiAgent.CheckPath()) //if the path is unreachable
            {
                currentState = AIStates.CollectKey; //switch to the Check state
            }
            yield return null;
        }
        NextState();
    }

    private IEnumerator CollectKeyRoutine() 
    {
        while (currentState == AIStates.CollectKey) //while current state is set to AIStates CollectKey
        {
            if (_aiAgent.keyCollected)
            {
                currentState = AIStates.CollectCollectible; //switch to CollectCollectible state
                NextState();
            }
            _aiAgent.MoveToTarget(_aiAgent.key); //sets the MoveToTarget to be the key
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
        UnlockDoor
    }