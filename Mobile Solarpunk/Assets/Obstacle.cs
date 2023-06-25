using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.OnHit();
        }
    }
}
