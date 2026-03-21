using UnityEngine;

[CreateAssetMenu(fileName = "Augment", menuName = "Augments/Size/GrowUp")]
public class GrowUp : Augment
{
    [Header("Augment Specifics")]
    public int numberOfBodySegments;
    public override void Apply(GameObject player)
    {
        WormAnimation wormAnimation = PlayerStats.Instance.gameObject.GetComponent<WormAnimation>();
        if (wormAnimation != null) wormAnimation.ExtendBody(numberOfBodySegments);
        PlayerStats.Instance.IncreaseMaxHealth(numberOfBodySegments);
    }
}
