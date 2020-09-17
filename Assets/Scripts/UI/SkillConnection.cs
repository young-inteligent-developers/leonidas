using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillConnection : MonoBehaviour
{
    [HideInInspector]
    public Skill[] skills = new Skill[2];

    LineRenderer line;
    Color c;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        c = Color.white;

        //RefreshLineColor();
    }

    /*public void RefreshLineColor()
    {
        if (skills[0].unlocked == false && skills[1].unlocked == false)
            c = Color.red;
        else if (skills[0].unlocked == true && skills[1].unlocked == true)
            c = Color.green;
        else if (skills[0].unlocked == true && skills[1].canUnlock == true)
            c = Color.grey;

        line.startColor = c;
        line.endColor = c;
    }*/
}
