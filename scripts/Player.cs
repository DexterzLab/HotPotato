using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // bool Player1turn = true;
    // bool Player2turn = false;

   
    AudioSource audioSource;
    public AudioClip collisionSound;
    Rigidbody rb;

    float movementX;
    float movementY;

    public bool playerIsSafe;
    public float speed = 5;
    public Transform spawnpoint;
    public Transform spawnpoint2;


    //GameplayManager gameplayMan;

    // Start is called before the first frame update
    void Start()
    {

       // gameplayMan = GameObject.Find("Gameplay").GetComponent<GameplayManager>();

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //GameplayManager.Instance.CheckPlayer();
        // rb.transform.position = spawnpoint.position;

    }

    // Update is called once per frame
    void Update()
    {
        movementX = Input.GetAxis("Horizontal") * speed;
        movementY = Input.GetAxis("Vertical") * speed;

        

    }

    private void FixedUpdate()
    {
        //rb.AddForce(movementX * Time.deltaTime, 0, movementY * Time.deltaTime, ForceMode.Impulse); //alright at speed 10
        // rb.AddForce(movementX, 0, movementY, ForceMode.Impulse); // good with a speed of 0.5 to 1

        //rb.AddForce(movementX * Time.deltaTime, 0, movementY * Time.deltaTime); //not too good, speed at 200, gives too much control to the player
        rb.AddForce(movementX, 0, movementY, ForceMode.Force);// ok with a speed of 10 to 20
        //rb.AddForce(movementX, 0, movementY);// ok with a speed of 10 to 20



    }

    //if the player hits the wall die and pass the turn to next player
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.CompareTag("Wall") && playerIsSafe == false)
        {
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            GameplayManager.Instance.CheckDeath();
            GameplayManager.Instance.CheckPlayer();

            // GameplayManager.Instance.Invoke("CheckPlayer", 1);
            audioSource.PlayOneShot(collisionSound);
           // Debug.Log("Collision done");
        }
    }

    //in order to stop players using a exploit where if they hold the wall they will still be alive, we will check if the player is still holding the wall
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall") && playerIsSafe == false)
        {
            //rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            GameplayManager.Instance.CheckDeath();
            GameplayManager.Instance.CheckPlayer();

            // GameplayManager.Instance.Invoke("CheckPlayer", 1);
            audioSource.PlayOneShot(collisionSound);
            //Debug.Log("Player has hit the wall in the safe zone");
        }
    }

    //if player reaches goal, pass the turn to next player
    //triggers are used to reduce collision bugs such as the ball flying into the air too fast
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1Goal"))
        {
            Debug.Log("Player2 has scored!");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            GameplayManager.Instance.CheckPoints();
            GameplayManager.Instance.CheckPlayer();
           
        }

        if (other.CompareTag("Player2Goal"))
        {
            Debug.Log("Player1 has scored!");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            GameplayManager.Instance.CheckPoints();
            GameplayManager.Instance.CheckPlayer();
            
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            playerIsSafe = true;
            Debug.Log("Current player is safe");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SafeZone"))
        {
            playerIsSafe = false;
            Debug.Log("Current player has left safe zone");
        }
    }



    /* if (other.gameObject.CompareTag("Wall") && other.gameObject.CompareTag("SafeZone"))
     {
         Debug.Log("Current player is safe");
     }
     if (other.gameObject.CompareTag("Wall") )
     {
         //audioSource.PlayOneShot(collisionSound);

         //rb.constraints = RigidbodyConstraints.FreezeAll;
         rb.velocity = Vector3.zero;
         rb.angularVelocity = Vector3.zero;
         GameplayManager.Instance.CheckDeath();
         GameplayManager.Instance.CheckPlayer();

        // GameplayManager.Instance.Invoke("CheckPlayer", 1);
         audioSource.PlayOneShot(collisionSound);


         /*  if (Player1turn == true && Player2turn == false)
           {
               rb.transform.position = spawnpoint.position;
           }
           else if (Player1turn == false && Player2turn == true)
           {
               rb.transform.position = spawnpoint2.position;
           }


           Player1turn = false;
           Player2turn = true;
          */ //stop here
             //}


}
