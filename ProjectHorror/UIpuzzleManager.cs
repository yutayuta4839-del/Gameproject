using Cysharp.Threading.Tasks;
using UnityEngine;

public class UIpuzzleManager : MonoBehaviour
{
    public GameObject puzzleprefab;
    private RectTransform canvasrectTransform;

    void Start()
    {
        // 1. Canvasを探してRectTransformを取得しておく
        GameObject canvasObj = GameObject.FindWithTag("Canvas");
        if (canvasObj != null)
        {
            canvasrectTransform = canvasObj.GetComponent<RectTransform>();
        }

        ShowPuzzle();
    }

    public void ShowPuzzle()
    {
        // 2. まず普通にプレハブを生成する
        GameObject newobj = Instantiate(puzzleprefab);

        // 3. SetParentで親をCanvasに設定する
        // ※第2引数に `false` を渡すのが超重要です！
        // （falseにすることで、UIのサイズや位置がローカル基準できれいに収まります）
        newobj.transform.SetParent(canvasrectTransform, false);
    }
}