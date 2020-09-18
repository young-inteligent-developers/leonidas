using UnityEngine;

public class FieldConnection : MonoBehaviour
{
    public const float ALPHA             = 0.902f;

    public Int2 connection;

    [HideInInspector]
    public Field[] fields               = new Field[2];

    FieldManager manager;
    LineRenderer lr;
    Color dColor;
    Color hColor = Color.black;

    void Start()
    {
        manager = transform.parent.parent.GetComponent<FieldManager>();
        lr = GetComponent<LineRenderer>();
        dColor = lr.startColor;
    }

    void Update()
    {
        Vector3 f = fields[0].transform.position;
        Vector3 s = fields[1].transform.position;
        f.z = s.z = transform.position.z;
        lr.SetPosition(0, f);
        lr.SetPosition(1, s);
    }

    public void Highlight(Color toColor, bool save)
    {
        CancelTween();
        manager.highlightedConnections.Add(this);
        if (save) hColor = toColor;

        HighlightTween(0.2f, toColor);
    }

    public void HighlightSwipe(int index)
    {
        Field.Ownership os;
        if (fields[0].index != index)
        {
            manager.actionField = fields[0];
            os = fields[0].ownership;
        }
        else
        {
            manager.actionField = fields[1];
            os = fields[1].ownership;
        }
        Color c = (os == Field.Ownership.Player ? new Color(0.501f, 0.905f, 0.976f, ALPHA) : new Color(0.962f, 0.213f, 0.292f, ALPHA));

        HighlightTween(0.2f, c);
    }

    public void Unhighlight(bool clear)
    {
        CancelTween();

        if (clear) hColor = Color.black;
        Color toColor = hColor == Color.black ? dColor : hColor;

        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, lr.startColor, toColor, 0.2f)
            .setEase(LeanTweenType.easeOutSine);
    }

    void HighlightTween(float time, Color toColor)
    {
        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, lr.startColor, toColor, time)
            .setEase(LeanTweenType.easeInSine);
    }

    void CancelTween()
    {
        LeanTween.cancel(gameObject);
    }
}
