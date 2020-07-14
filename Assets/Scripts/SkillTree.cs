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

    public void Unlocked(GameObject skill)
    {
        if (skill.GetComponent<Skill>().CanUnlocked == true && skill.GetComponent<Skill>().Unlocked == false)
        {
            // Unlocked this skill
            skill.GetComponent<Skill>().Unlocked = true;

            // Can unlocked next
            GameObject afterSkill = skill.GetComponent<Skill>().afterSkill;
            afterSkill.GetComponent<Skill>().CanUnlocked = true;

            // Minus x points
            int p = int.Parse(points);
            p -= skill.GetComponent<Skill>().Cost;
            points = p.ToString();
        }
    }
}
