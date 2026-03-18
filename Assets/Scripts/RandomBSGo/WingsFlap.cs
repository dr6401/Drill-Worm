using UnityEditor.Timeline;
using UnityEngine;

public class WingsFlap : MonoBehaviour
{
    public float flapSpeed = 100f;
    public float angle = 40f;

    public Transform leftWing;
    public Transform rightWing;
    
    private Quaternion leftStartWingRotation;
    private Quaternion rightStartWingRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftStartWingRotation = leftWing.localRotation;
        rightStartWingRotation = rightWing.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float rotAngle = Mathf.Sin(Time.time * flapSpeed) * angle;
        
        leftWing.localRotation = leftStartWingRotation * Quaternion.Euler(0f, 0f, rotAngle);
        rightWing.localRotation = rightStartWingRotation * Quaternion.Euler(0f, 0f, rotAngle);
    }
}
