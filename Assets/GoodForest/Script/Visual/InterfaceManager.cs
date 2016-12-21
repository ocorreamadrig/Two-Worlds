using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InterfaceManager : MonoBehaviour {

    public Text viewA;
    public Text questAView;

    public Text questBView;
    public Text viewB;

    public Text questCView;
    public Text viewC;

    public SpriteRenderer Shield;
    public SpriteRenderer AA;
    public SpriteRenderer AB;
    public SpriteRenderer AC;
    public GameObject EndPanel;


    // graficos de las estadisticas
    public Image CircleGraphA;
    public Text TextGraphA;
    public Image CircleGraphB;
    public Text TextGraphB;
    public Image CircleGraphC;
    public Text TextGraphC;

    public Slider BarGraphA;
    public Text TextBarGraphA;
    public Slider BarGraphB;
    public Text TextBarGraphB;
    public Slider BarGraphC;
    public Text TextBarGraphC;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	public void ShowStatistics () {

        BarGraphA.value = GameManager.manager.ScoreHelperH3A;
        TextBarGraphA.text = Math.Round(GameManager.manager.ScoreHelperH3A * 100, 1).ToString() + "%";

        BarGraphB.value = GameManager.manager.ScoreHelperH3B;
        TextBarGraphB.text = Math.Round(GameManager.manager.ScoreHelperH3B * 100, 1).ToString() + "%";

        BarGraphC.value = GameManager.manager.ScoreHelperH3C;
        TextBarGraphC.text = Math.Round(GameManager.manager.ScoreHelperH3C * 100, 1).ToString() + "%";

        
        //float total = GameManager.manager.ScoreHelperA + GameManager.manager.ScoreHelperB + GameManager.manager.ScoreHelperC;

        
        float C = GameManager.manager.ScoreHelperC * 100;
        CircleGraphC.fillAmount = C / 100;
        TextGraphC.text = (Math.Round(C, 1)).ToString() + "%";

        float B = GameManager.manager.ScoreHelperB * 100;
        CircleGraphB.fillAmount = B / 100;
        TextGraphB.text = (Math.Round(B, 1)).ToString() + "%";

        float A = GameManager.manager.ScoreHelperA * 100;
        CircleGraphA.fillAmount = A / 100;
        TextGraphA.text = (Math.Round(A, 1)).ToString() + "%";
        

        if (GameManager.manager.HelperSuccess_A.Count != 0)
        {
            for (int i = 0; i < GameManager.manager.HelperSuccess_A.Count; i++)
            {
                if (i < 9) viewA.text += GameManager.manager.HelperSuccess_A[i].ToString() + " - ";
                else
                   viewA.text += GameManager.manager.HelperSuccess_A[i].ToString();
            }

        }
        if (GameManager.manager.HelperSuccess_B.Count != 0)
        {
            for (int i = 0; i < GameManager.manager.HelperSuccess_B.Count; i++)
            {
                if (i < 9) viewB.text += GameManager.manager.HelperSuccess_B[i].ToString() + " - ";
                else
                    viewB.text += GameManager.manager.HelperSuccess_B[i].ToString();
            }


        }
        if (GameManager.manager.HelperSuccess_C.Count != 0)
        {
            for (int i = 0; i < GameManager.manager.HelperSuccess_C.Count; i++)
            {
                if (i < 9) viewC.text += GameManager.manager.HelperSuccess_C[i].ToString() + " - ";
                else
                    viewC.text += GameManager.manager.HelperSuccess_C[i].ToString();
            }


        }
        questAView.text = GameManager.manager.HelperSuccess_A.ToString();
        questBView.text = GameManager.manager.HelperSuccess_B.ToString();
        questCView.text = GameManager.manager.HelperSuccess_C.ToString();
    }

    public void CleanUI()
    {
        CircleGraphC.fillAmount = 0;
        TextGraphC.text = "0%";
        CircleGraphB.fillAmount = 0;
        TextGraphB.text = "0%";
        CircleGraphA.fillAmount = 0;
        TextGraphA.text = "0%";
        BarGraphC.value = 0;
        TextBarGraphC.text = "0";
        BarGraphB.value = 0;
        TextBarGraphB.text = "0";
        BarGraphA.value = 0;
        TextBarGraphA.text = "0";
        viewA.text = "";
        viewB.text = "";
        viewC.text = "";
    }

    public void ChangeAiderIcon(AiderActive icon)
    {
        switch (icon)
        {
            case AiderActive.SlowTime:
                AA.gameObject.SetActive(true);
                break;
            case AiderActive.Psychic:
                AB.gameObject.SetActive(true);
                break;
            case AiderActive.Shield:
                AC.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }
}
