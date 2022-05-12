using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorBehaviour : MonoBehaviour
{
    #region Variables
    public LockState lockState;

    private bool isLocked = true;

    private AIAgent _aiAgent => GameObject.FindGameObjectWithTag("CollectorAgent").GetComponent<AIAgent>();
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
        lockState = isLocked ? LockState.Closed : LockState.Open; //lockState is set based on isLocked value. True being closed. False being open
        isLocked = _aiAgent.KeyCollected ? false : true; //isLocked is set based on the KeyCollected value from AIAgent. True being isLocked is false. False being isLocked is true
    }

    private void NextState() //starts the coroutine based on the lockState set
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

    private IEnumerator OpenRoutine() //if the door is in OpenRoutine sets ShouldShowDoor to false
    {
        ShouldShowDoor(false);
        yield return null;
        NextState();
    }

    private IEnumerator CloseRoutine() //if the door is in CloseRoutine sets ShouldShowDoor to true
    {
        ShouldShowDoor(true);
        yield return null;
        NextState();
    }

    private void ShouldShowDoor(bool shouldShow)
    {
        transform.gameObject.SetActive(shouldShow); //sets active in relation to bool passed in
    }

    public enum LockState
    {
        Open,
        Closed
    }
}
