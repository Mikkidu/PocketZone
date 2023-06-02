using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleAttack : MonoBehaviour
{
    [SerializeField] private string _selfTag;
    [SerializeField] private int _damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(_selfTag) && collision.gameObject.TryGetComponent<IHealth>(out IHealth target))
        {
            target.TakeDamage(_damage);
        }
    }

}
