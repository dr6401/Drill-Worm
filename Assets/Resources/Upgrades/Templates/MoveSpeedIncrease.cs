using UnityEngine;

public class MoveSpeedIncrease : Augment
{
    [Header("Augment Specifics")]
    public float moveSpeedIncreaseMultiplier = 1f;
    public override void Apply(GameObject player)
    {
        Movement.Instance.normalMoveSpeed = Movement.Instance.normalMoveSpeed * moveSpeedIncreaseMultiplier;
        Movement.Instance.moveSpeed = Movement.Instance.normalMoveSpeed;
    }
}
