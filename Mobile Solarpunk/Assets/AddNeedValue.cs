using UnityEngine;

public class AddNeedValue : MonoBehaviour
{
    public Needs need;

    public void AddNeed(float value)
    {
        NeedManager.Instance.AddValue(need, value);
    }
}
