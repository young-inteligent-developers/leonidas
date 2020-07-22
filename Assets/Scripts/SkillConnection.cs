using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillConnection : MonoBehaviour
{
    public Int2[] skillConnections;

    public GameObject element;
    public GameObject connections;

    Camera cam;

    public Skill GetSkill(int id)
    {
        if (id > 0 && id <= transform.childCount - 1)
            return transform.GetChild(id - 1).GetComponent<Skill>();

        return null;
    }

    public void LineColor(Skill s)
    {
        Debug.Log(s.id);
        //connections.transform.GetChild(s.id).GetComponent<LineRenderer>().startColor = Color.red;
        Debug.Log(connections.transform.GetChild(s.id).GetComponent<LineRenderer>());
    }

    void Start()
    {
        cam = Camera.main;

        foreach (Int2 sc in skillConnections) 
        {

            GameObject con = Instantiate(element, connections.transform);

            LineRenderer line = con.GetComponent<LineRenderer>();

            Vector3 f = cam.ScreenToWorldPoint(GetSkill(sc.first).transform.position); f.z = 1;
            Vector3 s = cam.ScreenToWorldPoint(GetSkill(sc.second).transform.position); s.z = 1;

            line.SetPosition(0, f);
            line.SetPosition(1, s);
        }
    }
}
