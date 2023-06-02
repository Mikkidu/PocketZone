using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTarget : MonoBehaviour
{
    private Animator _animator;
    private Unit _controller;
    private Transform _shootPoint;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<Unit>();
        _shootPoint = _controller.getShootPoint;
    }

    void Update()
    {
        if(_controller.currentTarget != null)
        {
            Vector3 direction = (_controller.currentTarget.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(Vector3.up, direction);
            _shootPoint.localRotation = Quaternion.Euler(Vector3.back * angle);
            _animator.SetFloat("LookAtY", direction.y);
            _controller.FlipCharacter(direction);
        }
        
    }



}
