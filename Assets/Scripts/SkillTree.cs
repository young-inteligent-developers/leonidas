using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public int points;
    public GameObject SkillPoint;

    public GameObject attack;
    public GameObject defence;

    public GameObject infoSkillPanel;
    public TextMeshProUGUI infoSkillPanelName;
    public TextMeshProUGUI infoSkillPanelCost;

    private Skill skill;

    void Start()
    {
        SkillPoint.GetComponent<TextMeshProUGUI>().text = points.ToString();
    }

    public void ActivateDefence()
    {
        defence.SetActive(true);
        attack.SetActive(false);

        CloseInfoSkillPanel();

        //SkillPoint.GetComponent<RectTransform>().position = new Vector2(180, 300);
    }

    public void ActivateAttack()
    {
        defence.SetActive(false);
        attack.SetActive(true);

        CloseInfoSkillPanel();

        //SkillPoint.GetComponent<RectTransform>().position = new Vector2(110, 450);
    }

    public void InfoSkillPanel(Skill s)
    {
        skill = s;
        infoSkillPanel.SetActive(true);

        infoSkillPanelName.text = s.name;
        infoSkillPanelCost.text = s.cost.ToString();

        Image info = infoSkillPanel.transform.GetChild(4).GetComponent<Image>();
        Button click = infoSkillPanel.transform.GetChild(4).GetComponent<Button>();

        if (skill.unlocked != true && skill.canUnlock == false)
        {
            info.color = Color.red;
            click.interactable = false;
        }
        else if (skill.unlocked == true)
        {
            info.color = Color.green;
            click.interactable = false;
        }
        else
        {
            info.color = Color.white;
            click.interactable = true;
        }
    }

    public void Unlock()
    {
        Skill s = skill;

        if (s.canUnlock == true && s.unlocked == false)
        {
            int c = s.cost;
            if (points >= c) {
                // Unlocked this skill
                s.unlocked = true;

                // Can unlocked next
                if (s.nextSkill != s)
                    s.nextSkill.canUnlock = true;
                
                // Minus x points
                points -= c;
                SkillPoint.GetComponent<TextMeshProUGUI>().text = points.ToString(); // Start();

                // Color change
                s.RefreshSkill();
                s.nextSkill.RefreshSkill();
            }
        }
    }

    public void CloseInfoSkillPanel()
    {
        infoSkillPanel.SetActive(false);
    }
    public void UnlockInfoSkillPanel()
    {
        Unlock();
        infoSkillPanel.SetActive(false);
    }
}
