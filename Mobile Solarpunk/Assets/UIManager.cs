using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text humidityText;
    [SerializeField] private TMP_Text refreshWaterText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetHumidityText(string text)
    {
        if (humidityText == null) return;

        humidityText.text = text;
    }

    public void SetWaterText(string text)
    {
        if (refreshWaterText == null) return;

        refreshWaterText.text = text;
    }
}
