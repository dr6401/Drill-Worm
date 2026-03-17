using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Movement : MonoBehaviour
{
    private Transform headTransform;
    public float maxMoveSpeed = 5f;
    public float acceleration = 15f;
    private float slowRadius = 1f;

    private Vector2 velocity;
    private Vector3 worldPosition;

    private float maxRotationSpeed = 360;
    private float rotationOffset = 270;

    private WormAnimation wormAnimation;
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        headTransform = GameObject.FindGameObjectWithTag("Head").transform;
        wormAnimation = gameObject.GetComponent<WormAnimation>();
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
        worldPosition = cam.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        
        Vector2 delta = worldPosition - headTransform.position;
        /*if (delta.magnitude > 0.1f)
        {
            //Debug.Log($"Big enough mouse distance, moving");
            Move();
            Rotate();
        }
        else
        {
            //Debug.Log($"Not big enough mouse distance, not moving");
        }*/
        Move();
        Rotate();
        wormAnimation.AnimateBody();
    }

    void Move()
    {
        Vector2 toTarget = worldPosition - headTransform.position;
        float distance = toTarget.magnitude;

        float targetSpeed = maxMoveSpeed;
        if (distance < slowRadius)
        {
            targetSpeed = maxMoveSpeed * (distance / slowRadius);
        }
        Vector2 desiredVelocity = toTarget.normalized * targetSpeed;
        velocity = Vector2.MoveTowards(velocity, desiredVelocity, acceleration * Time.deltaTime);
        
        headTransform.position += (Vector3) (velocity * Time.deltaTime);
    }

    void Rotate()
    {
        Vector2 direction = (worldPosition - headTransform.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + rotationOffset;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        
        
        headTransform.rotation = Quaternion.RotateTowards(headTransform.rotation, targetRotation, maxRotationSpeed * Time.deltaTime);
    }
}
