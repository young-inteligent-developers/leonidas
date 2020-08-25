using UnityEngine;

public class InputManager : MonoBehaviour
{
    public FieldManager fieldManager;
    public ActionPanel attackPanel;
    public ActionPanel defensePanel;

    int inputPhase                      = 1;
    FieldConnectionInfo currentInfo     = null;

    public void CancelSelection()
    {
        inputPhase = 1;
        fieldManager.selectedField.Deselect();
        fieldManager.UnhighlightConnectedFields();
    }

    public void CancelLineHighlight()
    {
        if (currentInfo != null)
        {
            currentInfo.fieldConnection.Unhighlight();
            currentInfo = null;
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        if (!Input.GetMouseButton(0))
        {
            CancelLineHighlight();
            return;
        }
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended) 
            return;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif

        /*RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
        if (!hit)
        {
            if (inputPhase == 2) 
                CancelSelection();
            else if (fieldManager.infoField != null) 
                fieldManager.infoField.HideInfo();

            return;
        }

        if (hit.transform.tag == "Field")
        {
            Field f = hit.transform.GetComponent<Field>();

            if (inputPhase == 1)
            { 
                if (fieldManager.infoField != null && fieldManager.infoField != f)
                    fieldManager.infoField.HideInfo();

                if (f.ownership == Field.Ownership.Player)
                {
                    inputPhase++;
                    f.Select();
                    fieldManager.HighlightConnectedFields(f.index);
                }
                else
                {
                    f.ShowInfo();
                }
            }
            else if (inputPhase == 2)
            {
                if (!f.highlighted)
                {
                    CancelSelection();
                    f.ShowInfo();
                    return;
                }
                    
                //inputPhase++;
                fieldManager.actionField = f;

                ActionPanel ap;
                if (fieldManager.actionField.ownership == fieldManager.selectedField.ownership)
                    ap = defensePanel;
                else
                    ap = attackPanel;
                ap.Set(fieldManager.selectedField.strength);
                ap.Open();
            }
        }*/

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
        if (fieldManager.selectedField == null && hit)
        {
            Field f = hit.transform.GetComponent<Field>();
            if (f.ownership != Field.Ownership.Player)
                return;

            f.Select();
            fieldManager.HighlightFieldConnections(f.index);
        }
        else if (fieldManager.selectedField != null)
        {
            Vector2 sp = fieldManager.selectedField.transform.position;
            Vector2 d = pos - sp;
            float angle = Math.RadToDeg360(Mathf.Atan2(d.y, d.x));
            bool inRange = false;

            foreach (FieldConnectionInfo i in fieldManager.selectedField.fcInfos)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(i.angle, angle)) <= 15)
                {
                    inRange = true;
                    if (i == currentInfo)
                        break;

                    CancelLineHighlight();
                    i.fieldConnection.Highlight(Color.blue);
                    currentInfo = i;
                }
            }
            if (!inRange)
                CancelLineHighlight();
        }
    }
}
