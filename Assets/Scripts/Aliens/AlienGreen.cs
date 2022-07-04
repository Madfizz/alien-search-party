using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGreen : AlienBase
{
    [SerializeField]
    private int abductTime;
    [SerializeField]
    private int resistTime;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float resistSpeed;

    void Update()
    {
        if (!isUnderBeam)
        {
            Move();
            timer = 0;
        }
        else
        {
            if (timer < resistTime)
            {
                Resist();
            }
            else if (timer > resistTime && timer < resistTime + abductTime)
            {
                Abduct();
            }
            else 
            {
                Destroy();
            }

            timer += Time.deltaTime;
        }
    }

    protected override void Move()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    protected override void Abduct()
    {
        navMeshAgent.isStopped = true;
    }

    private void Resist()
    {
        navMeshAgent.speed = resistSpeed;
    }
}