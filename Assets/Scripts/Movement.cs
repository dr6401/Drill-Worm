using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Movement : MonoBehaviour
{
    private Transform headTransform;
    public float moveSpeed = 5f;

    private float rotationOffset = 270;

    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        headTransform = GameObject.FindGameObjectWithTag("Head").transform;
        cam = Camera.main;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Position
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 mouseScreen = new Vector3(mousePosition.x, mousePosition.y, 0);
        Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0;
        
        Vector2 direction = (mouseWorldPosition - headTransform.position).normalized;
        Vector2 delta = mouseWorldPosition - headTransform.position;
        if (delta.magnitude > 0.1f)
        {
            //Debug.Log($"Big enough mouse distance, moving");
            headTransform.position += (Vector3) (direction * (moveSpeed * Time.deltaTime));
        }
        else
        {
            //Debug.Log($"Not big enough mouse distance, not moving");
        }
        

        
        // Rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        headTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
