using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStage
{
    Ready, Playing, Pause, Over
}
public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameStage gameStage;

    public Transform playerStartPos, Pool;
    public GameObject playerObj, clouds, truyen;
    public Slider hpBar, alcoholBar, soapBar, maskBar;
    public Text countFood, timeTxt, timeO, txtVirusCount;

    [HideInInspector]
    public int score, foodCount, virusCount;
    public int[] itemCount = new int[3];
    public float startHungerPoint, hungerPoint, healthValue, time;
    public bool grandmaGotTouched;

    public float[] listHightTime = new float[10];

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hungerPoint = startHungerPoint;
        Destroy(truyen, 3);

        for (int i = 0; i < 10; i++)
        {
            listHightTime[i] = PlayerPrefs.GetFloat(string.Format("highTime{0}", i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (gameStage)
        {
            case GameStage.Ready:
                ReadyStat();
                break;
            case GameStage.Playing:
                PlayingStat();
                break;
            case GameStage.Pause:

                break;
            case GameStage.Over:
                ReadyStat();
                break;
        }
    }


    void ReadyStat()
    {
        
        playerObj.transform.position = playerStartPos.position;
        grandmaGotTouched = false;
        score = 0; itemCount[0] = 0; itemCount[0] = 0; itemCount[0] = 0; time = 0; virusCount = 0;
        foodCount = 0;
        hungerPoint = startHungerPoint;
        foreach (Transform child in Pool)
        {
            Destroy(child.gameObject);
        }
    }
    int min, sec;
    void PlayingStat()
    {
        if (grandmaGotTouched || hungerPoint <= 0)
        {
            gameStage = GameStage.Over;
            OverStat();
        }



        hungerPoint -= Time.deltaTime;
        hpBar.value = hungerPoint;
        alcoholBar.value = itemCount[0]; if (itemCount[0] > 3) { itemCount[0] = 3; };
        soapBar.value = itemCount[1]; if (itemCount[1] > 3) { itemCount[1] = 3; };
        maskBar.value = itemCount[2]; if (itemCount[2] > 3) { itemCount[2] = 3; };
        countFood.text = foodCount.ToString();
        time += Time.deltaTime;
        min = (int)time / 60; sec = (int)time % 60;
        timeTxt.text = min + " : " + sec;
        txtVirusCount.text = virusCount.ToString();

    }

    void OverStat()
    {
        UIButtonController.instance.overMenu.SetActive(true);
        UIButtonController.instance.HUD.SetActive(false);
        foreach (Transform child in Pool)
        {
            Destroy(child.gameObject);
        }
        grandmaGotTouched = false;
        timeO.text = min + " : " + sec;
        txtVirusCount.text = virusCount.ToString();

        for (int i = 0; i < 10; i++)
        {
            listHightTime[i] = PlayerPrefs.GetFloat(string.Format("highTime{0}", i));
        }

        for (int i = 9; i >= 0; i--)
        {
            if (time > listHightTime[i])
            {
                listHightTime[i] = time;
                PlayerPrefs.SetFloat(string.Format("highTime{0}", i), time);
            }
        }
    }
}
