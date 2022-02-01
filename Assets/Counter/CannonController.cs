using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CannonController : MonoBehaviour
{
    private float speed = 20;
    private float turretSpeed = 30;
    [SerializeField] private GameObject turret;
    [SerializeField] private GameObject ball;
    [SerializeField] private Text timerText;
    private GameObject introText;
   

    private Image powerBar;
    private float cannonForce = 15000;
    private float cannonPower = 0;

    private int timeRemaining = 60;
    private bool isGameOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        powerBar = GameObject.Find("Power Bar").GetComponent<Image>();

        introText=GameObject.Find("Intro Text");

        UpdatePowerBar();
        InvokeRepeating("Countdown",1,1);
        Invoke("ClearTitle", 4);
        Invoke("ClearInstructions", 8);
    }

    // Update is called once per frame
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

    private void UpdatePowerBar()
    {
        powerBar.transform.localScale = new Vector3(cannonPower/100, 1, 1);
    }

    private void FireCannon()
    {
        GameObject cannonBall = Instantiate(ball,(turret.transform.TransformPoint(Vector3.forward*6)),turret.transform.rotation);
        Rigidbody rb=cannonBall.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * cannonForce * cannonPower);
    }

    public void NextLevel(int level)
    {
        int addTime = (int)Mathf.Ceil((50.0f / level));
        if (addTime < 20) addTime = 20;
        timeRemaining += addTime;

        float mX = -level * 8;
        if (mX < -58) mX = -58;

        GameObject.Find("Box").GetComponent<MoveBox>().SetXRange(mX,mX+10);

    }
    private void Countdown()
    {
        timerText.text = "Time: "+timeRemaining;
        
        if (timeRemaining == 0)
        {
            GameOver();
        }
        else
        {
            timeRemaining = timeRemaining - 1;
        }
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        isGameOver = true;
        Text goText = introText.GetComponent<Text>();
        goText.text = "GAME OVER";
        goText.fontSize = 30;
        introText.SetActive(true);
    }

    private void ClearInstructions()
    {
        introText.SetActive(false);

    }

    private void ClearTitle()
    {
        GameObject.Find("Title Text").SetActive(false);
    }
}
