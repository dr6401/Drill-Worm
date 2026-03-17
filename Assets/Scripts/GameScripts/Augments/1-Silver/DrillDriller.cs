using UnityEngine;

[CreateAssetMenu(fileName = "Augment", menuName = "Augments/Silver/Drill/DrillDriller")]
public class DrillDriller : Augment
{
    public override void Apply(GameObject player)
    {
        PlayerStats.Instance.drillDamage += 1;
    }
}