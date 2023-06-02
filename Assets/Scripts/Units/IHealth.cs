using UnityEngine;
public interface IHealth
{
    bool isAlive { get; }
    int getMaxHealth { get; }
    void TakeDamage(int damage);
    delegate void OnHpChanged(int hp);
    OnHpChanged AddHPEvent { set; }

}
