using UnityEngine;

public class eldrich : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private float distanceplayer;
    [SerializeField] float sanityvaluedecreaseamount;
    public GameObject player;
    public Transform playerpos;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            playerpos = player.GetComponent<Transform>();

            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player == null) return;

        //危ないところに入ったというような演出をplayerに

        Debug.Log(CalculateEldrichdistance(playerpos));



        if (CalculateEldrichdistance(playerpos) <= 5f)
        {
            //サン値へる処理。presenterから　presenter(sanityvalurdecereaseamount);となる。
            Debug.Log("近い");
        }
    }
    
    float CalculateEldrichdistance(Transform plyaertrans)
    {
        distanceplayer = Vector2.Distance(transform.position, plyaertrans.position);

        //playerとeldrichの距離を検知する。

        return distanceplayer;
    }
}
