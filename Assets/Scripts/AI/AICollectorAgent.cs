using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollectorAgent : AIAgent
{
    #region Variables
    [Header("Collectible Data")]
    [SerializeField] private Transform _key;
    [SerializeField] private GameObject _collectiblesParent;
    [SerializeField] private List<Transform> _collectibles = new List<Transform>();

    private AICollectorStates _currentState;
    #endregion
    #region Properties
    public List<Transform> Collectibles { get => _collectibles; }
    #endregion

    private void Awake()
    {
        foreach (Transform transform in _collectiblesParent.transform) //sets the collectibles List from the transforms within collectiblesParent gameobject
        {
            _collectibles.Add(transform);
        }
    }

    private void Start()
    {
        NextState();
    }

    private void NextState()
    {
        switch (_currentState) //StartsCoroutine depending on currentStated
        {
            case AICollectorStates.Loiter:
                StartCoroutine(LoiterRoutine());
                break;
            case AICollectorStates.CollectKey:
                StartCoroutine(CollectKeyRoutine());
                break;
            case AICollectorStates.CollectCollectible:
                StartCoroutine(CollectCollectibleRoutine());
                break;
            default:
                break;
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    private IEnumerator LoiterRoutine()
    {
        while (_currentState == AICollectorStates.Loiter)
        {
            if(_collectibles != null) //if collectibles are not null
            {
                _currentState = AICollectorStates.CollectCollectible; //sets currentState to be CollectCollectibles
            }
            MoveToTarget(this.transform); //sets MoveToTarget to be this gameobjects position
            yield return null;
        }
            NextState();
    }

    private IEnumerator CollectKeyRoutine()
    {
        while(_currentState == AICollectorStates.CollectKey)
        {
            if (_key != null) MoveToTarget(_key.transform); //if key is not null //sets MoveToTarget to keys transform
            else _currentState = AICollectorStates.CollectCollectible; //else //set currentState to be CollectCollectibles
            yield return null;
        }
            NextState();
    }

    private IEnumerator CollectCollectibleRoutine()
    {
        while(_currentState == AICollectorStates.CollectCollectible)
        {
            if(_collectibles[0] != null) //if collectibles is not null
            {
                MoveToTarget(_collectibles[0]); //sets MoveToTarget to be collectibles[0]
                if (!CheckPath()) //if CheckPath returns false
                {
                    _currentState = AICollectorStates.CollectKey; //sets currentState to be CollectKey
                }
            }
            else //else
            {
                _currentState = AICollectorStates.Loiter; //sets currentState to Loiter
            }
            yield return null;
        }
        NextState();
    }

    public enum AICollectorStates
    {
        Loiter,
        CollectKey,
        CollectCollectible
    }
}
