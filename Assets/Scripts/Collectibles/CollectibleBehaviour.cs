using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    private AIAgent _aiAgent => GameObject.FindWithTag("CollectorAgent").GetComponent<AIAgent>();
    [SerializeField] private CollectibleType _collectibleType;

    private void OnTriggerEnter(Collider other)
    {
        var AIAgent = other.GetComponent<AIAgent>();
        if (AIAgent) //if AIAgent enters the trigger of this gameobject
        {
            Destroy(this.gameObject); //destroy this game object
        }
    }

    private void OnDestroy() //when this gameobject is destroyed
    {
        switch (_collectibleType)
        {
            case CollectibleType.GraveStone: //if CollectibleType is GraveStone
                for (int i = 0; i < _aiAgent.Collectibles.Count - 1; i++)
                {
                    if (_aiAgent.Collectibles[i].gameObject == this.gameObject) //if this gameobject matches with a gameobject in Collectibles list
                    {
                        _aiAgent.Collectibles.RemoveAt(i); //remove it at that point of the list
                    }
                }
                break;
            case CollectibleType.Key: //if CollectibleType is Key
                _aiAgent._keyCollected = true; //sets KeyCollected to true;
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
