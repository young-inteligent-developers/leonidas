using UnityEngine;

public class FieldConnection : MonoBehaviour
{
    public Int2 connection;

    FieldManager manager;
    LineRenderer lr;

    private void Start()
    {
        manager = transform.parent.parent.GetComponent<FieldManager>();
        lr = GetComponent<LineRenderer>();
    }

    public void Highlight(Color toColor)
    {
        manager.highlightedConnections.Add(this);

        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, Color.white, toColor, 0.3f)
            .setEase(LeanTweenType.easeInSine);
    }

    public void Unhighlight()
    {
        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, lr.startColor, Color.white, 0.3f)
            .setEase(LeanTweenType.easeOutSine);
    }
}
