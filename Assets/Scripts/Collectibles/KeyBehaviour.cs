using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var AIAgent = other.GetComponent<AIAgentOld>();
        if (AIAgent)
        {
            AIAgent._keyCollected = true;
            Destroy(this.gameObject);
        }
    }
}
