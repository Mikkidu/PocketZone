using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public int damage = 0;
    public float fireInterval = 0;
}