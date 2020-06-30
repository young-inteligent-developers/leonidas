using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackPanel : MonoBehaviour
{
    public TextMeshProUGUI strengthText;
    public Slider strengthSlider;

    public void Open(int strength)
    {
        strengthText.text = "1";
        strengthSlider.value = 1;
        strengthSlider.maxValue = strength;

        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void OnStrengthSliderChange()
    {
        strengthText.text = strengthSlider.value.ToString();
    }
}
