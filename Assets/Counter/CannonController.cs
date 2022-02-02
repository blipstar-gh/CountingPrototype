using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//
// Controls the cannon rotation and firing angle
// Launches cannon balls
// Handles the timer (which counts down to 0)
//


public class CannonController : MonoBehaviour
{
    private int timeRemaining = 30;         // time left
    private float speed = 20;               // speed to cannon rotation
    private float turretSpeed = 40;         // speed of turret raising/lifting
    private float cannonForce = 15000;      // "Average" cannon ball speed
    private float cannonPower = 0;          // "Power" bar value (0 to 100)
    private bool isGameOver = false;        // Is game over?

    // Reference various game objects we need
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject ball;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject restartButton;
    private GameObject introText;
    private Image powerBar;
        
  
    

    // Initialise cannon
    // Load high score, display the high score
    // Set up the cannon power bar
    // Start the timer
    void Start()
    {

        MainManager.Instance.LoadScore();
        GameObject.Find("Highscore Text").GetComponent<Text>().text = "High score: " + MainManager.Instance.highScore + " (" + MainManager.Instance.highScoreName + ")";

        MainManager.Instance.UpdateScore();
        MainManager.Instance.isHighScore = false;

        powerBar = GameObject.Find("Power Bar").GetComponent<Image>();

        introText=GameObject.Find("Intro Text");

        UpdatePowerBar();
        InvokeRepeating("Countdown",1,1);

        restartButton.SetActive(false);

        
    }

    // Check for key inputs
    // Alter the cannon rotation and turrent angle
    // Fire ball
    void Update()
    {
        if (isGameOver) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cannonPower = 6;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if(cannonPower<100)cannonPower++;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            cannonPower = cannonPower / 40;
            FireCannon();
            cannonPower = 0;
            UpdatePowerBar();
        }

        if (cannonPower > 0) UpdatePowerBar();


        float horizInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");
        float turretAngle = turret.transform.forward.y;
        if ((vertInput > 0 && turretAngle < 0.9) || (vertInput<0 && turretAngle>0))
        { 
        
        turret.transform.Rotate(new Vector3(-vertInput * Time.deltaTime * turretSpeed, 0, 0));
         }

        transform.Rotate(new Vector3(0, horizInput* speed * Time.deltaTime, 0));
  
    }

    // Update the cannon power bar
    private void UpdatePowerBar()
    {
        powerBar.transform.localScale = new Vector3(cannonPower/100, 1, 1);
    }

    // Fire cannon ball
    private void FireCannon()
    {
        GameObject cannonBall = Instantiate(ball,(turret.transform.TransformPoint(Vector3.forward*6)),turret.transform.rotation);
        Rigidbody rb=cannonBall.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * cannonForce * cannonPower);
    }

    // When the score goes up, add some extra time and set the box distance range
    public void NextLevel(int level)
    {
        int addTime = (int)Mathf.Ceil((40.0f / level));
        if (addTime < 20) addTime = 20;
        timeRemaining += addTime;

        float mX = -level * 8;
        if (mX < -58) mX = -58;

        GameObject.Find("Box").GetComponent<MoveBox>().SetXRange(mX,mX+10);

    }

    // Countdown the timer
    // Check for game over
    private void Countdown()
    {
        timerText.text = "Time: "+timeRemaining;

        if (timeRemaining == 10) timerText.color = new Color(1, 0, 0);
        
        if (timeRemaining == 0)
        {
            GameOver();
        }
        else
        {
            timeRemaining = timeRemaining - 1;
        }
    }

    // Game over
    // Display a message and turn on the restart button
    private void GameOver()
    {
        isGameOver = true;
        Text goText = introText.GetComponent<Text>();
        string gT= "Game Over";

        goText.text = gT;
        goText.fontSize = 25;
        
        restartButton.SetActive(true);
     
    }

   
}
