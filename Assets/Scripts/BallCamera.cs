using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera : MonoBehaviour
{
    private GameObject _playerBall;

    private Transform _ballTransform;
    private Transform _cameraTransform;

    private Vector3 _offsetVector = new Vector3(0f, 5f, -4f);


    private void Awake()
    {
        _playerBall = GameObject.Find("Player Ball");
        
        _ballTransform = _playerBall.transform;
        _cameraTransform = this.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform.position = _ballTransform.position + _offsetVector;
        _cameraTransform.rotation = Quaternion.Euler(40f, 0f, 0f);
    }

    // Camera follows the playerBall
    void Update()
    {
        _cameraTransform.position = _ballTransform.position + _offsetVector;
    }
}
