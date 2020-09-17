using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldUI : MonoBehaviour
{
    [HideInInspector]
    public bool isAnimating = false;

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
        CancelAllTweens();
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
        CancelAllTweens();

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

    public void UpdateColor(Color c)
    {
        c.a = defenseBackground.color.a;
        LeanTween.cancel(defenseBackground.gameObject);
        LeanTween.value(defenseBackground.gameObject, (Color col) => {
            defenseBackground.color = col;
        }, defenseBackground.color, c, 0.2f)
            .setEase(LeanTweenType.easeInSine);
    }

    void CancelAllTweens()
    {
        LeanTween.cancel(defenseBonus.gameObject);
        LeanTween.cancel(defenseBackground.gameObject);
        LeanTween.cancel(defenseText.gameObject);
    }
}
