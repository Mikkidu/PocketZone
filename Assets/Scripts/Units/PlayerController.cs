using UnityEngine;
public class PlayerController : Unit
{
    [SerializeField] private FloatingJoystick _joystick;


    private void FixedUpdate()
    {
        if (!isDead)
        {
            Move(new Vector2(_joystick.Horizontal, _joystick.Vertical).normalized);
        }
        
    }
}