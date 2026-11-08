using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventryItem : MonoBehaviour , IDragHandler , IBeginDragHandler, IEndDragHandler
{

    [Header("itemdata")]
    [SerializeField] private InventryItemdata item_Data;

    [Header("References")]
    [SerializeField] private Image item_image;

    [Header("Inventory")]
    [SerializeField] private Inventorygrid inventory_grid;

    private RectTransform item_Rect;
    private RectTransform item_Layer;

    private int previous_grid_X;
    private int previous_grid_Y;

    private int hovered_grid_X = -1;
    private int hovered_grid_Y = -1;

    private int grid_X;
    private int grid_Y;

    private Vector2 drag_Pointer_Offset;

    private int grabbed_Cell_X;
    private int grabbed_Cell_Y;

    private int Grid_X => grid_X;
    private int Grid_Y => grid_Y;


    public InventryItemdata ItemData => item_Data;

    public int GridWidth => item_Data != null ? item_Data.Item_Width : 1; //もしデータがセットされていたら、itemdataの横幅を返し、もしなかったら、1を返す。

    public int GridHeight => item_Data != null ? item_Data.Item_Height : 1;

    private void Awake()
    {
        item_Rect = GetComponent<RectTransform>();

        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (item_Data == null || item_image == null) return;

        item_image.sprite = item_Data.ItemIcon;
        item_image.preserveAspect = true; //UIのImageコンポーネントは引き伸ばされて枠に表示されるが、
                                          //Preserve Aspectをオンにすると画像の縦横比を保ったまま枠の中に収まるように自動調整

    }

    public void SetGridPosition(int x, int y)
    {
        grid_X = x;
        grid_Y = y;
    }

    public void SetInventryGrid(Inventorygrid grid)
    {
        inventory_grid = grid;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventory_grid == null || item_Rect == null) return;

        RectTransform item_Parent = item_Rect.parent as RectTransform;

        if (item_Parent == null) return;

        bool Point_Covered = RectTransformUtility.ScreenPointToLocalPointInRectangle(item_Parent, eventData.position, 
            eventData.pressEventCamera, out Vector2 pointer_local_Position);

        if (Point_Covered)
        {
            item_Rect.anchoredPosition = pointer_local_Position - drag_Pointer_Offset;
        }

        bool over_Grid = inventory_grid.GetGridPosition(eventData.position, eventData.pressEventCamera, out int pointer_Cell_X, out int pointer_Cell_Y);

        if (over_Grid)
        {
            hovered_grid_X = pointer_Cell_X - grabbed_Cell_X;
            hovered_grid_Y = pointer_Cell_Y - grabbed_Cell_Y;

            inventory_grid.ShowPlacementPreview(this, hovered_grid_X, hovered_grid_Y);
        }
        else
        {
            hovered_grid_X = -1;
            hovered_grid_Y = -1;

            inventory_grid.HidePlacementPreview();
        }
    }

    public void OnBeginDrag(PointerEventData event_data)
    {
        if (inventory_grid == null || item_Rect == null) return;

        previous_grid_X = grid_X;
        previous_grid_Y = grid_Y;

        RectTransform item_Parent = item_Rect.parent as RectTransform; //???

        if (item_Parent != null)
        {
            bool point_covered = RectTransformUtility.ScreenPointToLocalPointInRectangle(item_Parent, event_data.position,
                                                                event_data.pressEventCamera, out Vector2 pointer_local_position);

            if (point_covered)
            {
                drag_Pointer_Offset = pointer_local_position - item_Rect.anchoredPosition;
            }

            bool over_Grid = inventory_grid.GetGridPosition(event_data.position, event_data.pressEventCamera, out int pointer_Cell_X, out int pointer_Cell_Y);

            if (over_Grid)
            {
                hovered_grid_X = pointer_Cell_X - grabbed_Cell_X;
                hovered_grid_Y = pointer_Cell_Y - grabbed_Cell_Y;

                inventory_grid.ShowPlacementPreview(this, hovered_grid_X, hovered_grid_Y);

            }
            else
            {
                hovered_grid_X = -1;
                hovered_grid_Y = -1;

                inventory_grid.HidePlacementPreview();
            }
        }

        bool item_Pointer_Covered = RectTransformUtility.ScreenPointToLocalPointInRectangle(item_Parent, event_data.position,
                                                                event_data.pressEventCamera, out Vector2 item_local_Point);

        if (item_Pointer_Covered)
        {
            float cell_Width = item_Rect.rect.width / GridWidth;
            float cell_Height = item_Rect.rect.height / GridHeight;

            grabbed_Cell_X = Mathf.FloorToInt((item_local_Point.x - item_Rect.rect.xMin) / cell_Width);
            grabbed_Cell_Y = Mathf.FloorToInt((item_Rect.rect.yMax - item_local_Point.y) / cell_Height);

            grabbed_Cell_X = Mathf.Clamp(grabbed_Cell_X, 0, GridWidth - 1);
            grabbed_Cell_Y = Mathf.Clamp(grabbed_Cell_Y, 0, GridHeight - 1);

            inventory_grid.RemoveItem(this);

            transform.SetAsLastSibling();
        }
    }

    public void OnEndDrag(PointerEventData event_data)
    {
        if (inventory_grid == null) return;

        bool placed = false;

        bool over_grid = inventory_grid.GetGridPosition(event_data.position, event_data.pressEventCamera, out int pointer_Cell_X, out int pointer_Cell_Y);

        if (over_grid)
        {
            int target_cell_X = pointer_Cell_X - grabbed_Cell_X;
            int target_cell_Y = pointer_Cell_Y - grabbed_Cell_Y;

            placed = inventory_grid.PlaceItem(this, target_cell_X, target_cell_Y);
        }

        if (!placed)
        {
            inventory_grid.PlaceItem(this, previous_grid_X, previous_grid_Y);
        }

        inventory_grid.HidePlacementPreview();

        hovered_grid_X = -1;
        hovered_grid_Y = -1;
    }
}
