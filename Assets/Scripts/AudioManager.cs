using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public int StageNum;
    public AudioClip[] BGM;
    public float[] BPM;
    public BeatJudge BJ;
    public AudioSource AS;
    public StepJudge SJ;
    // Start is called before the first frame update
    void Start()
    {
        StageNum = PlayerPrefs.GetInt("StageNum") + 1;
        AS.clip = BGM[StageNum];
        BJ.bpm = BPM[StageNum];
        BJ.secPerBeat = 60f / BPM[StageNum];
        if (StageNum == 0){
            BJ.Offset = 0.56f;
        }
        else{
            BJ.Offset = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!AS.isPlaying && BJ.beat0 && !BJ.IsPause){
            if(SJ.quecount[4] == SJ.Ingredient.Length){
                Debug.Log("Congratulations! Finish All Dishes!");
            }
            else{
                Debug.Log("Keep it up! You finish " + SJ.FinishDishes.Count + "dishes.");
            }
            if (SJ.FinishDishes.Count > 0){
                PlayerPrefs.SetInt("Stage" + StageNum.ToString(), 1);
                for (int i = 0;i < SJ.FinishDishes.Count; i++){
                    PlayerPrefs.SetInt("Dishes" + ((StageNum - 1) * 3 + SJ.FinishDishes[i]).ToString(),1);
                }
            }
            if (PlayerPrefs.GetInt("Cool" + (StageNum - 1).ToString()) < BJ.cool)
            {
                PlayerPrefs.SetInt("Cool" + (StageNum - 1).ToString() ,BJ.cool);
                PlayerPrefs.SetInt("Fine" + (StageNum - 1).ToString() ,BJ.fine);
            }
            PlayerPrefs.Save();
            Time.timeScale = 1;
            //AS.Stop();
            SceneManager.LoadScene("Menu");
        }
    }
}
