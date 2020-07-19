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

    public void Highlight()
    {
        manager.highlightedConnections.Add(this);

        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, Color.white, new Color(1, 0.929f, 0.705f), 0.3f)
            .setEase(LeanTweenType.easeInSine);
    }

    public void Unhighlight()
    {
        LeanTween.value(gameObject, (Color c) => {
            lr.startColor = lr.endColor = c;
        }, new Color(1, 0.929f, 0.705f), Color.white, 0.3f)
            .setEase(LeanTweenType.easeOutSine);
    }
}
