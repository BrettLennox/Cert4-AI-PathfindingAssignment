using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleBehaviour : MonoBehaviour
{
    private AICollectorAgent _aiAgent;
    private DoorBehaviour _door;
    [SerializeField] private CollectibleType _collectibleType;

    private void Awake()
    {
        _aiAgent = GameObject.Find("AICollector").GetComponent<AICollectorAgent>();
        _door = GameObject.Find("Door").GetComponent<DoorBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var AICollector = other.tag == "AICollector";
        if (AICollector) //if AIAgent enters the trigger of this gameobject
        {
            Destroy(this.gameObject); //destroy this game object
        }
    }

    private void OnDestroy() //when this gameobject is destroyed
    {
        switch (_collectibleType)
        {
            case CollectibleType.GraveStone: //if CollectibleType is GraveStone
                if (_aiAgent != null)
                {
                    for (int i = 0; i < _aiAgent.Collectibles.Count - 1; i++)
                    {
                        if (_aiAgent.Collectibles[i].gameObject == this.gameObject) //if this gameobject matches with a gameobject in Collectibles list
                        {
                            _aiAgent.Collectibles.RemoveAt(i); //remove it at that point of the list
                        }
                    }
                }
                else return;
                break;
            case CollectibleType.Key: //if CollectibleType is Key
                if (_door != null)
                {
                    _door.OpenDoor(); //performs OpenDoor function
                }
                else return;
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
