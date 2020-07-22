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

    Color[] c = { Color.red, Color.gray, Color.green};

    void Start()
    {
        if (unlocked == false) 
        {
            this.GetComponent<Image>().color = c[0];
        }

        RefreshSkill();
    }

    public void RefreshSkill()
    {
        if (canUnlock == true)
        {
            this.GetComponent<Image>().color = c[1];
        }

        if (unlocked == true)
        {
            this.GetComponent<Image>().color = c[2];
        }
    }
}
