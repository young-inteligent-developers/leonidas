using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isPlayerTurn    = true;
    [HideInInspector]
    public int turn             = 1;
    [HideInInspector]
    public int gamePoints       = 50;
    [HideInInspector]
    public int unlockPoints     = 50;

    [Header("Properties")]
    public int recruitmentBonus;

    [Header("Objects")]
    public InputManager inputManager;
    public FieldManager fieldManager;
    public TextMeshProUGUI turnText;

    public static GameManager instance;

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

    void Start()
    {
        if (!instance)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }
}
