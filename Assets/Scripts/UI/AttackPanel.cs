using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackPanel : MonoBehaviour
{
    public InputManager inputManager;
    public FieldManager fieldManager;
    public TextMeshProUGUI strengthText;
    public Slider strengthSlider;

    public void Open()
    {
        strengthText.text = "1";
        strengthSlider.value = 1;
        gameObject.SetActive(true);
    }

    public void Close()
    {
        inputManager.CancelSelection();
        gameObject.SetActive(false);
    }

    public void Set(int strength)
    {
        strengthSlider.maxValue = strength;
    }

    public void OnStrengthSliderChange()
    {
        int s = (int) strengthSlider.value;
        strengthText.text = s.ToString();
    }
}
