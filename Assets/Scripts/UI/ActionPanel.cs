using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActionPanel : MonoBehaviour
{
    [Header("Objects")]
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

    public void Close()
    {
        fieldManager.actionField = null;
        inputManager.swipedField = null;
        inputManager.enabled = true;
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
        inputManager.swipedField.Attack(strength);
        Close();
    }

    public void Regroup()
    {
        inputManager.swipedField.Regroup(strength);
        Close();
    }
}
