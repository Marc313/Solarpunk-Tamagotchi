using UnityEngine;

public class AddNeedValue : MonoBehaviour
{
    public Needs need;

    public void AddNeed(float value)
    {
        if (FoodManager.foodAmount > 0)
        {
            NeedManager.Instance.AddValue(need, value);
            FoodManager.Instance.DecreaseFood();
        }
    }
}
