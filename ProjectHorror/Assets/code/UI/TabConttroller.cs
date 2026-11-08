using UnityEngine;
using UnityEngine.UI;

public class TabConttroller : MonoBehaviour
{
    public Image[] tabimage;
    public GameObject[] pages;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateTab(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTab(int tabNo)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabimage[i].color = Color.grey;
        }
        pages[tabNo].SetActive(true);
        tabimage[tabNo].color = Color.white;
    }
}
