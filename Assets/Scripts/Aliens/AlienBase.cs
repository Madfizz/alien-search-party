using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AlienBase : MonoBehaviour
{
    protected NavMeshAgent NavMeshAgent;
    protected float Timer;
    [SerializeField]
    protected float AbductTime;
    [SerializeField]
    protected float Speed;
    
    // If Alien has reached target
    [HideInInspector]
    public bool TargetReached;
    [HideInInspector]
    // If Alien is under player's UFO abduction beam
    public bool IsUnderBeam;

    public float DetectionTime;

    protected void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        IsUnderBeam = false;
        TargetReached = false;
        Timer = 0;
    }

    protected virtual void Move()
    {
        NavMeshAgent.isStopped = false;
        NavMeshAgent.speed = Speed;
    }

    protected void Abduct()
    {
        NavMeshAgent.isStopped = true;
    }

    protected void ReachedTarget()
    {
        Invoke("WasDetected", DetectionTime);
    }

    protected void WasDetected()
    {
        gameObject.SetActive(false);
        GameManager.Instance.PlayState.Failed = true;
        GameManager.Instance.GMStateMachine.ChangeState(GameManager.Instance.EndLevelState);
    }

    protected void OnDisable()
    {
        CancelInvoke();
    }
    
    protected void Caught()
    {
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        IsUnderBeam = false;
        TargetReached = false;
        Timer = 0;
    }

    public void Spawn(Transform spawnPoint, Transform destination)
    {
        gameObject.transform.position = spawnPoint.position;
        gameObject.SetActive(true);
        NavMeshAgent.SetDestination(destination.position);
        NavMeshAgent.isStopped = false;
    }
}

