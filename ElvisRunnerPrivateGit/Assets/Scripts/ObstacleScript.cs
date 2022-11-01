using UnityEngine;

public class ObstacleScript : TileScript
{
   
    protected override void Awake()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlatformKiller"))
        {
            Destroy(gameObject);
        }
    }

}
