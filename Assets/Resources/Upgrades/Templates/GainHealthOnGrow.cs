using UnityEngine;

[CreateAssetMenu(fileName = "Augment", menuName = "Augments/Hp/GainHealthOnGrow")]

public class GainHealthOnGrow : Augment
{
    [Header("Augment Specifics")]
    public int healthPerBodyPartIncrease = 1;
    public override void Apply(GameObject player)
    {
        PlayerStats.Instance.healthOnGrowAmount = healthPerBodyPartIncrease;
        WormAnimation wormAnimation = PlayerStats.Instance.gameObject.GetComponent<WormAnimation>();
        int currentNumberOfBodyParts = wormAnimation.segments.Count - 2; // -2 for head and tail
        PlayerStats.Instance.IncreaseMaxHealth(currentNumberOfBodyParts);
    }
}