using UnityEngine;

public enum BodyPart
{
    None,
    Head,
    Top,
    Bottom,
    Feet,
    RightHand,
    LeftHand,
    TwoHand,
}


[CreateAssetMenu(fileName = "new EquipmentItemData", menuName = "RPG3D/ItemData/Equipment")]
public class EquipmentItemData : UsableItemData
{
    public BodyPart bodyPart;
    public int levelRequired;

    public override void Use()
    {
    }

    public override void Use(int slotIndex)
    {
        // todo -> equip this item
    }
}