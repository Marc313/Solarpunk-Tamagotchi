using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text humidityText;
    [SerializeField] private TMP_Text refreshWaterText;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject startScreen;

    private static bool isStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (!isStarted)
        {
            isStarted = true;
            ShowStartScreen();
        }
    }

    private void Update()
    {
        if (winScreen.activeSelf && Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0)
        {
            winScreen.SetActive(false);
        }
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

    public void ShowWinScreen()
    {
        if (winScreen == null) return;
        winScreen.SetActive(true);
    }

    public void ShowStartScreen()
    {
        if (startScreen == null) return;
        startScreen.SetActive(true);
    }
}
