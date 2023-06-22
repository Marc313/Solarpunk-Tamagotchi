using TMPro;
using UnityEngine;

public class RunnerGameManager : Singleton<RunnerGameManager>
{
    [Header("Score")]
    public float scorePerSecond;
    public TMP_Text scoreText;

    [Header("Lose Screen")]
    public GameObject loseScreen;
    public TMP_Text loseText;
    public float timeTillLoseScreen;

    private float currentScore;
    private bool isPlaying;

    private void Awake()
    {
        Instance = this;
        isPlaying = true;
    }

    private void Update()
    {
        if (!isPlaying) return;

        currentScore += scorePerSecond * Time.deltaTime;
        scoreText.text = "" + (int) currentScore;
    }

    public void OnLose()
    {
        Invoke(nameof(ShowLoseScreen), timeTillLoseScreen);
        isPlaying = false;
    }

    private void ShowLoseScreen()
    {
        loseText.text = loseText.text.Replace("%SCORE%", "" + (int) currentScore);
        loseScreen.SetActive(true);
    }
}
