using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollectorAgent : AIAgent
{
    #region Variables
    [Header("Collectible Data")]
    [SerializeField] private Transform _key;
    [SerializeField] private GameObject _graveStonesParent;
    [SerializeField] private List<Transform> _graveStones = new List<Transform>();
    private int _graveStoneIndex = 0;
    #endregion

    private void Awake()
    {
        foreach(Transform transform in _graveStonesParent.transform)
        {
            _graveStones.Add(transform);
        }
    }

    protected override void Update()
    {
        base.Update();
    }
}
