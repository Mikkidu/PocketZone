using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Unit
{
    [SerializeField] private Loot _loot;
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

    protected override void Death()
    {
        base.Death();
        Instantiate(_loot, transform.position, Quaternion.identity).Initialize("IT02", 1);
    }
}
