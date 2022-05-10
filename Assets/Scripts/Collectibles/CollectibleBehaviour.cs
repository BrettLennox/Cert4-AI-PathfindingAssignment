using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    private AIAgent _aiAgent => GameObject.Find("Agent").GetComponent<AIAgent>();
    [SerializeField] private CollectibleType _collectibleType;
    private void OnTriggerEnter(Collider other)
    {
        var AIAgent = other.GetComponent<AIAgent>();
        if (AIAgent)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        switch (_collectibleType)
        {
            case CollectibleType.GraveStone:
                for (int i = 0; i < _aiAgent.Waypoints.Count - 1; i++)
                {
                    if (_aiAgent.Waypoints[i].gameObject == this.gameObject)
                    {
                        _aiAgent.Waypoints.RemoveAt(i);
                    }
                }
                break;
            case CollectibleType.Key:
                _aiAgent._keyCollected = true;
                break;
        }
    }
    [System.Serializable]
    public enum CollectibleType
    {
        GraveStone,
        Key
    }
}
