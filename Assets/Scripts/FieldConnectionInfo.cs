using UnityEngine;

public class FieldConnectionInfo
{
    public int angle;
    public FieldConnection fieldConnection;

    public FieldConnectionInfo()
    {
        angle = 0;
        fieldConnection = null;
    }

    public FieldConnectionInfo(FieldConnectionInfo i)
    {
        angle = i.angle;
        fieldConnection = i.fieldConnection;
    }

    public void SetAngle(Vector2 a, Vector2 b)
    {
        Vector2 d = b - a;
        angle = (int) Math.RadToDeg360(Mathf.Atan2(d.y, d.x));
    }
}
