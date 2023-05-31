using UnityEngine;
public interface IHealth
{
    bool isAlive { get; }
    Transform unitTransform {get;}
    void TakeDamage(int damage);
}
