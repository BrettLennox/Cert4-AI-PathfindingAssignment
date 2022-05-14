using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIAgent : MonoBehaviour
{
    #region Variables
    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();
    private Animator _anim => GetComponentInChildren<Animator>();
    #endregion

    protected virtual void Update()
    {
        AnimationState();
    }

    public virtual void MoveToTarget(Transform target) => _agent.SetDestination(target.position);

    public virtual bool CheckPath()
    {
        if (_agent.path.status != NavMeshPathStatus.PathComplete)
        {
            return false;
        }
        return true;
    }

    protected virtual void AnimationState() => _anim.SetBool("isWalking", _agent.velocity != Vector3.zero);
}
