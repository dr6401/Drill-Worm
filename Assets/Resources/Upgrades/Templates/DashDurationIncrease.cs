using UnityEngine;

[CreateAssetMenu(fileName = "Augment", menuName = "Augments/Speed/DashDurationIncrease")]
public class DashDurationIncrease : Augment
{
    [Header("Augment Specifics")]
    public float durationIncrease;
    public override void Apply(GameObject player)
    {
        Movement movement = Movement.Instance.gameObject.GetComponent<Movement>();
        movement.maxDashingTime += durationIncrease;
    }
}