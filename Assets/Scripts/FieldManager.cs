using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public Int2[] fieldConnections;

    [Space(16)]

    public GameObject strengthTextPrefab;
    public GameObject connectionPrefab;
    public Transform connections;
    public Transform canvas;

    [HideInInspector]
    public Field selectedField;
    [HideInInspector]
    public Field actionField;
    [HideInInspector]
    public Field infoField;
    List<Field> fields = new List<Field>();
    List<Field> highlightedFields = new List<Field>();
    List<FieldConnection> fConnections = new List<FieldConnection>();
    List<FieldConnection> highlightedConnections = new List<FieldConnection>();

    public Field GetField(int index)
    {
        if (index > 0 && index <= transform.childCount - 1)
            return transform.GetChild(index - 1).GetComponent<Field>();

        return null;
    }

    public FieldConnection GetFieldConnection(int index)
    {
        foreach (FieldConnection fc in fConnections)
            if (fc.connection.first == index || fc.connection.second == index)
                return fc;

        return null;
    }

    public void HighlightConnectedFields(int index)
    {
        foreach (Int2 fc in fieldConnections)
        {
            Field f = null;

            if (fc.first == index)
                f = GetField(fc.second);
            else if (fc.second == index)
                f = GetField(fc.first);

            if (f != null)
            {
                f.Highlight();
                highlightedFields.Add(f);

                FieldConnection fCon = GetFieldConnection(index);
                highlightedConnections.Add(fCon);
                fCon.Highlight();
            }
        }
    }

    public void UnhighlightConnectedFields()
    {
        foreach (Field f in highlightedFields) f.Unhighlight();
        highlightedFields.Clear();

        foreach (FieldConnection fc in highlightedConnections) fc.Unhighlight();
        highlightedConnections.Clear();
    }

    public void IncreaseUnits(Field.Ownership os, int v)
    {
        foreach (Field f in fields)
            if (f.ownership == os)
                f.SetStrength(f.strength + v);
    }

    public void DecreaseUnits(Field.Ownership os, int v)
    {
        foreach (Field f in fields)
            if (f.ownership == os)
                f.SetStrength(f.strength - v);
    }

    void Start()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
            fields.Add(transform.GetChild(i).GetComponent<Field>());

        foreach (Int2 fc in fieldConnections)
        {
            FieldConnection fCon = Instantiate(connectionPrefab, connections).GetComponent<FieldConnection>();
            fCon.connection = fc;
            fConnections.Add(fCon);

            LineRenderer l = fCon.GetComponent<LineRenderer>();
            Vector3 f = GetField(fc.first).transform.position; f.z = 1;
            Vector3 s = GetField(fc.second).transform.position; s.z = 1;
            float d = Vector2.Distance(f, s);

            l.SetPosition(0, Vector3.Lerp(f, s, 1 / d * 0.52f));
            l.SetPosition(1, Vector3.Lerp(s, f, 1 / d * 0.52f));
        }
    }
}
