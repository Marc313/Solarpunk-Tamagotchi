using TMPro;

public class FoodManager : Singleton<FoodManager>
{
    public static int foodAmount = 3;
    private TMP_Text feedText;

    private void Awake()
    {
        Instance = this;
        feedText= GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        SetFoodAmount(foodAmount);
    }

    public void SetFoodAmount(int newAmount)
    {
        foodAmount = newAmount;
        feedText.text = $"Feed ({newAmount})";
    }

    public void DecreaseFood()
    {
        SetFoodAmount(foodAmount - 1);
    }
}
