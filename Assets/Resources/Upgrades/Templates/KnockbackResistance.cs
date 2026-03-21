using UnityEngine;

[CreateAssetMenu(fileName = "Augment", menuName = "Augments/Resistance/KnockbackResistance")]
public class KnockbackResistance : Augment
{
    [Header("Augment Specifics")]
    public float knockbackResistanceIncrease = 0.05f;
    public override void Apply(GameObject player)
    {
        Movement.Instance.knockbackForceReductionMultiplier += knockbackResistanceIncrease;
        Movement.Instance.knockbackForceReductionMultiplier = Mathf.Clamp(Movement.Instance.knockbackForceReductionMultiplier, 0, 1);
        if (Movement.Instance.knockbackForceReductionMultiplier >= 1)
        {
            removeFromPoolAfterPicking = true;
        }
    }
}