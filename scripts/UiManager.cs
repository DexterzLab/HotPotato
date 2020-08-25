using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;

    public TextMeshProUGUI player1PointsText;
    public TextMeshProUGUI player2PointsText;

    public TextMeshProUGUI playerTurnsText;


    //private variable to declare the instance of this class.
    private static UiManager _instance;
    public static UiManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("ui is null");
            }

            return _instance;
        }
    }

    private void Start()
    {
        //player1Text = player1Text.GetComponent<TextMeshProUGUI>();
        //player2Text = player2Text.GetComponent<TextMeshProUGUI>();

        //player1PointsText = player1PointsText.GetComponent<TextMeshProUGUI>();
        //player2PointsText = player2PointsText.GetComponent<TextMeshProUGUI>();

        //player1Text.SetText("player1");
        player1Text.text = "Player1";
        player2Text.text = "Player2";
    }

    private void Awake()
    {
        _instance = this;


    }

    private void Update()
    {
        
    }

    public void UpdateTurn(string playerturn)
    {
        playerTurnsText.text = playerturn;
    }

    public void UpdateScore(int score)
    {
        Debug.Log("score: " + score);

        Debug.Log("Notifying gamemanager");
    }
}
