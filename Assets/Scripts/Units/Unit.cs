using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IHealth
{
    [SerializeField] protected int health;
    [SerializeField] public bool isDead;
    [SerializeField] protected float moveSpeed = 5;
    [SerializeField] protected int unitDamage;
    [SerializeField] protected string targetTag;
    [SerializeField] protected float attackInterval = 0.5f;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float agrDistance;
    [SerializeField] protected ContactFilter2D targetsFilter;
    [SerializeField] List<Collider2D> findTargets;

    public bool isAlive => !isDead;
    public Transform unitTransform => transform;

    protected Rigidbody2D rb;
    [SerializeField] protected List<IHealth> targets;
    [SerializeField] protected Collider2D currentTarget;
    protected float attackTimer;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targets = new List<IHealth>();
    }
    private void Start()
    {
        StartCoroutine("FindEnemies");
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            //CheckTargets();
            Attack();
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Death();
    }

    protected void Death()
    {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
    }

    protected void Move(Vector2 direction)
    {
        rb.velocity = direction * moveSpeed;
    }


    protected float GetDistance(Collider2D target)
    {
        return (transform.position - target.bounds.center).magnitude;
    }

    protected void Attack()
    {
        if (attackTimer <= Time.realtimeSinceStartup && currentTarget != null)
        {
            if (GetDistance(currentTarget) < attackDistance)
            {
                //Debug.Log("Attack " + gameObject +" " + currentTarget);
                currentTarget.gameObject.GetComponent<Unit>().TakeDamage(unitDamage);
                attackTimer = Time.realtimeSinceStartup + attackInterval;
            }
        }
    }

    IEnumerator FindEnemies()
    {
        findTargets = new List<Collider2D>();
        float currentDistance;
        while (true)
        {
            Physics2D.OverlapCircle(transform.position, agrDistance, targetsFilter, findTargets);
            if (findTargets.Count > 0)
            {
                if (currentTarget == null || !currentTarget.enabled)
                {
                    currentTarget = findTargets[0];
                }
                currentDistance = GetDistance(currentTarget);
                for (int i = 1; i < findTargets.Count; i++)
                {
                    Debug.Log(currentDistance + " " + GetDistance(findTargets[i]));
                    if (currentDistance > GetDistance(findTargets[i]))
                    {
                        
                        currentTarget = findTargets[i];
                        currentDistance = GetDistance(currentTarget);
                    }
                }
            }
            else currentTarget = null;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
