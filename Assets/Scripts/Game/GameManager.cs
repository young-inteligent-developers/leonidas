using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [NonSerialized]
    public bool isPlayerTurn    = true;
    [NonSerialized]
    public int turn             = 1;
    [NonSerialized]
    public int gamePoints       = 50;
    [NonSerialized]
    public int unlockPoints     = 50;

    [Header("Properties")]
    public int recruitmentBonus;

    [Header("Objects")]
    public InputManager inputManager;
    public FieldManager fieldManager;
    public TextMeshProUGUI turnText;

    public static GameManager instance;

    void Start()
    {
        if (!instance)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    public void EndGame()
    {
        Debug.Log("Game is over! =)");
    }

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        SetTurn(turn + 1);
        inputManager.ResetInput();
        fieldManager.OnStartOfTurn();

        if (isPlayerTurn)
        {
            fieldManager.IncreaseUnits(Field.Ownership.Player, recruitmentBonus);
            inputManager.enabled = true;
        }
        else
        {
            fieldManager.IncreaseUnits(Field.Ownership.Enemy, recruitmentBonus);
            inputManager.enabled = false;
        }
    }

    public void SetTurn(int v)
    {
        turn = v;
        turnText.text = v.ToString();
    }
}
