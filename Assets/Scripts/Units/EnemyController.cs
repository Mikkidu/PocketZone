using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Unit
{

    private void FixedUpdate()
    {
        if (!isDead && currentTarget != null)
        {
            if (GetDistance(currentTarget) > attackDistance)
            {

                Move((currentTarget.bounds.center - transform.position).normalized);
            }
            //Debug.Log(currentTarget.bounds.center + " " + currentTarget);
        }

    }
}
