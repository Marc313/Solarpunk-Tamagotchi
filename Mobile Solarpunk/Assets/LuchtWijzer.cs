using UnityEngine;

public class LuchtWijzer : MonoBehaviour
{
    [SerializeField] private float minRotateAngle;
    [SerializeField] private float maxRotateAngle;
    [SerializeField] private float speed;

    private int direction = 1;
    private bool active = true;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            active= false;
        }

        if (active)
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation += Vector3.forward * direction * speed * Time.deltaTime;
            if (rotation.z < minRotateAngle || rotation.z > maxRotateAngle)
            {
                Debug.Log("TRUE");
                direction = -direction;
            }
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
