using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public bool unlocked;
    public int cost;
    public GameObject before;
    public GameObject after;

    SpriteRenderer skill;
    SpriteRenderer skillAfter;
    SpriteRenderer skillBefore;

    Color[] c = { Color.red, Color.gray, Color.green };
    void Start()
    {
        skill = transform.GetChild(0).GetComponent<SpriteRenderer>();
        skillBefore = before.transform.GetChild(0).GetComponent<SpriteRenderer>();
        skillAfter = after.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // if skillAfter == null

        //else

        if (skillBefore.color == c[0])
        {
            skill.color = c[0];
        }
        else if (skillBefore.color == c[1])
        {
            skill.color = c[0];
        }
        else if (skillBefore.color == c[2])
        {
            if (unlocked == true)
                skill.color = c[2];
            else
                skill.color = c[1];
        }
    }
}
