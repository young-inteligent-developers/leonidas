using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public Int2[] fieldConnections;

    [Space(16)]

    public GameObject connectionPrefab;
    public Transform connections;
    public Transform canvas;
    public AttackPanel attackPanel;

    void Start()
    {
        foreach (Int2 fc in fieldConnections)
        {
            LineRenderer l = Instantiate(connectionPrefab, connections).GetComponent<LineRenderer>();
            Vector3 f = transform.GetChild(fc.first - 1).position; f.z = 1;
            Vector3 s = transform.GetChild(fc.second - 1).position; s.z = 1;
            float d = Vector2.Distance(f, s);

            l.SetPosition(0, Vector3.Lerp(f, s, 1 / d * 0.52f));
            l.SetPosition(1, Vector3.Lerp(s, f, 1 / d * 0.52f));
        }
    }
}
