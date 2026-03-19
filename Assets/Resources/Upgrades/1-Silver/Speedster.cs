using UnityEngine;

[CreateAssetMenu(fileName = "Augment", menuName = "Augments/Silver/Movement/Speedster")]
public class Speedster : MoveSpeedIncrease
{
    public override void Apply(GameObject player)
    {
        Movement.Instance.normalMoveSpeed = Movement.Instance.normalMoveSpeed * moveSpeedIncreaseMultiplier;
        Movement.Instance.moveSpeed = Movement.Instance.normalMoveSpeed;
    }
}