using UnityEngine;

[CreateAssetMenu(fileName = "Augment", menuName = "Augments/Speed/DashSpeedIncrease")]
public class DashSpeedIncrease : Augment
{
    [Header("Augment Specifics")]
    public float speedIncrease;
    public override void Apply(GameObject player)
    {
        Movement movement = Movement.Instance.gameObject.GetComponent<Movement>();
        movement.dashMoveSpeedIncreaseMultiplier *= 1 + speedIncrease;
    }
}