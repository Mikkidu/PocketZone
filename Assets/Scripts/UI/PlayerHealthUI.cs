using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : HealthUI
{
    [SerializeField] private Slider _healthUI;

    protected override void Awake()
    {
        base.Awake();
        _healthUI.maxValue = maxValue;
    }


    public override void UpdateHP(int health)
    {
        
        _healthUI.value = health;
    }
}
