using MarcoHelpers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NeedMeter : MonoBehaviour
{
    [Header("Type")]
    public Needs need;

    [Header("Decay")]
    public float decayRateInSeconds;
    public float timerInterval;
    public bool isDecayActive = true;

    private Slider slider;

    private void Awake()
    {
        slider= GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        EventSystem.Subscribe(EventName.NEEDMANAGER_UPDATE, OnNeedValueChanged);
        EventSystem.Subscribe(EventName.SET_DECAY_ACTIVE, SetDecayActive);
        SetValue(NeedManager.Instance.GetValue(need));
        SetDecayManual(need, NeedManager.Instance.GetDecayValue(need));
    }

    private void OnDisable()
    {
        EventSystem.Unsubscribe(EventName.NEEDMANAGER_UPDATE, OnNeedValueChanged);
        EventSystem.Unsubscribe(EventName.SET_DECAY_ACTIVE, SetDecayActive);
    }

    public void Start()
    {
        SetValue(NeedManager.Instance.GetValue(need));
        SetDecayManual(need, NeedManager.Instance.GetDecayValue(need));
        InvokeRepeating(nameof(Decay), 0.0f, timerInterval);
    }

    public void Decay()
    {
        if (!isDecayActive || !gameObject.activeInHierarchy) { return; }

        float newValue = Mathf.Clamp01(slider.value - decayRateInSeconds * timerInterval);
        slider.value = newValue;
        NeedManager.Instance.SetValue(need, newValue);
    }

/*    public void Retrieve(float percent)
    {
        slider.value = Mathf.Clamp01(slider.value + percent);
    }*/

    public void OnNeedValueChanged(Needs changedNeed, float value)
    {
        if (this.need == changedNeed)
            SetValue(value);
    }

    public void SetValue(float value)
    {
        value = Mathf.Clamp01(value);
        slider.value = value;
    }

    public void SetDecayActive(Needs changedNeed, float isActiveValue)
    {
        if (this.need == changedNeed)
        {
            bool isActive = isActiveValue > 0.5f;
            isDecayActive = isActive;
        }

    }

    private void SetDecayManual(Needs need, bool isActive)
    {
        isDecayActive= isActive;
    }
}
