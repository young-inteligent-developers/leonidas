using UnityEngine;

public class InputManager : MonoBehaviour
{
    public FieldManager fieldManager;
    public ActionPanel attackPanel;
    public ActionPanel defensePanel;

    int inputPhase                      = 1;

    public void CancelSelection()
    {
        inputPhase = 1;
        fieldManager.selectedField.Deselect();
        fieldManager.UnhighlightConnectedFields();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (!Input.GetMouseButtonUp(0)) return;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Ended) 
            return;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif

        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
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
        }
    }
}
