using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform target;
    void LateUpdate()
    {
        Vector3 position = target.position;
        position.z = transform.position.z;
        transform.position = position;
    }
}
