using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public string points;
    public GameObject SkillPoint;

    public GameObject attack;
    public GameObject defence;

    void Start()
    {
        SkillPoint.GetComponent<Text>().text = points;
    }

    public void DefenceActive()
    {
        defence.SetActive(true);
        attack.SetActive(false);

        SkillPoint.GetComponent<Transform>().position = new Vector2(240, 400);
    }

    public void AttackActive()
    {
        defence.SetActive(false);
        attack.SetActive(true);

        SkillPoint.GetComponent<Transform>().position = new Vector2(90, 450);
    }
}
