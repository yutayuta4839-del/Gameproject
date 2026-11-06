using UnityEngine;
using UnityEngine.UI;

public class Sanityview : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Slider sanityslider;

    //UIに関する更新処理を記述していく。顔の変更、UIの数値の変更。

    public void ShowSanityUI(GameObject sanityUI,bool show)
    {
        sanityUI.SetActive(show);
    }

    public void Colorchange(GameObject faceUI,float colorchangeamount) //引数には色を変える値を
    {
        //色を変える。
    }

    
}
