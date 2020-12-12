using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private float _rotXSpeed = 30f;
    [SerializeField] private float _rotYSpeed = 30f;
    [SerializeField] private Transform _followTarget;
    [SerializeField] private float _smoothFollowFactor = 4f;

    private float _angleX = 0;
    private float _angleY = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        _angleX += mouseX * Time.deltaTime * _rotXSpeed;
        _angleY += mouseY * Time.deltaTime * _rotYSpeed;
        _angleY = Mathf.Clamp(_angleY, -85f, 85f);

        transform.position = _followTarget.position; // Vector3.Lerp(transform.position, followTarget.position, Time.fixedDeltaTime * smoothFollowFactor);
        transform.rotation = Quaternion.Euler(-_angleY, _angleX, 0);
    }
}
