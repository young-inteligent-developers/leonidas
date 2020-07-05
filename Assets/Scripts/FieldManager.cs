using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public Int2[] fieldConnections;

    [Space(16)]

    public GameObject connectionPrefab;
    public Transform connections;
    public Transform canvas;
    public AttackPanel attackPanel;

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
            if (fc.first == index)
                GetField(fc.second).Highlight();
            else if (fc.second == index)
                GetField(fc.first).Highlight();
        }
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
