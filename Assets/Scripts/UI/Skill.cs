using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public int id;

    public bool unlocked;
    public bool canUnlock;

    public string skillName;
    public string description;
    public int cost;
    public Skill nextSkill;

    Color[] border = {new Color(0.65f, 0, 0, 1), new Color(0.23f, 0.23f, 0.23f, 1), new Color(0, 0.60f, 0, 1)};
    Color[] insite = { Color.red, Color.gray, Color.green};

    Image img;

    void Start()
    {
        img = GetComponent<Image>();

        if (unlocked == false) 
        {
            img.color = border[0];
            transform.GetChild(0).GetComponent<Image>().color = insite[0];
        }

        RefreshSkillColor();
    }

    public void RefreshSkillColor()
    {
        if (canUnlock == true)
        {
            img.color = border[1];
            transform.GetChild(0).GetComponent<Image>().color = insite[1];
        }

        if (unlocked == true)
        {
            img.color = border[2];
            transform.GetChild(0).GetComponent<Image>().color = insite[2];
        }
    }
}
