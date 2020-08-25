using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public int player1Points;
    public int player2Points;

    public Transform spawnpoint;
    public Transform spawnpoint2;

    public GameObject Ball;
   

    public GameObject player1Goalobj;
    public GameObject player2Goalobj;

    public Color colorOfPlayer;
   // Material myMat;
    Color player1Color = Color.red;
    Color player2Color = Color.blue;

    public enum PlayerTurns
    {
        Player1Active,
        Player2Active
    }

    public enum PlayerDeaths
    {
        Player1Death,
        Player2Death
    }

    public enum PlayerScored
    {
        Player1Scored,
        Player2Scored
    }

    public PlayerTurns playerTurns;
    public PlayerDeaths playerDeaths;
    public PlayerScored playerToScore;

    //private variable to declare the instance of this class.
    private static GameplayManager _instance;
    public static GameplayManager Instance
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
    

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        playerTurns = PlayerTurns.Player1Active;
        playerToScore = PlayerScored.Player1Scored;

        

        player1Goalobj = GameObject.FindWithTag("Player1Goal");
        player2Goalobj = GameObject.FindWithTag("Player2Goal");

        CheckPlayer();

        //Ball.GetComponent<Renderer>().material.color = colorOfPlayer;
        //myMat = Ball.GetComponent<Renderer>().material;
        //myMat.color = colorOfPlayer;
    }

    private void Update()
    {

        UiManager.Instance.player1PointsText.text = player1Points.ToString();   
        UiManager.Instance.player2PointsText.text = player2Points.ToString();


    }

    public void CheckColor(Color colorToAssign)
    {
        colorOfPlayer = colorToAssign;

        Ball.GetComponent<Renderer>().material.color = colorToAssign;

    }

    public void CheckDeath()
    {
        //check what the value of playerdeaths is if the function is called.
        switch (playerDeaths)
        {
            //if the value is player1death when the function is called, give points to the opposing player and make it thier turn
            //and make the value for the switch equal to the opposing player, etc.

            case PlayerDeaths.Player1Death:
                Debug.Log("player 1 has died");
                player2Points++;      
                playerTurns = PlayerTurns.Player2Active;

                //if the player has died, it means they have failed to score so that turn will be passed on to the next player
                playerToScore = PlayerScored.Player2Scored;
                break;

            case PlayerDeaths.Player2Death:
                player1Points++;
                Debug.Log("player 2 has died");
                playerTurns = PlayerTurns.Player1Active;
                playerToScore = PlayerScored.Player1Scored;
                break;
        }

        /* if(playerTurns == PlayerTurns.Player2Active)
                 {

                 }
                 */

    }

    public void CheckPlayer()
    {

        //check what the value of playerturns is if the function is called, this will be called after checking for the death of the player.
        switch (playerTurns)
        {
            //once the turns have been changed after death, spawn the ball at the opposing side and set that it is player1s turn to die

            //if it is player1s turn spawn them at thier safe zone
            case PlayerTurns.Player1Active:
                //UiManager.Instance.UpdateTurn("It is" + playerTurns + "turn");
                UiManager.Instance.UpdateTurn("It is Player1's turn");

                //turn the goals on and off to stop registering collision when it spawns
                player1Goalobj.SetActive(false);
                player2Goalobj.SetActive(true);

                //make sure on there turn, tell the manager it also thier turn to die, then spawn them at thier starting possition
                Debug.Log("It is player 1's turn");
                playerDeaths = PlayerDeaths.Player1Death;
                //it is this players turn to score
                playerToScore = PlayerScored.Player1Scored;
                //change the players color to indicate clearer whos turn it is
                CheckColor(player1Color);
               // colorOfPlayer = player1Color;
                Ball.transform.position = spawnpoint.position;
                break;

            //if it is player1s turn spawn them at thier safe zone
            case PlayerTurns.Player2Active:
                //UiManager.Instance.UpdateTurn("It is" + playerTurns + "turn");                       
                UiManager.Instance.UpdateTurn("It is Player2's turn");

                //turn the goals on and off to stop registering collision when it spawns
                player1Goalobj.SetActive(true);
                player2Goalobj.SetActive(false);

                Debug.Log("It is player 2's turn");
                playerDeaths = PlayerDeaths.Player2Death;
                //it is this players turn to score
                playerToScore = PlayerScored.Player2Scored;
                //change the players color to indicate clearer whos turn it is
                CheckColor(player2Color);
               // colorOfPlayer = player2Color;
                Ball.transform.position = spawnpoint2.position;
                break;

        }
    }

    

    public void CheckPoints()
    {
        //check which players turn is it to score
        switch (playerToScore)
        {
            //if a player has scored, award this player a point then give the turn to the next player
            case PlayerScored.Player1Scored:

                player1Points++;
                playerTurns = PlayerTurns.Player2Active;

                break;
            case PlayerScored.Player2Scored:

                player2Points++;
                playerTurns = PlayerTurns.Player1Active;

                break;

        }

    }


}
