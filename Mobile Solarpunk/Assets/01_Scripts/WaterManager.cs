using UnityEngine;
using UnityEngine.Events;

public class WaterManager : MonoBehaviour
{
    public UnityEvent OnRefreshSuccess;
    private bool isEmptied;
    private bool isAdded;

    public void Empty()
    {
        isEmptied = true;
        isAdded = false;

        UIManager.Instance.SetWaterText("The dirty water is given to the plants! Quickly add clean water!");
    }

    public void Add()
    {
        isAdded = true;

        if (isEmptied) OnSuccess();
        else
        {
            UIManager.Instance.SetWaterText("The water is still dirty, make sure to empty the water first!");
        }
    }

    public void OnSuccess()
    {
        UIManager.Instance.SetWaterText("The water is fresh now!");
        NeedManager.Instance.AddValue(Needs.Hygiene, 1f);
        OnRefreshSuccess?.Invoke();
    }
}
