using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public Int2[] fieldConnections;

    [Space(16)]

    public GameObject connectionPrefab;
    public Transform connections;
    public Transform canvas;
    public AttackPanel attackPanel;

    List<Field> highlightedFields = new List<Field>();

    public Field GetField(int index)
    {
        if (index <= transform.childCount - 1)
            return transform.GetChild(index - 1).GetComponent<Field>();

        return null;
    }

    public void HighlightConnectedFields(int index)
    {
        foreach (Int2 fc in fieldConnections)
        {
            Field f = null;

            if (fc.first == index)
            {
                f = GetField(fc.second);
                f.Highlight();
            }
            else if (fc.second == index)
            {
                f = GetField(fc.first);
                f.Highlight();
            }

            if (f != null) 
                highlightedFields.Add(f);
        }
    }

    public void UnhighlightConnectedFields()
    {
        foreach (Field f in highlightedFields)
            f.Unhighlight();
        
        highlightedFields.Clear();
    }

    void Start()
    {
        foreach (Int2 fc in fieldConnections)
        {
            LineRenderer l = Instantiate(connectionPrefab, connections).GetComponent<LineRenderer>();
            Vector3 f = GetField(fc.first).transform.position; f.z = 1;
            Vector3 s = GetField(fc.second).transform.position; s.z = 1;
            float d = Vector2.Distance(f, s);

            l.SetPosition(0, Vector3.Lerp(f, s, 1 / d * 0.52f));
            l.SetPosition(1, Vector3.Lerp(s, f, 1 / d * 0.52f));
        }
    }
}
