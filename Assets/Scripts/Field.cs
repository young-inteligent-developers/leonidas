using UnityEngine;
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
    public Ownership ownership;

    public GameObject strengthText;  

    FieldManager manager;
    AnimColor animColor;
    Animator animator;
    SpriteRenderer border;
    SpriteRenderer fill;
    SpriteRenderer selectRing;
    Color[] colors = new Color[2];

    public void OnSelect()
    {
        animator.SetTrigger("clicked");
        StartCoroutine(WaitForAnimColor(0.25f));
    }

    public void Highlight()
    {
        highlighted = true;
        animColor.Animate(0.4f, colors[1]);
    }

    public void Unhighlight()
    {
        highlighted = false;
        animColor.Animate(0.4f, Color.white);
    }

    void Start()
    {
        manager = transform.parent.GetComponent<FieldManager>();
        animColor = GetComponent<AnimColor>();
        animator = GetComponent<Animator>();
        border = transform.Find("Border").GetComponent<SpriteRenderer>();
        fill = transform.Find("Fill").GetComponent<SpriteRenderer>();
        selectRing = transform.Find("Select ring").GetComponent<SpriteRenderer>();

        Color[] c = { Color.white, Color.white };
        switch (ownership)
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
        colors[0] = fill.color = c[0];
        colors[1] = c[1];

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GameObject strengthUi = Instantiate(strengthText, manager.canvas);
        strengthUi.GetComponent<RectTransform>().position = pos;
        Transform text = strengthUi.transform.GetChild(0);
        text.GetComponent<TextMeshProUGUI>().text = strength.ToString();
    }

    IEnumerator WaitForAnimColor(float s)
    {
        yield return new WaitForSeconds(s);
        animColor.Animate(0.3f, new Color(0.952f, 0.797f, 0.301f));
    }
}
