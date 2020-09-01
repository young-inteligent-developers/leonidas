using UnityEngine;

public class FieldConnection : MonoBehaviour
{
    public Int2 connection;

    FieldManager manager;
    LineRenderer lr;
    Color dColor;
    Color hColor = Color.black;

    private void Start()
    {
        manager = transform.parent.parent.GetComponent<FieldManager>();
        lr = GetComponent<LineRenderer>();
        dColor = lr.startColor;
    }

    public void Highlight(Color toColor, bool save)
    {
        manager.highlightedConnections.Add(this);
        if (save) hColor = toColor;

        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, lr.startColor, toColor, 0.3f)
            .setEase(LeanTweenType.easeInSine);
    }

    public void Unhighlight(bool clear)
    {
        if (clear) hColor = Color.black;
        Color toColor = hColor == Color.black ? dColor : hColor;

        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, lr.startColor, toColor, 0.3f)
            .setEase(LeanTweenType.easeOutSine);
    }
}
