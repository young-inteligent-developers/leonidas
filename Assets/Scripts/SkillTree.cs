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

    public Int2[] skillConnections;

    public GameObject element;
    public GameObject connections;

    void Start()
    {
        SkillPoint.GetComponent<TextMeshProUGUI>().text = points.ToString();

        RenderLines();
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

            if (sc.first == 1)
                Debug.Log(GetSkill(sc.first, defence.transform).GetComponent<RectTransform>().position);
            
            line.SetPosition(0, f);
            line.SetPosition(1, s);

            SkillConnection skillConn = con.GetComponent<SkillConnection>();
            skillConn.indexes[0] = sc.first;  
            skillConn.indexes[1] = sc.second;  
        }

        connections.transform.GetChild(0).GetComponent<LineRenderer>().startColor = Color.grey;
        connections.transform.GetChild(1).GetComponent<LineRenderer>().startColor = Color.grey;
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
        CloseInfoSkillPanel();
    }
}
