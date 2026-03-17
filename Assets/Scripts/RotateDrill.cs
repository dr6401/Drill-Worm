using UnityEngine;

public class RotateDrill : MonoBehaviour
{
    [SerializeField] private Transform drillMiddle;
    private float rotationSpeed = 1000f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        drillMiddle.Rotate(0f, rotationSpeed * Time.deltaTime, 0);
    }
}
