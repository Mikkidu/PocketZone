using UnityEngine;
public class PlayerController : Unit
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private SpriteRenderer _weaponSprite;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            Move(new Vector2(_joystick.Horizontal, _joystick.Vertical).normalized);
        }
        
    }


    public void SetWeapon(Weapon weapon)
    {
        _weapon = weapon;
        _unitDamage = weapon.damage;
        attackInterval = weapon.fireInterval;
    }
}