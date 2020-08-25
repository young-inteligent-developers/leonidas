using UnityEngine;

public class FieldConnection : MonoBehaviour
{
    public Int2 connection;

    FieldManager manager;
    LineRenderer lr;
    Color dColor;

    private void Start()
    {
        manager = transform.parent.parent.GetComponent<FieldManager>();
        lr = GetComponent<LineRenderer>();
        dColor = lr.startColor;
    }

    public void Highlight(Color toColor)
    {
        manager.highlightedConnections.Add(this);

        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, lr.startColor, toColor, 0.3f)
            .setEase(LeanTweenType.easeInSine);
    }

    public void Unhighlight()
    {
        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, lr.startColor, dColor, 0.3f)
            .setEase(LeanTweenType.easeOutSine);
    }
}
