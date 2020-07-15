using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    public string points;
    public GameObject SkillPoint;

    public GameObject attack;
    public GameObject defence;

    void Update()
    {
        SkillPoint.GetComponent<TextMeshProUGUI>().text = points;
    }

    public void ActivateDefence()
    {
        defence.SetActive(true);
        attack.SetActive(false);

        //SkillPoint.GetComponent<RectTransform>().position = new Vector2(180, 300);
    }

    public void ActivateAttack()
    {
        defence.SetActive(false);
        attack.SetActive(true);

        //SkillPoint.GetComponent<RectTransform>().position = new Vector2(110, 450);
    }

    public void Unlock(GameObject skill)
    {
        if (skill.GetComponent<Skill>().canUnlock == true && skill.GetComponent<Skill>().unlocked == false)
        {
            int p = int.Parse(points);
            int c = skill.GetComponent<Skill>().cost;
            if (p >= c) {
                // Unlocked this skill
                skill.GetComponent<Skill>().unlocked = true;

                // Can unlocked next
                GameObject afterSkill = skill.GetComponent<Skill>().afterSkill;
                afterSkill.GetComponent<Skill>().canUnlock = true;

                // Minus x points
                p -= c;
                points = p.ToString();
            }
        }
    }
}
