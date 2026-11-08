using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventorygrid : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler
{                                                //マウスの位置をけんちするevent 

    [Header("gridsetting")]
    [SerializeField] private int grid_Width = 10;
    [SerializeField] private int grid_Height = 6;

    [Header("References")]
    [SerializeField] private RectTransform grid_visual;
    [SerializeField] private RectTransform hovere_highlight;
    [SerializeField] private RectTransform Item_Layer;

    [SerializeField] private GameObject grid_prefab;


    private RectTransform grid_Rect;

    private int hovered_cell_x = -1;
    private int hovered_cell_y = -1;   //無効なグリッドであるという意味

    private InventryItem[,] inventry_Items;

    [Header("Test")]
    [SerializeField] private InventryItem test_item_prefab;

    [Header("Place Review")]
    [SerializeField] private Color valid_Placement_Color = new Color(0f, 1f, 0f, 0.35f);

    [SerializeField] private Color invalid_Placement_Color = new Color(1f, 0f, 0f, 0.35f);

    private Image hovere_Highlight_Image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inventry_Items = new InventryItem[grid_Width, grid_Height];

        grid_Rect = GetComponent<RectTransform>();

        GenerateGrid();

        if (hovere_highlight != null)
        {
            hovere_Highlight_Image = hovere_highlight.GetComponent<Image>();
            hovere_highlight.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Start() //test
    {
        if (test_item_prefab == null) return;

        InventryItem testItem = Instantiate(test_item_prefab, Item_Layer);

        PlaceItem(testItem, 2, 2);
    }

    void GenerateGrid()
    {
        if (grid_visual == null && grid_prefab == null) return;

        int totalcell = grid_Width * grid_Height;

        for (int i = 0; totalcell > i; i++)
        {
            Instantiate(grid_prefab, grid_visual, false);
        }
    }

    void Updatehoverhighlight(float cell_width, float cell_height)　　//視覚的なハイライトロジック
    {
        if (hovere_highlight == null) return;

        if (test_item_prefab == null)
        {
            hovere_highlight.gameObject.SetActive(false);
            return;
        }

        int hovered_item_width = 1;
        int hovered_item_height = 1;

        bool can_place = CanplaceItem(test_item_prefab, hovered_cell_x, hovered_cell_y);

        hovere_highlight.gameObject.SetActive(true);

        hovere_highlight.sizeDelta = new Vector2(cell_width * hovered_item_width, cell_height * hovered_item_height);

        hovere_highlight.anchoredPosition = new Vector2(hovered_cell_x * cell_width, -hovered_cell_y * cell_height); //ホバーの固定位置を計算

        if (hovere_Highlight_Image != null)
        {
            hovere_Highlight_Image.color = can_place ? valid_Placement_Color : invalid_Placement_Color;
        }
    }

    void Updatehoveredcell(PointerEventData eventData) //playerのmouseをホバーさせて、枠内をハイライトする。
    {
        if (grid_Rect == null) return;

        bool pointercovered = RectTransformUtility.ScreenPointToLocalPointInRectangle(grid_Rect, eventData.position,
                                                                                             eventData.enterEventCamera, out Vector2 local_point);
        if (!pointercovered) return;

        if (!grid_Rect.rect.Contains(local_point))
        {
            if (hovere_highlight != null)
            {
                hovere_highlight.gameObject.SetActive(false);

            }

            return;
        }

        float cell_Width = grid_Rect.rect.width / grid_Width;
        float cell_Height = grid_Rect.rect.height / grid_Height;

        hovered_cell_x = Mathf.FloorToInt((local_point.x - grid_Rect.rect.xMin) / cell_Width);
        hovered_cell_y = Mathf.FloorToInt((grid_Rect.rect.yMax - local_point.y) / cell_Height);

        hovered_cell_x = Mathf.Clamp(hovered_cell_x, 0, grid_Width - 1);
        hovered_cell_y = Mathf.Clamp(hovered_cell_y, 0, grid_Height - 1);

        Updatehoverhighlight(cell_Width, cell_Height);
    }

    private bool IsInsideGrid(int start_X, int start_Y, int item_Width, int item_Height)  //アイテムが枠内にあるかの確認
    {
        if (start_X < 0 || start_Y < 0) return false;

        if (start_X + item_Width > grid_Width) return false;

        if (start_Y + item_Height > grid_Height) return false;

        return true;
    }

    private bool CanplaceItem(InventryItem item, int start_X, int start_Y)
    {
        if (item == null) return false;

        int item_width = item.GridWidth;
        int item_height = item.GridHeight;

        if (!IsInsideGrid(start_X, start_Y, item_width, item_height)) return false;

        for (int x = 0; x < item_width; x++)
        {
            for (int y = 0; y < item_height; y++)
            {

                InventryItem occuping_item = inventry_Items[start_X + x, start_Y + y];

                if (occuping_item != null && occuping_item != item)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool PlaceItem(InventryItem item, int start_X, int start_Y)
    {
        if (!CanplaceItem(item, start_X, start_Y)) return false;

        for (int x = 0; x < item.GridWidth; x++)
        {
            for (int y = 0; y < item.GridHeight; y++)
            {
                inventry_Items[start_X + x, start_Y + y] = item;
            }
        }

        item.SetGridPosition(start_X, start_Y);
        item.SetInventryGrid(this);

        PositionItem(item, start_X, start_Y);

        return true;
    }

    private void PositionItem(InventryItem item, int start_X, int start_Y)
    {
        RectTransform item_rect = item.GetComponent<RectTransform>();

        if (item_rect == null) return;

        float cell_Width = grid_Rect.rect.width / grid_Width;
        float cell_Height = grid_Rect.rect.height / grid_Height;

        item_rect.SetParent(Item_Layer, false);

        item_rect.anchorMin = new Vector2(0f, 1f);
        item_rect.anchorMax = new Vector2(0f, 1f);

        item_rect.pivot = new Vector2(0f, 1f);

        item_rect.sizeDelta = new Vector2(item.GridWidth * cell_Width, item.GridHeight * cell_Height);
        item_rect.anchoredPosition = new Vector2(start_X * cell_Width, -start_Y * cell_Height);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Updatehoveredcell(eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        hovered_cell_x = -1;
        hovered_cell_y = -1;

        if (hovere_highlight != null)
        {
            hovere_highlight.gameObject.SetActive(false);

        }
    }

    public void RemoveItem(InventryItem item)
    {
        if (item == null) return;

        for (int x = 0; x < grid_Width; x++)
        {
            for (int y = 0; y < grid_Height; y++)
            {
                if (inventry_Items[x, y] == item)
                {
                    inventry_Items[x, y] = null;
                }
            }
        }
    }

    public bool GetGridPosition(Vector2 screenposition, Camera event_camera, out int cell_X, out int cell_Y)
    {
        cell_X = -1;
        cell_Y = -1;

        if (grid_Rect == null) return false;

        bool point_covered = RectTransformUtility.ScreenPointToLocalPointInRectangle(grid_Rect, screenposition, event_camera, out Vector2 localPoint);

        if (!point_covered) return false;

        float cell_Width = grid_Rect.rect.width / grid_Width;
        float cell_Height = grid_Rect.rect.height / grid_Height;

        cell_X = Mathf.FloorToInt((localPoint.x - grid_Rect.rect.xMin) / cell_Width);
        cell_Y = Mathf.FloorToInt((grid_Rect.rect.yMax - localPoint.y) / cell_Height);

        return true;


    }

    public void ShowPlacementPreview(InventryItem item, int cell_X, int cell_Y)
    {
        if (item == null || hovere_highlight == null) return;

        float cell_Width = grid_Rect.rect.width / grid_Width;
        float cell_Height = grid_Rect.rect.height / grid_Height;

        bool can_place = CanplaceItem(item, cell_X, cell_Y);

        hovere_highlight.gameObject.SetActive(true);

        hovere_highlight.sizeDelta = new Vector2(item.GridWidth * cell_Width, item.GridHeight * cell_Height);
        hovere_highlight.anchoredPosition = new Vector2(cell_X * cell_Width, -cell_Y * cell_Height);

        if (hovere_Highlight_Image != null)
        {
            hovere_Highlight_Image.color = can_place ? valid_Placement_Color : invalid_Placement_Color;
        }

    }

    public void HidePlacementPreview()
    {
        if (hovere_highlight != null) hovere_highlight.gameObject.SetActive(false);
    }
}
