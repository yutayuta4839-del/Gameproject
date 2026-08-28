using UnityEngine;
using UnityEngine.EventSystems;

public class clickdestroy : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("クリックされた: " + gameObject.name);
        // 左クリックだけを対象にする
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            gameObject.SetActive(false); // まずは非表示にする（後述）
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition(eventData);
        offset = transform.position - mouseWorldPos;
    }

    // ドラッグ中、毎フレーム呼ばれる
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition(eventData);
        transform.position = mouseWorldPos + offset;
    }

    // ドラッグ終了時
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("ドラッグ終了: " + gameObject.name);
    }

    private Vector3 GetMouseWorldPosition(PointerEventData eventData)
    {
        Vector3 screenPos = new Vector3(eventData.position.x, eventData.position.y, mainCamera.WorldToScreenPoint(transform.position).z);
        return mainCamera.ScreenToWorldPoint(screenPos);
    }
}
