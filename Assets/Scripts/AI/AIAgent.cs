using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIAgent : MonoBehaviour
{
    #region Variables
    protected NavMeshAgent _agent => GetComponent<NavMeshAgent>();
    private Animator _anim => GetComponentInChildren<Animator>();
    #endregion

    protected virtual void Update()
    {
        AnimationState();
    }

    protected virtual void MoveToTarget(Transform target) => _agent.SetDestination(target.position); //sets the Agents destination to the passed in target

    protected virtual bool CheckPath() //returns based on Agent Path Status
    {
        if (_agent.path.status != NavMeshPathStatus.PathComplete) 
        {
            return false; 
        }
        return true;
    }

    protected virtual void AnimationState() => _anim.SetBool("isWalking", _agent.velocity != Vector3.zero); //sets the animation bool based on the agents velocity
}
