using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionController : SerializedMonoBehaviour
{
    [OnValueChanged("OnHeaderChanged")]
    public string heading;
    private string baseString = "Container_";

    public bool showComponents = false;

    [ShowIf("showComponents")]
    public TextMeshProUGUI header;
    [ShowIf("showComponents")]
    public Slider slider;
    [ShowIf("showComponents")]
    public TextMeshProUGUI displayText;

    [OnValueChanged("OnUseEnumChange")]
    public bool useEnum = false;

    [HideInInspector]
    public string enumString = "";
    [ShowIf("useEnum")]
    public List<string> customValues = new List<string>();

    [HideIf("useEnum")]
    [PropertyRange("sliderMin", "sliderMax")]
    [OnValueChanged("OnValueChange")]
    public float fValue;
    [HideIf("useEnum")]
    [OnValueChanged("OnMinValueChange")]
    public float minValue;
    [HideIf("useEnum")]
    [OnValueChanged("OnMaxValueChange")]
    public float maxValue;
    [ShowIf("useEnum")]
    [PropertyRange("minRange_int", "maxRange_int")]
    [OnValueChanged("OnValueChange")]
    public int iValue;

    [HideInInspector]
    public UnityAction<float> onSlideFloatChanged;
    [HideInInspector]
    public UnityAction<int> onSlideIntChanged;

    public AudioClip onChangeSFX;

    [HideInInspector]
    public int hideIndex
    {
        get { return m_hideIndex; }
        set
        {
            m_hideIndex = value;
            if (m_hideIndex != -1)
                sliderMax = m_hideIndex;
        }
    }
    private int m_hideIndex = -1;

    private int maxRange_int { get { return (int)slider.maxValue; } }
    private int minRange_int { get { return (int)slider.minValue; } }
    public float sliderMax {
        get { return slider.maxValue; }
        set { slider.maxValue = value; } }
    public float sliderMin {
        get { return slider.minValue; }
        set { slider.minValue = value; } }

    public void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        OnSliderValueChanged(fValue);
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float _value)
    {
        if (useEnum)
        {
            int val = Mathf.FloorToInt(_value);
            if (val != iValue)
            {
                iValue = Mathf.FloorToInt(_value);
                onSlideIntChanged?.Invoke(iValue);
                SoundManager.Inst.PlaySoundEffect(onChangeSFX);
                displayText.text = customValues[iValue].ToString().Replace("_", " ");
            }
        }
        else
        {
            if (_value != fValue)
            {
                fValue = _value;
                onSlideFloatChanged?.Invoke(fValue);
                SoundManager.Inst.PlaySoundEffect(onChangeSFX);
                displayText.text = fValue.ToString("F2");
            }
        }
    }

    #region OnValuesChanged
    private void OnMaxValueChange()
    {
        maxValue = Mathf.Clamp(maxValue, minValue + 0.01f, maxValue);
        sliderMax = maxValue;
        fValue = Mathf.Clamp(fValue, minValue, maxValue);
    }

    private void OnMinValueChange()
    {
        minValue = Mathf.Clamp(minValue, minValue, maxValue - 0.01f);
        sliderMin = minValue;
        fValue = Mathf.Clamp(fValue, minValue, maxValue);
    }

    private void OnHeaderChanged()
    {
        gameObject.name = baseString + heading;
        header.text = heading + ":";
    }

    private void OnValueChange()
    {
        slider.value = (useEnum) ? iValue : fValue;
        if (!useEnum)
            iValue = Mathf.FloorToInt(fValue);
        else
            fValue = iValue;

        OnSliderValueChanged(fValue);
    }

    private void OnUseEnumChange()
    {
        slider.wholeNumbers = useEnum;
        OnValueChange();
    }
    #endregion

    public void SetValue(int _value)
    {
        SetValue((float)_value);
    }

    public void SetValue(float _value)
    {
        iValue = Mathf.FloorToInt(_value);
        fValue = _value;
        if (useEnum)
        {
            displayText.text = customValues[iValue].ToString().Replace("_", " ");
        }
        else
        {
            displayText.text = fValue.ToString("F2");
        }
        slider.SetValueWithoutNotify(_value);
    }

    private void OnValidate()
    {
        header = transform.FindChildWithNameRecusively<TextMeshProUGUI>("Heading");
        slider = transform.FindChildWithNameRecusively<Slider>("Slider");
        displayText = transform.FindChildWithNameRecusively<TextMeshProUGUI>("DisplayText");
    }
}
