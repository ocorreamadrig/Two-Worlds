using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum AiderActive { SlowTime, Psychic, Shield, NONE}

public class GameManager : MonoBehaviour {

    public static GameManager manager;
    public AiderActive actualAider;

    public List<string> aiderArray = new List<string>();
   
    public bool applyAyudante;
    public bool beginTime;

    // Variables del ayudante A
    public bool activeAider_A;
    public List<float> HelperSuccess_A = new List<float>();
    public float ScoreHelperA = 0;
    public float ScoreHelperH2A = 0;
    public float ScoreHelperH3A = 0;
    public float CountSuccess_A = 0;
    public float CountSuccess_A_H3 = 0;
    public float CountCeroSuccess_A = 0;

    // Variables del ayudante B
    public bool activeAider_B;
    public List<float> HelperSuccess_B = new List<float>();
    public float ScoreHelperB = 0;
    public float ScoreHelperH2B = 0;
    public float ScoreHelperH3B = 0;
    public float CountSuccess_B = 0;
    public float CountSuccess_B_H3 = 0;
    public float CountCeroSuccess_B = 0;

    // Variables del ayudante C
    public bool activeAider_C;
    public List<float> HelperSuccess_C = new List<float>();
    public float ScoreHelperC = 0;
    public float ScoreHelperH2C = 0;
    public float ScoreHelperH3C = 0;
    public float CountSuccess_C = 0;
    public float CountSuccess_C_H3 = 0;
    public float CountCeroSuccess_C = 0;

    public InterfaceManager UI;
    public DataManager dataManager;
    // Accesorios de los Ayudantes
    public SpriteRenderer Shield;

    public GameObject Player;

    // Use this for initialization
    void Start ()
    {
        if(manager == null) manager = this;
        //UI = GetComponent<InterfaceManager>();

        if (!File.Exists(Application.persistentDataPath + "/Profile.dg"))
        {
            HelperSuccess_A = new List<float>();
            HelperSuccess_B = new List<float>();
            HelperSuccess_C = new List<float>();
        }
        else dataManager.Load();

                 

        // Hacer los calculos de la heuristica hasta el momento actual
        currentSuccess();
        
    }

    public void AddScoreToArray(int currentScore) {
        switch (actualAider)
        {
            case AiderActive.SlowTime:
                HelperSuccess_A.Add(currentScore);
                Debug.Log("Score Added in A");
                break;
            case AiderActive.Psychic:
                HelperSuccess_B.Add(currentScore);
                Debug.Log("Score Added in B");
                break;
            case AiderActive.Shield:
                HelperSuccess_C.Add(currentScore);
                Debug.Log("Score Added in C");
                break;
            default:
                break;
        }

    }
	
	public void CleanAll () {
        
        
        HelperSuccess_A.Clear();
        HelperSuccess_B.Clear();
        HelperSuccess_C.Clear();
        UI.CleanUI();
        dataManager.Save();

        actualAider = AiderActive.SlowTime;
 
        
    }

    public void UpdateAssets()
    {
        // Activar el ayudante correspondiente a la situación
        if (HelperSuccess_A.Count < 10)
        {
            actualAider = AiderActive.SlowTime;
            UI.ChangeAiderIcon(AiderActive.SlowTime);
            StartCoroutine(AyudanteAUpdate());
        }
        else if (HelperSuccess_B.Count < 10)
        {
            actualAider = AiderActive.Psychic;
            UI.ChangeAiderIcon(AiderActive.Psychic);
            StartCoroutine(AyudanteBUpdate());

        }
        else if (HelperSuccess_C.Count < 10)
        {
            actualAider = AiderActive.Shield;
            UI.ChangeAiderIcon(AiderActive.Shield);
            StartCoroutine(AyudanteCUpdate());

        } 
        else if(HelperSuccess_C.Count==10) actualAider = AiderActive.NONE;

    }
    public void currentSuccess()
    {
        int   CountIterHelper = 30;
        int   SuccessMean     = 3;
        int   CountHelpers    = 3;
        float RTFactor        = 1.9F; 
        int CountAiderPass  = 10;
       
       
        //Cálculo de la cantidad de aciertos y fallos del ayudante A
       if (HelperSuccess_A.Count > 0)
        {
            Debug.Log("The Aider A is -->Active<---");
            

            for (int i = 0; i < HelperSuccess_A.Count; i++)
            {
                if (HelperSuccess_A[i] >= SuccessMean) CountSuccess_A++;
                if (HelperSuccess_A[i] == 0) CountCeroSuccess_A++;
            }

            ScoreHelperA   = CountSuccess_A / (CountHelpers * CountIterHelper);
            ScoreHelperH2A = (CountSuccess_A + CountCeroSuccess_A) / (CountHelpers * CountIterHelper);
            ScoreHelperH3A = ((CountSuccess_A * RTFactor) + CountCeroSuccess_A) / (CountHelpers * CountIterHelper);
        }

        //Cálculo de la cantidad de aciertos y fallos del ayudante B
        if (HelperSuccess_B.Count > 0)
        {
            Debug.Log("The Aider B is -->Active<---");

            for (int i = 0; i < HelperSuccess_B.Count; i++)
            {
                if (HelperSuccess_B[i] >= SuccessMean) CountSuccess_B++;
                if (HelperSuccess_B[i] == 0) CountCeroSuccess_B++;    
            }

            ScoreHelperB   = CountSuccess_B / (CountHelpers * CountIterHelper);
            ScoreHelperH2B = (CountSuccess_B + CountCeroSuccess_B) / (CountHelpers * CountIterHelper);
            ScoreHelperH3B = ((CountSuccess_B * RTFactor) + CountCeroSuccess_B) / (CountHelpers * CountIterHelper);
        }

        //Cálculo de la cantidad de aciertos y fallos del ayudante C 
        if (HelperSuccess_C.Count > 0)
        {
            Debug.Log("The Aider B is -->Active<---");

            for (int i = 0; i < HelperSuccess_C.Count; i++)
            {
                if (HelperSuccess_C[i] >= SuccessMean) CountSuccess_C++;
                if (HelperSuccess_C[i] == 0) CountCeroSuccess_C++;
            }

            ScoreHelperC  =  CountSuccess_C / (CountHelpers * CountIterHelper);
            ScoreHelperH2C = (CountSuccess_C + CountCeroSuccess_C) / (CountHelpers * CountIterHelper);
            ScoreHelperH3C = ((CountSuccess_C * RTFactor) + CountCeroSuccess_C) / (CountHelpers * CountIterHelper);
        }
        
        Debug.Log("Dime su entro");
        UI.ShowStatistics();

        //UpdateAssets();
    }

    IEnumerator AyudanteAUpdate()
    {

        yield return new WaitForSeconds(5);
        beginTime = false;
        activeAider_A = true;
        applyAyudante = true;
        Time.timeScale = 0.5F;
        yield return new WaitForSeconds(5);
        applyAyudante = false;
        activeAider_A = false;
        Time.timeScale = 1;
    }
    IEnumerator AyudanteBUpdate()
    {

        yield return new WaitForSeconds(5);
        beginTime = false;
        activeAider_B = true;
        applyAyudante = true;
        yield return new WaitForSeconds(5);
        applyAyudante = false;
        activeAider_B = false;
    }
    IEnumerator AyudanteCUpdate()
    {

        yield return new WaitForSeconds(5);
        Shield.gameObject.SetActive(true);
        Player.GetComponent<BoxCollider2D>().enabled = false;
        beginTime = false;
        activeAider_C = true;
        applyAyudante = true;
        yield return new WaitForSeconds(5);
        Shield.gameObject.SetActive(false);
        applyAyudante = false;
        activeAider_C = false;
        Player.GetComponent<BoxCollider2D>().enabled = true;
    }
}
