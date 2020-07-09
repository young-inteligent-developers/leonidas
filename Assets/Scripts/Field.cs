using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    Animator animator;
    SpriteRenderer border;
    SpriteRenderer fill;
    SpriteRenderer highlight;

    public void OnClick()
    {
        manager.attackPanel.Open(strength);
    }

    public void Highlight()
    {
        bool b = true;

        highlighted = b;
        animator.SetBool("highlight", b);
        highlight.gameObject.SetActive(b);
    }

    public void Unhighlight()
    {
        bool b = false;

        highlighted = b;
        animator.SetBool("highlight", b);
        highlight.gameObject.SetActive(b);
    }

    void Start()
    {
        manager = transform.parent.GetComponent<FieldManager>();
        animator = GetComponent<Animator>();
        border = transform.Find("Border").GetComponent<SpriteRenderer>();
        fill = transform.Find("Fill").GetComponent<SpriteRenderer>();
        highlight = transform.Find("Highlight").GetComponent<SpriteRenderer>();

        Color[] c = { Color.white, Color.white };
        switch (ownership)
        {
            case Ownership.Player:
                c[0] = new Color(0.254f, 0.823f, 0.976f);
                c[1] = new Color(0.501f, 0.905f, 0.976f);
                break;
            case Ownership.Neutral:
                c[0] = new Color(0.670f, 0.647f, 0.749f);
                c[1] = new Color(0.768f, 0.756f, 0.827f);
                break;
            case Ownership.Enemy:
                c[0] = new Color(0.968f, 0.262f, 0.262f);
                c[1] = new Color(0.976f, 0.450f, 0.450f);
                break;
        }
        fill.color = c[0];

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GameObject strengthUi = Instantiate(strengthText, manager.canvas);
        strengthUi.GetComponent<RectTransform>().position = pos;
        Image background = strengthUi.transform.GetChild(0).GetComponent<Image>();
        background.color = c[0];
        Transform text = strengthUi.transform.GetChild(1);
        text.GetComponent<TextMeshProUGUI>().text = strength.ToString();
    }
}
