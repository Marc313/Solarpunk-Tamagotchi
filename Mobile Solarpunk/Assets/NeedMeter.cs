using UnityEngine;
using UnityEngine.UI;

public class NeedMeter : MonoBehaviour
{
    [Header("Decay")]
    public float decayRateInSeconds;
    public float timerInterval;

    private Slider slider;

    private void Awake()
    {
        slider= GetComponentInChildren<Slider>();
    }

    public void Start()
    {
        InvokeRepeating(nameof(Decay), 0.0f, timerInterval);
    }

    public void Decay(/*object sender, ElapsedEventArgs e*/)
    {
        float newValue = Mathf.Clamp01(slider.value - decayRateInSeconds * timerInterval);
        Debug.Log(newValue);
        slider.value = newValue;
    }

    public void Retrieve(float percent)
    {
        slider.value = Mathf.Clamp01(slider.value + percent);
    }
}
