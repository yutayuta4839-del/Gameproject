using UnityEngine;

public class eldrich : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    private float distanceplayer;
  


    float Eldrichdistance(Transform plyaerpos)
    {
        distanceplayer =  Vector2.Distance(transform.position, plyaerpos.position);

        //player‚Æeldrich‚Ì‹——£‚ğŒŸ’m‚·‚éB

        return distanceplayer;
    }
}
