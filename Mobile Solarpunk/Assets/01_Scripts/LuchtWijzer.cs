using UnityEngine;

public class LuchtWijzer : MonoBehaviour
{
    [Header("Meter Movement")]
    [SerializeField] private float minRotateAngle;
    [SerializeField] private float maxRotateAngle;
    [SerializeField] private float speed;
    [Header("Success Criteria")]
    [SerializeField] private float minTargetAngle;
    [SerializeField] private float maxTargetAngle;
    [SerializeField] private float timeTillRetry = 2;

    private int direction = 1;
    private bool active = true;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Stop();
        }

        if (active)
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation += Vector3.forward * direction * speed * Time.deltaTime;
            float checkAngle = rotation.z > 180 ? rotation.z - 360 : rotation.z;

            // If statement is seperated to avoid constantly changing direction
            if (checkAngle < minRotateAngle)
            {
                direction = 1;
            }
            else if (checkAngle > maxRotateAngle)
            {
                direction = -1;
            }


            transform.rotation = Quaternion.Euler(rotation);
        }
    }

    public void Stop()
    {
        active = false;

        Vector3 rotation = transform.rotation.eulerAngles;
        float checkAngle = rotation.z > 180 ? rotation.z - 360 : rotation.z;

        if (checkAngle > minTargetAngle && checkAngle < maxTargetAngle)
        {
            // Change Text
            UIManager.Instance.SetHumidityText("The humidity is just right!");
            NeedManager.Instance.SetDecayActive(Needs.Air, false);
            NeedManager.Instance.UpdateValue(Needs.Air, 1f);
            // Happy egg
        }
        else
        {
            UIManager.Instance.SetHumidityText("That is not quite right. Give it another try!");
            Invoke(nameof(Activate), timeTillRetry);
        }
    }

    public void Activate()
    {
        active = true;
    }
}
