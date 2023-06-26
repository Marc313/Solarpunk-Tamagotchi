using MarcoHelpers;
using System.Collections.Generic;
using UnityEngine;

public enum Needs
{
    Fun,
    Food,
    Hygiene,
    Air
}

public class NeedManager : Singleton<NeedManager>
{
    [SerializeField] private float startValue = 0.5f;
    private static Dictionary<Needs, float> needValues = new Dictionary<Needs, float>();
    private static Dictionary<Needs, bool> needActivation = new Dictionary<Needs, bool>();
    private static bool isStarted = false;

    private void Awake()
    {
        if (isStarted) return;

        Instance = this;

        needValues.Add(Needs.Fun, startValue);
        needValues.Add(Needs.Food, startValue);
        needValues.Add(Needs.Hygiene, startValue);
        needValues.Add(Needs.Air, startValue);

        needActivation.Add(Needs.Fun, true);
        needActivation.Add(Needs.Food, true);
        needActivation.Add(Needs.Hygiene, true);
        needActivation.Add(Needs.Air, true);

        EventSystem.RaiseEvent(EventName.NEEDMANAGER_UPDATE, Needs.Fun, startValue);
        EventSystem.RaiseEvent(EventName.NEEDMANAGER_UPDATE, Needs.Air, startValue);
        EventSystem.RaiseEvent(EventName.NEEDMANAGER_UPDATE, Needs.Hygiene, startValue);
        EventSystem.RaiseEvent(EventName.NEEDMANAGER_UPDATE, Needs.Food, startValue);

        isStarted = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            UpdateValue(Needs.Fun, 1.0f);
        }
    }

    public float GetValue(Needs need)
    {
        if (needValues.ContainsKey(need))
        {
            return needValues[need];
        }

        return 1;
    }

    public bool GetDecayValue(Needs need)
    {
        return needActivation[need];
    }

    public void SetValue(Needs need, float value)
    {
        if (needValues.ContainsKey(need))
        {
            needValues[need] = value;
        }

        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        int goodNeeds = 0;
        foreach (Needs need in needValues.Keys)
        {
            if (needValues[need] > 0.85f)
            {
                goodNeeds++;
            }
        }

        if (goodNeeds == 4)
        {
            UIManager.Instance.ShowWinScreen();
        }
    }

    /// <summary>
    /// Sets the new value for the corresponding need, and also broadcasts this new value to meters.
    /// </summary>
    /// <param name="need"></param>
    /// <param name="value"></param>
    public void UpdateValue(Needs need, float value)
    {
        SetValue(need, value);

        EventSystem.RaiseEvent(EventName.NEEDMANAGER_UPDATE, need, value);
    }

    public void SetDecayActive(Needs need, bool isActive)
    {
        if (needActivation.ContainsKey(need))
        {
            needActivation[need] = isActive;
        }

        EventSystem.RaiseEvent(EventName.SET_DECAY_ACTIVE, need, isActive ? 1f : 0f);
    }

    public void AddValue(Needs need, float addition)
    {
        if (needValues.ContainsKey(need))
        {
            float newValue = Mathf.Clamp01(needValues[need] + addition);
            UpdateValue(need, newValue);
        }
    }
}
