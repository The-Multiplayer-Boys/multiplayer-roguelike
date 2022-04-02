using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position;
    }

    void Update()
    {
        transform.position = target.position + offset;
    }
}
