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
        foreach (Transform transform in _collectiblesParent.transform)
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
        switch (_currentState)
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
            if(_collectibles[0] != null)
            {
                _currentState = AICollectorStates.CollectCollectible;
            }
            MoveToTarget(this.transform);
            yield return null;
        }
            NextState();
    }

    private IEnumerator CollectKeyRoutine()
    {
        while(_currentState == AICollectorStates.CollectKey)
        {
            if (_key != null) MoveToTarget(_key.transform);
            else _currentState = AICollectorStates.CollectCollectible;
            yield return null;
        }
            NextState();
    }

    private IEnumerator CollectCollectibleRoutine()
    {
        while(_currentState == AICollectorStates.CollectCollectible)
        {
            if(_collectibles[0] != null)
            {
                MoveToTarget(_collectibles[0]);
                if (!CheckPath())
                {
                    _currentState = AICollectorStates.CollectKey;
                }
            }
            else
            {
                _currentState = AICollectorStates.Loiter;
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
