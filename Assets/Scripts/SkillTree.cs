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
    public TextMeshProUGUI infoSkillPanelDescription;
    public TextMeshProUGUI infoSkillPanelCost;

    private Skill skill;
    SkillConnection skillConn;

    public Int2[] skillConnections;

    public GameObject element;
    //public GameObject deffElement;
    //public GameObject attaElement;
    public GameObject connections;
    //public GameObject deffConnections;
    //public GameObject attaConnections;

    void Start()
    {
        SkillPoint.GetComponent<TextMeshProUGUI>().text = points.ToString();

        RenderLines();
        RefreshLineColor(connections);
    }

    public Skill GetSkill(int id, Transform t)
    {
        if (id > 0 && id <= t.childCount - 1)
            return t.GetChild(id - 1).GetComponent<Skill>();

        return null;
    }

    void RenderLines()
    {
        Camera cam = Camera.main;

        foreach (Int2 sc in skillConnections)
        {
            GameObject con = Instantiate(element, connections.transform);

            LineRenderer line = con.GetComponent<LineRenderer>();

            Vector3 f = cam.ScreenToWorldPoint(GetSkill(sc.first, defence.transform).GetComponent<RectTransform>().position); f.z = 1;
            Vector3 s = cam.ScreenToWorldPoint(GetSkill(sc.second, defence.transform).GetComponent<RectTransform>().position); s.z = 1;

            line.SetPosition(0, f);
            line.SetPosition(1, s);

            skillConn = con.GetComponent<SkillConnection>();
            skillConn.skills[0] = GetSkill(sc.first, defence.transform);
            skillConn.skills[1] = GetSkill(sc.second, defence.transform); 
        }
    }

    public void ActivateDefence()
    {
        defence.SetActive(true);
        attack.SetActive(false);

        CloseInfoSkillPanel();
    }

    public void ActivateAttack()
    {
        defence.SetActive(false);
        attack.SetActive(true);

        CloseInfoSkillPanel();
    }

    public void InfoSkillPanel(Skill s)
    {
        skill = s;
        infoSkillPanel.SetActive(true);

        infoSkillPanelName.text = s.skillName;
        infoSkillPanelDescription.text = s.description;
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
                SkillPoint.GetComponent<TextMeshProUGUI>().text = points.ToString();

                // Color change
                s.RefreshSkillColor();
                s.nextSkill.RefreshSkillColor();

                RefreshLineColor(connections);
            }
        }
    }

    public void RefreshLineColor(GameObject c)
    {
        Color cr = Color.white;

        Debug.Log(c.transform.childCount);

        for (int i = 0; i < c.transform.childCount; i++)
        {
            SkillConnection sc = c.transform.GetChild(i).GetComponent<SkillConnection>();

            if (sc.skills[0].unlocked == true && sc.skills[1].unlocked == true)
                cr = Color.green;
            else if (sc.skills[0].unlocked == true && sc.skills[1].canUnlock == true)
                cr = Color.grey;
            else
                cr = Color.red;

            sc.GetComponent<LineRenderer>().startColor = cr;
            sc.GetComponent<LineRenderer>().endColor = cr;
        }
        
    }

    public void CloseInfoSkillPanel()
    {
        infoSkillPanel.SetActive(false);
    }
    public void UnlockInfoSkillPanel()
    {
        Unlock();
        CloseInfoSkillPanel();
    }
}
