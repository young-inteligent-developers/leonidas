using UnityEngine;

public class InputManager : MonoBehaviour
{
    public FieldManager fieldManager;
    public ActionPanel attackPanel;
    public ActionPanel regroupPanel;

    int inputPhase                      = 1;
    float idleTime                      = 0;
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
#if UNITY_EDITOR
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

        if (Input.GetMouseButton(0) && startPos == new Vector2(-1, -1))
            startPos = pos;

        DetectSwipe(pos);
        DetectClick(pos);   
    }

    void DetectClick(Vector2 pos)
    {
#if UNITY_EDITOR
        //if (pos == startPos && idleTime < 1) return;
        //Debug.Log(idleTime < 1 && (!Input.GetMouseButtonUp(0) || swipedField));
        if (!Input.GetMouseButtonUp(0) || swipedField) return;
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.GetTouch(0).phase != TouchPhase.Ended) return;
#endif

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
        if (!hit || hit.transform.tag != "Field")
        {
            DeselectField();
            return;
        }
        
        Field f = hit.transform.GetComponent<Field>();
        //Debug.Log("Selected field:\t" + (fieldManager.selectedField != f));
        if (fieldManager.selectedField && fieldManager.selectedField != f)
            fieldManager.selectedField.Deselect();
        if (fieldManager.selectedField != f)
            f.Select();
        else
            f.Deselect();
    }

    void DetectSwipe(Vector2 pos)
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            if (fieldManager.actionField)
            {
                ActionPanel ap;
                if (fieldManager.actionField.ownership == Field.Ownership.Player)
                    ap = regroupPanel;
                else
                    ap = attackPanel;
                ap.Set(swipedField.strength);
                ap.Open();
            }

            startPos = new Vector2(-1, -1);
            inputPhase = 1;
            idleTime = 0;
            swipedField = null;
            fieldManager.actionField = null;
            fieldManager.UnhighlightConnectedFields();

            return;
        }
        if (!Input.GetMouseButton(0))
        {
            return;
        }
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.GetTouch(0).phase != TouchPhase.Ended) return;
#endif
         
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
        if (inputPhase == 1 && hit && hit.transform.tag == "Field")
        {
            Field f = hit.transform.GetComponent<Field>();
            if (f.ownership != Field.Ownership.Player)
                return;
            if (pos == startPos)
            {
                if (idleTime >= 0.6f)
                {
                    if (fieldManager.selectedField && fieldManager.selectedField != f)
                        fieldManager.selectedField.Deselect();
                    if (!fieldManager.selectedField)
                        f.Select();
                }
                idleTime += Time.deltaTime;
                return;
            }

            DeselectField();
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
                    i.fieldConnection.HighlightSwipe(swipedField.index);
                    currentInfo = i;
                }
            }
            if (!inRange)
            {
                fieldManager.actionField = null;
                CancelLineHighlight(false);
            }    
        }
    }

    void DeselectField()
    {
        if (fieldManager.selectedField)
            fieldManager.selectedField.Deselect();
    }
}
