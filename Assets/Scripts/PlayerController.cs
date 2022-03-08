using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRigidbody;
    private float _force = 8f;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        // Process left/right movement
        if (Input.GetKey(KeyCode.A))
        {
            _playerRigidbody.AddForce(Vector3.left * _force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _playerRigidbody.AddForce(Vector3.right * _force);
        }
    }
}
