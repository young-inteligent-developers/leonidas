﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Field : MonoBehaviour
{
    public enum Ownership { Player, Neutral, Enemy }

    [HideInInspector]
    public bool highlighted;
    public int index;
    public int strength;
    public int defense;
    public Ownership ownership;
    
    FieldManager manager;
    AnimColor fillAC;               // field fill color animation
    AnimColor borderAC;             // field border color animation
    Animator animator;
    SpriteRenderer fill;
    SpriteRenderer border;
    SpriteRenderer selectRing;
    FieldUI fieldUI;
    Color[] colors = new Color[2];  // field fill [0] and border [1] colors

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

        animator.SetTrigger("selected");
        fieldUI.GetComponent<Animator>().SetTrigger("selected");
        StartCoroutine(WaitForAnimColor(0.25f));
    }

    public void Deselect()
    {
        manager.selectedField = null;
        borderAC.Animate(0.3f, Color.white);
        fieldUI.GetComponent<Animator>().SetTrigger("deselected");
    }

    public void Highlight()
    {
        highlighted = true;
        borderAC.Animate(0.4f, colors[1]);
    }

    public void Unhighlight()
    {
        highlighted = false;
        borderAC.Animate(0.3f, Color.white);
    }

    public void ShowInfo()
    {

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
        fillAC.Animate(0.3f, colors[0]);
    }

    void Start()
    {
        // // // // // // // Private components assignment  // // // // // // //

        manager = transform.parent.GetComponent<FieldManager>();
        animator = GetComponent<Animator>();
        fill = transform.Find("Fill").GetComponent<SpriteRenderer>();
        border = transform.Find("Border").GetComponent<SpriteRenderer>();
        selectRing = transform.Find("Select ring").GetComponent<SpriteRenderer>();
        fillAC = fill.GetComponent<AnimColor>();
        borderAC = border.GetComponent<AnimColor>();


        // // // // // // // Field color assignment according its ownership // // // // // // //
        
        SetColors(ownership);
        fill.color = colors[0];


        // // // // // // // Field UI creation  // // // // // // //

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        fieldUI = Instantiate(manager.strengthTextPrefab, manager.canvas).GetComponent<FieldUI>();
        fieldUI.GetComponent<RectTransform>().position = pos;
        fieldUI.unitText.text = strength.ToString();
        fieldUI.defenseText.text = defense.ToString();
        fieldUI.defenseBackground.color = colors[1];
    }

    void SetColors(Ownership os)
    {
        Color[] c = { Color.white, Color.white };
        switch (os)
        {
            case Ownership.Player:
                c[0] = new Color(0.254f, 0.823f, 0.976f);
                c[1] = new Color(0.501f, 0.905f, 0.976f);
                break;
            case Ownership.Neutral:
                c[0] = new Color(0.470f, 0.420f, 0.622f);
                c[1] = new Color(0.729f, 0.683f, 0.867f);
                break;
            case Ownership.Enemy:
                c[0] = new Color(0.968f, 0.262f, 0.262f);
                c[1] = new Color(0.976f, 0.450f, 0.450f);
                break;
        }

        colors = c;
    }

    IEnumerator WaitForAnimColor(float s)
    {
        yield return new WaitForSeconds(s);
        borderAC.Animate(0.3f, new Color(0.952f, 0.797f, 0.301f));
    }
}
