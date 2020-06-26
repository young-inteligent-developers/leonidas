using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Field : MonoBehaviour
{
    public enum Ownership { Player, Neutral, Enemy }

    public int strength;
    public Ownership ownership;

    public GameObject strengthText;

    SpriteRenderer border;
    SpriteRenderer fill;
    Transform canvas;
    
    void Start()
    {
        border = transform.Find("Border").GetComponent<SpriteRenderer>();
        fill = transform.Find("Fill").GetComponent<SpriteRenderer>();
        canvas = GameObject.Find("Canvas").transform;

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
        GameObject strengthUi = Instantiate(strengthText, canvas);
        strengthUi.GetComponent<RectTransform>().position = pos;
        Image background = strengthUi.transform.GetChild(0).GetComponent<Image>();
        background.color = c[1];
        Transform text = strengthUi.transform.GetChild(1);
        text.GetComponent<TextMeshProUGUI>().text = strength.ToString();
    }

    
    void Update()
    {
        
    }
}
