using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public Int2[] fieldConnections;

    [Space(16)]

    public GameObject strengthTextPrefab;
    public GameObject connectionPrefab;
    public Transform connections;
    public Transform fieldUI;

    [HideInInspector]
    public Field selectedField;
    [HideInInspector]
    public Field actionField;
    List<Field> fields = new List<Field>();
    [HideInInspector]
    public List<Field> highlightedFields = new List<Field>();
    List<FieldConnection> fConnections = new List<FieldConnection>();
    [HideInInspector]
    public List<FieldConnection> highlightedConnections = new List<FieldConnection>();

    void Start()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
            fields.Add(transform.GetChild(i).GetComponent<Field>());

        foreach (Int2 fc in fieldConnections)
        {
            FieldConnection fCon = Instantiate(connectionPrefab, connections).GetComponent<FieldConnection>();
            fCon.connection = fc;
            fCon.fields = new Field[] { GetField(fc.first), GetField(fc.second) };
            fConnections.Add(fCon);

            Vector3 f = fCon.fields[0].transform.position; f.z = 1;
            Vector3 s = fCon.fields[1].transform.position; s.z = 1;
            LineRenderer l = fCon.GetComponent<LineRenderer>();
            l.SetPosition(0, f);
            l.SetPosition(1, s);

            FieldConnectionInfo i = new FieldConnectionInfo();
            i.fieldConnection = fCon;
            i.SetAngle(f, s);
            GetField(fc.first).fcInfos.Add(new FieldConnectionInfo(i));
            i.SetAngle(s, f);
            GetField(fc.second).fcInfos.Add(new FieldConnectionInfo(i));
        }
    }

    public Field GetField(int index)
    {
        if (index > 0 && index <= transform.childCount - 1)
            return transform.GetChild(index - 1).GetComponent<Field>();

        return null;
    }

    public void OnStartOfTurn()
    {
        foreach (Field f in fields)
        {
            f.ctStrength = f.strength;
        }
    }

    public bool IsGameOver()
    {
        int nonPlayerFields = 0;
        foreach (Field f in fields)
            if (f.ownership != Field.Ownership.Player)
                nonPlayerFields++;

        return nonPlayerFields == 0;
    }

    public void HighlightFieldConnections(int index)
    {
        foreach (FieldConnection fc in fConnections)
        {
            if (fc.connection.first != index && fc.connection.second != index)
                continue;
            
            fc.Highlight(new Color(1, 1, 1, FieldConnection.ALPHA), true);
        }
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
                f.Highlight();
        }

        HighlightFieldConnections(index);
    }

    public void UnhighlightConnectedFields()
    {
        foreach (Field f in highlightedFields) f.Unhighlight();
        highlightedFields.Clear();

        foreach (FieldConnection fc in highlightedConnections) fc.Unhighlight(true);
        highlightedConnections.Clear();
    }

    public void IncreaseUnits(Field.Ownership os, int v)
    {
        foreach (Field f in fields)
            if (f.ownership == os)
                f.SetStrength(f.strength + v, true);
    }

    public void DecreaseUnits(Field.Ownership os, int v)
    {
        foreach (Field f in fields)
            if (f.ownership == os)
                f.SetStrength(f.strength - v, true);
    }
}
