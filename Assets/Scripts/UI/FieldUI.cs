using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldUI : MonoBehaviour
{
    /*
    public const float D_S          = 0.75f;    // defense bonus container scale
    public const float D_S_DUR      = 0.175f;   // defense bonus container scale duration
    public const float D_B_DUR      = 0.1f;     // defense background duration
    public const float D_B_DEL      = 0.1f;     // defense background delay
    public const float D_T_DUR      = 0.1f;     // defense text duration
    public const float D_T_DEL      = 0.2f;     // defense text delay
    */

    [Header("Animation curves")]
    public AnimationCurve defenseInAC;
    public AnimationCurve defenseOutAC;

    [Header("Field UI properties")]
    public TextMeshProUGUI unitText;
    public TextMeshProUGUI defenseText;
    public GameObject defenseBonus;
    public Image defenseBackground;

    public void DefenseIn()
    {
        defenseBonus.transform.localScale = Vector3.one * 0.75f;

        LeanTween.scale(defenseBonus, Vector3.one, 0.3f)
            .setEase(defenseInAC);
        LeanTween.alpha(defenseBackground.GetComponent<RectTransform>(), 1, 0.15f)
            .setEase(LeanTweenType.easeInCubic);
        LeanTween.value(defenseText.gameObject, (float v, float r) => {
            Color c = defenseText.color; c.a = v;
            defenseText.color = c;
        }, defenseText.color.a, 1, 0.15f)
            .setDelay(0.25f)
            .setEase(LeanTweenType.easeInCubic);
    }

    public void DefenseOut()
    {
        LeanTween.scale(defenseBonus, Vector3.one * 0.25f, 0.3f)
            .setEase(defenseOutAC);
        LeanTween.alpha(defenseBackground.GetComponent<RectTransform>(), 0, 0.15f)
            .setDelay(0.1f)
            .setEase(LeanTweenType.easeInCubic);
        LeanTween.value(defenseText.gameObject, (float v, float r) => {
            Color c = defenseText.color; c.a = v;
            defenseText.color = c;
        }, defenseText.color.a, 0, 0.15f)
            .setDelay(0.05f)
            .setEase(LeanTweenType.easeInCubic);
    }
}
