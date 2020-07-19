using UnityEngine;

public class FieldConnection : MonoBehaviour
{
    public Int2 connection;

    LineRenderer lr;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Highlight()
    {
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
