using TMPro;

public class FoodManager : Singleton<FoodManager>
{
    public int foodStartAmount;
    public static int foodAmount = 0;
    private static bool isStarted = false;
    private TMP_Text feedText;

    private void Awake()
    {
        feedText= GetComponentInChildren<TMP_Text>();
        if (isStarted) return;

        Instance = this;
        foodAmount = foodStartAmount;
        isStarted= true;
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

    public void AddFood()
    {
        SetFoodAmount(foodAmount + 1);
    }
}
