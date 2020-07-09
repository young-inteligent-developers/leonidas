using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackPanel : MonoBehaviour
{
    public InputManager inputManager;
    public FieldManager fieldManager;
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
        inputManager.ResetInput();
        fieldManager.UnhighlightConnectedFields();
        gameObject.SetActive(false);
    }

    public void OnStrengthSliderChange()
    {
        strengthText.text = strengthSlider.value.ToString();
    }
}
