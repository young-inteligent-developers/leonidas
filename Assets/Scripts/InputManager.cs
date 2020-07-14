﻿using UnityEngine;

public class InputManager : MonoBehaviour
{
    public FieldManager fieldManager;
    public ActionPanel attackPanel;
    public ActionPanel defensePanel;

    int inputPhase = 1;

    public void CancelSelection()
    {
        inputPhase = 1;
        fieldManager.selectedField.Deselect();
        fieldManager.UnhighlightConnectedFields();
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
            if (!hit)
            {
                if (inputPhase == 2) CancelSelection();
                return;
            }

            if (hit.transform.tag == "Field")
            {
                Field f = hit.transform.GetComponent<Field>();

                if (inputPhase == 1 && f.ownership == Field.Ownership.Player)
                {
                    inputPhase++;
                    f.Select();
                    fieldManager.HighlightConnectedFields(f.index);
                }
                else if (inputPhase == 2)
                {
                    if (!f.highlighted) return;
                    
                    inputPhase++;
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
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount == 0) return;

        Touch t = Input.GetTouch(0);
        if (t.phase == TouchPhase.Ended)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.up, 0.01f);
            if (!hit)
            {
                if (inputPhase == 2) CancelSelection();
                return;
            }

            if (hit.transform.tag == "Field")
            {
                Field f = hit.transform.GetComponent<Field>();

                if (inputPhase == 1 && f.ownership == Field.Ownership.Player)
                {
                    inputPhase++;
                    f.Select();
                    fieldManager.HighlightConnectedFields(f.index);
                    attackPanel.Set(f.strength);
                }
                else if (inputPhase == 2)
                {
                    if (!f.highlighted) return;
                    
                    inputPhase++;
                    fieldManager.actionField = f;
                    attackPanel.Open();
                }
            }
        }
#endif
    }

}
