using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public enum Ayudantes {Relentozar, Psyquico }



public class Player : MonoBehaviour {
    
    public Ayudantes FirthState;
    public enum FlappyYAxisTravelState
    {
        GoingUp, GoingDown
    }
    public Text UIScore;
    int score = 0;
    public int lifes;
    public Text UILifes;
    public int scoreBack;
    public Vector3 birdRotation = Vector3.zero;
    public float RotateUpSpeed = 1, RotateDownSpeed = 1;
    public FlappyYAxisTravelState flappyYAxisTravelState;

    public Vector2 jumpForce = new Vector2(0, 300);

    public Vector2 RotValue = new Vector2(0, 300);

    
    public GameObject parclicleColision;
    public AudioClip cointFx;
    private string subject = "Good Forest";
    private string message = "Has llegado a 10 puntos";


    // Use this for initialization
    void Start () {

        
        Time.timeScale = 0;
        //RefreshPowerIcon();
    }

    public void Play()
    {
        Time.timeScale = 1;
        GameManager.manager.beginTime = true;
    }
   
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector2(-14.99F, transform.position.y);
        FixFlappyRotation();
        UILifes.text = "x"+lifes.ToString();
        UIScore.text = score.ToString();
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().AddForce(jumpForce, ForceMode2D.Impulse);
                GetComponent<AudioSource>().Play();
            }

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.y > Screen.height || screenPosition.y < 0)
        {
            lifes--;
           
            Die();
        }

        
        
	}

    private void FixFlappyRotation()
    {
        if (GetComponent<Rigidbody2D>().velocity.y > 0) flappyYAxisTravelState = FlappyYAxisTravelState.GoingUp;
        else flappyYAxisTravelState = FlappyYAxisTravelState.GoingDown;
        float degreesToAdd = 0;
        switch (flappyYAxisTravelState)
        {
            case FlappyYAxisTravelState.GoingUp:
                degreesToAdd = 3 * RotateUpSpeed;
                break;
            case FlappyYAxisTravelState.GoingDown:
                degreesToAdd = -3 * RotateDownSpeed;
                break;
            default:
                break;
        }

        birdRotation = new Vector3(0, 0, Mathf.Clamp(birdRotation.z + degreesToAdd, RotValue.x, RotValue.y));
        transform.eulerAngles = birdRotation;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        lifes--;
        Instantiate(parclicleColision, transform.position, transform.rotation);
        Die(); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<AudioSource>().PlayOneShot(cointFx);
        if (other.CompareTag("Clock")) Destroy(other.gameObject);
        score++;
        if (!GameManager.manager.beginTime && !GameManager.manager.applyAyudante)
        {
            scoreBack++;
        }
    }

    void Die()
    {
        if (lifes == 0)        
        {
            
            GameManager.manager.AddScoreToArray(scoreBack);            
            GameManager.manager.dataManager.Save();            
            GameManager.manager.UI.EndPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    
    public void Quit()
    {
        Application.Quit();
    }

  
   

}
