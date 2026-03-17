using System.Collections.Generic;
using UnityEngine;

public class WormAnimation : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] Transform headTransform;
    [SerializeField] private Transform bodyPartsFolder;
    [SerializeField] Transform tailTransform;
    private List<Transform> segments = new List<Transform>();
    //[SerializeField] Transform tailBodyPartTransform;

    [Header("Parameters")] public float segmentDistance = 0.5f;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        segments.Add(headTransform);
        segments.AddRange(bodyPartsFolder.GetComponentsInChildren<Transform>());
        segments.Add(tailTransform);
        segments.RemoveAll(seg => seg.CompareTag("SegmentsFolder"));
        foreach (Transform transform in segments)
        {
            Debug.Log($"Hey my name is {transform.name} and I'm position {segments.IndexOf(transform)} in the chain");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AnimateBody()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            // Position
            Transform prev = segments[i - 1];
            Transform current = segments[i];

            float segmentDistanceMultiplier = 1f;
            if (i == segments.Count - 1)
            {
                segmentDistanceMultiplier = 0.25f;
            }
            
            Vector2 direction = (current.position - prev.position).normalized;
            current.position = prev.position + (Vector3)(direction * (segmentDistance * segmentDistanceMultiplier));
            
            // Rotation
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            current.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
