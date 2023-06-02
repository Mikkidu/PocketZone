using UnityEngine;

public class EnemyController : Unit
{
    [SerializeField] private Loot _loot;
    private void FixedUpdate()
    {
        if (!isDead && currentTarget != null)
        {
            if (GetDistance(currentTarget) > attackDistance && attackTimer < Time.realtimeSinceStartup)
            {
                Move((currentTarget.bounds.center - transform.position).normalized);
            }
            else Move(Vector2.zero);
        }

    }

    protected override void Death()
    {
        base.Death();
        Instantiate(_loot, transform.position, Quaternion.identity).Initialize("IT03", 10);
    }
}
