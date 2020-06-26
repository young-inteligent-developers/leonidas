using UnityEngine;
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

        Color c = Color.white;
        switch (ownership)
        {
            case Ownership.Player:
                c = new Color(0.254f, 0.823f, 0.976f);
                break;
            case Ownership.Neutral:
                c = new Color(0.670f, 0.647f, 0.749f);
                break;
            case Ownership.Enemy:
                c = new Color(0.968f, 0.262f, 0.262f);
                break;
        }
        fill.color = c;

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        GameObject text = Instantiate(strengthText, canvas);
        text.GetComponent<RectTransform>().position = pos;
        text.GetComponent<TextMeshProUGUI>().text = strength.ToString();
    }

    
    void Update()
    {
        
    }
}
