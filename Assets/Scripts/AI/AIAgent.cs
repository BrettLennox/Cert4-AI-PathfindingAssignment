using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIAgent : MonoBehaviour
{
    #region Variables
    private float _walkSpeed = 3f, _slowWalkSpeed = 1.5f;
    protected NavMeshAgent _agent => GetComponent<NavMeshAgent>();
    private Animator _anim => GetComponentInChildren<Animator>();
    #endregion

    protected virtual void Update()
    {
        AnimationState();
        AdjustMoveSpeed();
    }

    protected virtual void MoveToTarget(Transform target) => _agent.SetDestination(target.position); //sets the Agents destination to the passed in target

    protected virtual void AdjustMoveSpeed() //adjusts walk speed based off of the NavMeshMask registered when moving on the NavMesh
    {
        NavMeshHit navHit;
        _agent.SamplePathPosition(-1, 0.0f, out navHit);

        int SlowWalkMask = 1 << NavMesh.GetAreaFromName("SlowWalk");

        if (navHit.mask == SlowWalkMask) 
        { 
            _agent.speed = _slowWalkSpeed; 
        }
        else
        {
            _agent.speed = _walkSpeed;
        }
    }

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
