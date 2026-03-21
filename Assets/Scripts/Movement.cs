using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    
    private Transform headTransform;
    [Header("Speed")]
    public float currentSpeed;
    public float moveSpeed = 5f;
    public float normalMoveSpeed;
    public float originalMoveSpeed;
    public float acceleration = 15f;
    private float slowRadius = 1f;
    
    [Header("Dash")]
    public float dashMoveSpeedIncreaseMultiplier = 1.5f;
    public float dashCooldown = 3f;
    private float timeSinceLastDash = 0f;
    private float currentDashingTime = 0f;
    public float maxDashingTime = 1f;
    private bool isDashing;
    
    [Header("Knockback")]
    public float knockbackForceReductionMultiplier = 0f;

    private Vector2 velocity;
    private Vector3 worldPosition;

    private float maxRotationSpeed = 360;
    private float rotationOffset = 270;

    private WormAnimation wormAnimation;
    private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (Instance != null &&  Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        headTransform = GameObject.FindGameObjectWithTag("Head").transform;
        wormAnimation = gameObject.GetComponent<WormAnimation>();
        cam = Camera.main;
    }

    void Start()
    {
        normalMoveSpeed = moveSpeed;
        originalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Position
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        worldPosition = cam.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;

        if (Keyboard.current.spaceKey.isPressed && timeSinceLastDash >= dashCooldown)
        {
            Dash();
        }

        if ((Keyboard.current.spaceKey.wasReleasedThisFrame || currentDashingTime >= maxDashingTime) && isDashing)
        {
            EndDash();
        }
        
        timeSinceLastDash += Time.deltaTime;
        
        Move();
        Rotate();
        //wormAnimation.AnimateBody();
    }

    void Move()
    {
        Vector2 toTarget = worldPosition - headTransform.position;
        float distance = toTarget.magnitude;

        currentSpeed = moveSpeed;
        if (distance < slowRadius)
        {
            currentSpeed = moveSpeed * (distance / slowRadius);
        }
        Vector2 desiredVelocity = toTarget.normalized * currentSpeed;
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

    private void Dash()
    {
        isDashing = true;
        moveSpeed = normalMoveSpeed * dashMoveSpeedIncreaseMultiplier;
        currentDashingTime += Time.deltaTime;
    }

    private void EndDash()
    {
        currentDashingTime = 0;
        timeSinceLastDash = 0;
        isDashing = false;
        moveSpeed = normalMoveSpeed;
    }

    public void GetKnockedBack(Vector2 sourcePosition, float force)
    {
        Vector2 direction = ((Vector2)headTransform.position - sourcePosition).normalized;
        
        velocity += direction * (force * (1 - knockbackForceReductionMultiplier));
    }
}
