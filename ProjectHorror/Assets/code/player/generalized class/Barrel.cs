using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Barrel : interactobject , Iwindowprovider  //Barrelobject‚ÍÀ•¨‚Ì“–‚½‚è”»’è‚¾‚¯‚ğ‚Â
{
    [SerializeField] GameObject ObjectSearchwindow;
    public GameObject Window => ObjectSearchwindow;

    //public Image Window  ˆÈã‚Ì•¨‚ÍˆÈ‰º‚ÌÈ—ª
    //{
    //    get { return ObjectSearchwindow; }
    //}

}
