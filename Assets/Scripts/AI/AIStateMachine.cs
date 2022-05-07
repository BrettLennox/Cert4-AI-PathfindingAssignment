using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine : MonoBehaviour
{

    #region Variables
    public AIStates currentState;
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
        while (currentState == AIStates.Loiter)
        {
            Debug.Log("LOITER");
            yield return null;
        }
        NextState();
    }

    private IEnumerator CollectRoutine()
    {
        while (currentState == AIStates.Collect)
        {
            Debug.Log("COLLECT");
            yield return null;
        }
        NextState();
    }

    private IEnumerator CheckRoutine()
    {
        while (currentState == AIStates.Check)
        {
            Debug.Log("Check");
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