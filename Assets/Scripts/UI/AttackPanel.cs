using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackPanel : MonoBehaviour
{
    public InputManager inputManager;
    public FieldManager fieldManager;
    public TextMeshProUGUI strengthText;
    public Slider strengthSlider;

    int strength;

    public void Open()
    {
        strength = 1;

        strengthText.text = strength.ToString();
        strengthSlider.value = strength;
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
        strength = (int) strengthSlider.value;
        strengthText.text = strength.ToString();
    }

    public void OnAttack()
    {
        fieldManager.selectedField.Attack(strength);
        Close();
    }
}
