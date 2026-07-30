using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public BoxCollider2D Dialoguebox;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Transform actionmark = collision.transform.Find("actionmark");
            actionmark.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform actionmark = collision.transform.Find("actionmark");
        actionmark.gameObject.SetActive(false);
    }
}
