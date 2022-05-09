using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorBehaviour : MonoBehaviour
{
    #region Variables
    public LockState lockState;
    public List<Transform> childWalls;
    public NavMeshObstacle navMeshObstacle => GetComponent<NavMeshObstacle>();

    private bool isLocked = true;

    private AIAgent _aiAgent => GameObject.Find("AIAgent").GetComponent<AIAgent>();
    #endregion
    #region Properties
    public bool IsLocked { get => isLocked; set => value = isLocked; }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        NextState();
    }

    private void Update()
    {
        lockState = isLocked ? LockState.Closed : LockState.Open;
        isLocked = _aiAgent.keyCollected ? false : true;
    }

    private void NextState()
    {
        switch (lockState)
        {
            case LockState.Open:
                StartCoroutine(OpenRoutine());
                break;
            case LockState.Closed:
                StartCoroutine(CloseRoutine());
                break;
        }
    }

    private IEnumerator OpenRoutine()
    {
        ShouldShowDoor(false);
        yield return null;
        NextState();
    }

    private IEnumerator CloseRoutine()
    {
        ShouldShowDoor(true);
        yield return null;
        NextState();
    }

    private void ShouldShowDoor(bool shouldShow)
    {
        foreach (Transform transform in transform)
        {
            transform.gameObject.SetActive(shouldShow);
        }
    }

    public enum LockState
    {
        Open,
        Closed
    }
}
