using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionPanel : MonoBehaviour
{
    public InputManager inputManager;
    public FieldManager fieldManager;
    public TextMeshProUGUI strengthText;
    public Slider strengthSlider;

    int strength;

    public void Open()
    {
        inputManager.enabled = false;

        strength = 1;
        strengthText.text = strength.ToString();
        strengthSlider.value = strength;
        gameObject.SetActive(true);
    }

    public void Close(bool cancelSelection)
    {
        inputManager.enabled = true;
        if (cancelSelection) inputManager.CancelSelection();
        gameObject.SetActive(false);
    }

    public void Set(int strength)
    {
        strengthSlider.maxValue = strength;
    }

    public void ChangeStrengthSlider()
    {
        strength = (int) strengthSlider.value;
        strengthText.text = strength.ToString();
    }

    public void Attack()
    {
        fieldManager.selectedField.Attack(strength);
        Close(true);
    }

    public void Regroup()
    {
        fieldManager.selectedField.Regroup(strength);
        Close(true);
    }
}
