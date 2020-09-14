using UnityEngine;
using System.Collections.Generic;

public class Field : MonoBehaviour
{
    public enum Ownership { Player, Neutral, Enemy }

    [HideInInspector]
    public bool highlighted;
    [HideInInspector]
    public List<FieldConnectionInfo> fcInfos = new List<FieldConnectionInfo>();

    [Header("Field properties")]
    public int index;
    public int strength;
    public int defense;
    public Ownership ownership;
    
    FieldManager manager;
    SpriteRenderer fill;
    SpriteRenderer border;
    SpriteRenderer selectRing;
    FieldUI fieldUI;
    Color[] colors = new Color[2];  // field fill [0] and border [1] colors

    public static Color[] GetColors(Ownership os)
    {
        Color[] c = new Color[2];
        switch (os)
        {
            case Ownership.Player:
                c[0] = new Color(0.154f, 0.768f, 0.934f);
                c[1] = new Color(0.501f, 0.905f, 0.976f);
                break;
            case Ownership.Neutral:
                c[0] = new Color(0.470f, 0.420f, 0.622f);
                c[1] = new Color(0.729f, 0.683f, 0.867f);
                break;
            case Ownership.Enemy:
                c[0] = new Color(0.924f, 0.144f, 0.226f);
                c[1] = new Color(0.976f, 0.450f, 0.450f);
                break;
        }

        return c;
    }

    public void Attack(int s)
    {
        if (strength - s < 0) return;

        Field f = manager.actionField;
        if (s > f.strength) f.SetOwnership(ownership);
        f.SetStrength(Mathf.Abs(f.strength - s));
        SetStrength(strength - s);
    }

    public void Regroup(int s)
    {
        if (strength - s < 0) return;

        Field f = manager.actionField;
        f.SetStrength(f.strength + s);
        SetStrength(strength - s);
    }

    public void Select()
    {
        manager.selectedField = this;
        ShowInfo();

        /*selectRing.transform.localScale = Vector3.one * 0.95f;
        LeanTween.scale(selectRing.gameObject, Vector3.one * 1.5f, 0.25f);
        LeanTween.alpha(selectRing.gameObject, 1, 0.07f);
        LeanTween.alpha(selectRing.gameObject, 0, 0.07f)
            .setDelay(0.18f);
        LeanTween.color(border.gameObject, new Color(0.952f, 0.797f, 0.301f), 0.15f)
            .setDelay(0.1f)
            .setEase(LeanTweenType.easeInSine);*/
    }

    public void Deselect()
    {
        manager.selectedField = null;
        HideInfo();
    }

    public void Highlight()
    {
        manager.highlightedFields.Add(this);
        highlighted = true;
        LeanTween.color(border.gameObject, new Color(0.9f, 0.9f, 0.9f), 0.2f)
            .setEase(LeanTweenType.easeInSine);
    }

    public void Unhighlight()
    {
        highlighted = false;
        LeanTween.color(border.gameObject, colors[1], 0.2f)
            .setEase(LeanTweenType.easeOutSine);
    }

    public void ShowInfo()
    {
        fieldUI.DefenseIn();
    }

    public void HideInfo()
    {
        fieldUI.DefenseOut();
    }

    public void SetStrength(int s)
    {
        if (s == strength) return;

        strength = s;
        fieldUI.unitText.text = s.ToString();
    }

    public void SetOwnership(Ownership os)
    {
        if (os == ownership) return;

        ownership = os;
        SetColors(os);
        LeanTween.color(fill.gameObject, colors[0], 0.3f)
            .setEase(LeanTweenType.easeInSine);
    }

    void Start()
    {
        // // // // // // // Private components assignment  // // // // // // //

        manager = transform.parent.GetComponent<FieldManager>();
        fill = transform.Find("Fill").GetComponent<SpriteRenderer>();
        border = transform.Find("Border").GetComponent<SpriteRenderer>();
        selectRing = transform.Find("Select ring").GetComponent<SpriteRenderer>();


        // // // // // // // Field color assignment according its ownership // // // // // // //
        
        SetColors(ownership);
        fill.color = colors[0];
        border.color = colors[1];


        // // // // // // // Field UI creation  // // // // // // //

        Color c = colors[0]; c.a = 0;
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        fieldUI = Instantiate(manager.strengthTextPrefab, manager.fieldUI).GetComponent<FieldUI>();
        fieldUI.GetComponent<RectTransform>().position = pos;
        fieldUI.defenseBackground.color = c;
        fieldUI.unitText.text = strength.ToString();
        fieldUI.defenseText.text = defense.ToString();
    }

    void SetColors(Ownership os)
    {
        colors = GetColors(os);
    }
}
