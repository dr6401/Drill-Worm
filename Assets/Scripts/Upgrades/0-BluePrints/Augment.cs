using UnityEngine;

public enum AugmentTier
{
    Silver,
    Gold,
    Prismatic
};
public enum AugmentCategory
{
    Drill,
    Speed,
    Hp,
    Resistance
};

public abstract class Augment : ScriptableObject
{
    public string augmentName;
    [TextArea] public string description;
    public Sprite icon;
    public Color color;
    public AugmentTier tier;
    public AugmentCategory category;
    
    public abstract void Apply(GameObject player);
}
