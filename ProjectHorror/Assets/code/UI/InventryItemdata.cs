using UnityEngine;

[CreateAssetMenu(fileName = "New Inventry Item", menuName = "DevByte/Inventry/Inventry Item")]
public class InventryItemdata : ScriptableObject
{
    [Header("Item")]
    [SerializeField] private string Item_Name;
    [SerializeField]
    private Sprite Item_Icon
;

    [Header("gridsize")]
    [SerializeField, Min(1)] private int item_width = 1;
    [SerializeField, Min(1)] private int item_height = 1;

    //読み取りプロパティ

    public Sprite ItemIcon => Item_Icon;



    public int Item_Width => item_width;
    public int Item_Height => item_height;
}
