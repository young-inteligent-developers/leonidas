using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Field : MonoBehaviour
{
    public enum Ownership { Player, Neutral, Enemy }

    public Ownership ownership;

    SpriteRenderer border;
    SpriteRenderer fill;
    
    void Start()
    {
        border = transform.Find("Border").GetComponent<SpriteRenderer>();
        fill = transform.Find("Fill").GetComponent<SpriteRenderer>();

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
    }

    
    void Update()
    {
        
    }
}
