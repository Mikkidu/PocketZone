
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] protected IHealth owner;
    [SerializeField] private Transform _fillArea;
    [SerializeField] private GameObject _healthBar;

    protected int maxValue;

    protected virtual void Awake()
    {
        owner = GetComponent<IHealth>();
        owner.AddHPEvent = UpdateHP;
        maxValue = owner.getMaxHealth;
    }

    public virtual void UpdateHP(int health)
    {
        if (health < 0 || health == maxValue) _healthBar.SetActive(false);
        else if (!_healthBar.activeSelf) _healthBar.SetActive(true);
        float persent = (float)health / maxValue;
        Vector2 tempVector = _fillArea.localPosition;
        tempVector.x = -0.5f + persent / 2;
        _fillArea.localPosition = tempVector;

        tempVector = _fillArea.localScale;
        tempVector.x = persent;
        _fillArea.localScale = tempVector;
    }
}
