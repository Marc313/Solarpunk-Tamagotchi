using UnityEngine;

public class NewLuchtWijzer : MonoBehaviour
{
    [Header("Meter Movement")]
    [SerializeField] private float minSliderValue;
    [SerializeField] private float maxSliderValue;
    [SerializeField] private float speed;
    [Header("Success Criteria")]
    [SerializeField] private float minTargetValue;
    [SerializeField] private float maxTargetValue;
    [SerializeField] private float timeTillRetry = 2;

    private float currentValue = 0;
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
            currentValue += direction * speed * Time.deltaTime;
            Debug.Log(currentValue);    

            // If statement is seperated to avoid constantly changing direction
            if (currentValue < minSliderValue)
            {
                direction = 1;
            }
            else if (currentValue > maxSliderValue)
            {
                direction = -1;
            }

            GetComponent<MeshRenderer>().material.SetFloat("_Slider", currentValue);
        }
    }

    public void Stop()
    {
        active = false;
        GetComponent<MeshRenderer>().material.SetFloat("_Slider", currentValue);

        if (currentValue > minTargetValue && currentValue < maxTargetValue)
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
