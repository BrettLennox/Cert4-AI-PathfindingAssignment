using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorBehaviour : MonoBehaviour
{
    public void OpenDoor()
    {
        transform.gameObject.SetActive(false);
    }
}
