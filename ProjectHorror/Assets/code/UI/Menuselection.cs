using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Menuselection : MonoBehaviour
{
    public GameObject menucanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menucanvas.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleMenu();
        }                 //ここにtabを押したかの入力を検知して、menuを表示と非表示にする
    }

    public void ToggleMenu()
    {
        menucanvas.SetActive(!menucanvas.activeSelf);
    }

}
