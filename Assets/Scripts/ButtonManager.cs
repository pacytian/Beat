using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject Target1;
    public GameObject Target2;
    public AudioSource AS;
    public BeatJudge BJ;
    public StepJudge SJ;
    // Start is called before the first frame update
    void Start()
    {
        //AS = GameObject.Find("MainAudio").GetComponent<AudioSource>();
        //BJ = GameObject.Find("MainAudio").GetComponent<BeatJudge>();
        //SJ = GameObject.Find("PressJudge").GetComponent<StepJudge>();
    }

    public void ButtonStage(int num){
        Target1.GetComponent<MenuContent>().StageNum = num;
    }
    public void ButtonStageStart(){
        PlayerPrefs.SetInt("StageNum", Target1.GetComponent<MenuContent>().StageNum);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main");
    }

    public void ButtonPause(){
        Time.timeScale = 0;
        AS.Pause();
        BJ.Pause();
        Target2.SetActive(true);
        Target1.SetActive(false);
        this.gameObject.SetActive(false);
    }
    public void ButtonContinue(){
        Time.timeScale = 1;
        Invoke("ContinueTheMusic", 3.0f);
        GameObject.Find("MainAudio").GetComponent<CountDown>().enabled = true;
        Target1.SetActive(false);
    }
    void ContinueTheMusic(){
        Target2.SetActive(true);
        AS.UnPause();
        BJ.Pause();
    }
    public void ButtonRestart(){
        Time.timeScale = 1;
        AS.Stop();
        BJ.Restart();
        SJ.Restart();
        //Target2.SetActive(true);
        Target1.SetActive(false);
    }
    public void ButtonBack(){
        Time.timeScale = 1;
        AS.Stop();
        SceneManager.LoadScene("Menu");
    }
    public void ButtonStart(){
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }
    public void ButtonChange(){
        Target2.SetActive(true);
        Target1.SetActive(false);
    }  
}
