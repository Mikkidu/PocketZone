using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IHealth
{
    [SerializeField] protected float attackInterval = 0.5f;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float agrDistance;

    [SerializeField] private int _maxHealth;
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] protected int _unitDamage;
    [SerializeField] private string _targetTag;
    [SerializeField] private ContactFilter2D _targetsFilter;
    [SerializeField] private Collider2D _meleAttackArea;
    [SerializeField] private bool _isMeleAttack;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _shootPoint;


    private IHealth.OnHpChanged OnHpChangedCall;
    public IHealth.OnHpChanged AddHPEvent
    {
        set { OnHpChangedCall += value; }
    }

    public bool isAlive => !isDead;
    public int getMaxHealth => _maxHealth;
    public Transform getShootPoint => _shootPoint;

    protected float attackTimer;

    private Rigidbody2D _rb;
    private List<Collider2D> _findTargets;
    private Animator _animator;
    private int _health;
    private string _ammoID = "IT03";

    public bool isDead;
    public Collider2D currentTarget;
    public bool isLokingRight = true;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = _maxHealth;
    }

    protected virtual void Start()
    {
        StartCoroutine("FindEnemies");
        if (OnHpChangedCall != null) OnHpChangedCall.Invoke(_health);
    }

    protected virtual void Update()
    {
        if (!isDead)
        { 
            Attack();
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (OnHpChangedCall != null) OnHpChangedCall.Invoke(_health);
        if (_health <= 0) Death();
    }

    protected virtual void Death()
    {
        _animator.SetBool("IsDead", true);
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
    }

    protected void Move(Vector2 direction)
    {
        if (direction != Vector2.zero) _animator.SetBool("IsMoving", true);
        else _animator.SetBool("IsMoving", false);

        if (_isMeleAttack || currentTarget == null)
        {
            FlipCharacter(direction);
        }
        _animator.SetFloat("MoveY", direction.y);
        _rb.velocity = direction * _moveSpeed;
    }

    public void FlipCharacter(Vector2 direction)
    {
        if (isLokingRight && direction.x < -0.05f || !isLokingRight && direction.x > 0.05f)
        {
            isLokingRight = !isLokingRight;
            Vector2 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    protected float GetDistance(Collider2D target)
    {
        return (transform.position - target.bounds.center).magnitude;
    }

    public void Attack()
    {
        if (attackTimer <= Time.realtimeSinceStartup && currentTarget != null)
        {
            if (GetDistance(currentTarget) < attackDistance)
            {
                if (!_isMeleAttack)
                {
                    Shoot();
                }
                else _animator.SetTrigger("Attack");
                attackTimer = Time.realtimeSinceStartup + attackInterval;
            }
        }
    }

    private void Shoot()
    {
        if (Inventory.instance.CheckItemByID(_ammoID))
        {
            Vector3 toTarget = currentTarget.bounds.center - _shootPoint.position;
            float angle = Vector3.SignedAngle(Vector3.right, toTarget, Vector3.forward);
            Projectile bullet = Instantiate(_projectile, _shootPoint.position, Quaternion.Euler(Vector3.forward * angle));
            bullet.Initialize(_unitDamage, gameObject.tag);
            Inventory.instance.Spend(_ammoID, 1);
        }
    }

    IEnumerator FindEnemies()
    {
        _findTargets = new List<Collider2D>();
        float currentDistance;
        while (!isDead)
        {
            Physics2D.OverlapCircle(transform.position, agrDistance, _targetsFilter, _findTargets);
            if (_findTargets.Count > 0)
            {
                _animator.SetBool("HaveTarget", true);
                if (currentTarget == null || !_findTargets.Contains(currentTarget))
                {
                    currentTarget = _findTargets[0];
                }
                currentDistance = GetDistance(currentTarget);
                for (int i = 1; i < _findTargets.Count; i++)
                {
                    if (currentDistance > GetDistance(_findTargets[i]))
                    {
                        
                        currentTarget = _findTargets[i];
                        currentDistance = GetDistance(currentTarget);
                    }
                }
            }
            else
            {
                currentTarget = null;
                _animator.SetBool("HaveTarget", false);
            } 
            yield return new WaitForSeconds(0.2f);
        }
        currentTarget = null;
    }

}
