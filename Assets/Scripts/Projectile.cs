using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] private float _speed;

    private string _selfTag;
    private int _damage;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = transform.right * _speed;
        Destroy(gameObject, 1);
    }

    public void Initialize(int damage, string tag)
    {
        _damage = damage;
        _selfTag = tag;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(_selfTag) && collision.gameObject.TryGetComponent<IHealth>(out IHealth target))
        {
            target.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
