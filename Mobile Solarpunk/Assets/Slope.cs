using UnityEngine;

public class Slope : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Hoi");
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.EnableGravity();
        }
    }
}
