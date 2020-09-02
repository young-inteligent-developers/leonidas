using UnityEngine;

public class InputManager : MonoBehaviour
{
    public FieldManager fieldManager;
    public ActionPanel attackPanel;
    public ActionPanel defensePanel;

    int inputPhase                      = 1;
    Field swipedField                   = null;
    Vector2 startPos                    = new Vector2(-1, -1);
    FieldConnectionInfo currentInfo     = null;

    public void CancelSelection()
    {
        inputPhase = 1;
        //fieldManager.selectedField.Deselect();
        fieldManager.UnhighlightConnectedFields();
    }

    public void CancelLineHighlight(bool c)
    {
        if (currentInfo != null)
        {
            currentInfo.fieldConnection.Unhighlight(c);
            currentInfo = null;
        }
    }

    void Update()
    {
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

#if UNITY_EDITOR
        if (!Input.GetMouseButton(0) && !Input.GetMouseButtonUp(0))
        {
            startPos = new Vector2(-1, -1);
            inputPhase = 1;
            fieldManager.UnhighlightConnectedFields();

            return;
        }
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount != 1)
        {
            startPos = new Vector2(-1, -1);
            inputPhase = 1;
            fieldManager.UnhighlightConnectedFields();

            return;
        }
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        if (startPos == new Vector2(-1, -1))
            startPos = pos;

        if (pos == startPos)
        {
            OnClick(pos);
        }
        else
        {
            if (fieldManager.selectedField)
                fieldManager.selectedField.Deselect();

            OnSwipe(pos);
        }
    }

    void OnClick(Vector2 pos)
    {
#if UNITY_EDITOR
        if (!Input.GetMouseButtonUp(0)) return;
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.GetTouch(0).phase != TouchPhase.Ended) return;
#endif

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
        if (!hit || hit.transform.tag != "Field")
        {
            if (fieldManager.selectedField)
                fieldManager.selectedField.Deselect();

            return;
        }

        Field f = hit.transform.GetComponent<Field>();
        if (fieldManager.selectedField && fieldManager.selectedField != f)
            fieldManager.selectedField.Deselect();
        if (fieldManager.selectedField != f)
            f.Select();
        else
            f.Deselect();
    }

    void OnSwipe(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
        if (inputPhase == 1 && hit && hit.transform.tag == "Field")
        {
            Field f = hit.transform.GetComponent<Field>();
            if (f.ownership != Field.Ownership.Player)
                return;

            inputPhase = 2;
            swipedField = f;
            fieldManager.HighlightFieldConnections(f.index);
        }
        else if (inputPhase == 2)
        {
            Vector2 sp = swipedField.transform.position;
            Vector2 d = pos - sp;
            float angle = Math.RadToDeg360(Mathf.Atan2(d.y, d.x));
            bool inRange = false;

            foreach (FieldConnectionInfo i in swipedField.fcInfos)
            {
                if (Mathf.Abs(Mathf.DeltaAngle(i.angle, angle)) <= 20)
                {
                    inRange = true;
                    if (i == currentInfo)
                        break;

                    CancelLineHighlight(false);
                    i.fieldConnection.Highlight(new Color(0.154f, 0.768f, 0.934f, 0.902f), false);
                    currentInfo = i;
                }
            }
            if (!inRange) CancelLineHighlight(false);
        }
    }
}
