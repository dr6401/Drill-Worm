using UnityEngine;

[CreateAssetMenu(fileName = "Augment", menuName = "Augments/Drill/DrillDriller")]
public class DrillDriller : Augment
{
    [Header("Augment Specifics")]
    public int damageIncrease;
    public override void Apply(GameObject player)
    {
        PlayerStats.Instance.drillDamage += damageIncrease;
    }
}