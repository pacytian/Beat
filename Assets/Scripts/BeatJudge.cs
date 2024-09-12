using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;


public class BeatJudge : MonoBehaviour
{
    public float bpm;//存放歌曲BeatPerMinute
    public float[] notes;//存放节奏点，单位s
    public int nextIndex = 0;//下一拍是第几拍
    public float Offset;//第一拍开始的时间
    public float songPosition;//当前歌曲播放的秒数
    public float songPosInBeats;//当前歌曲播放到的节拍数，包含小数点后
    bool beat1 = false;//是否过了第一拍
    public bool beat0 = false;//是否开始播放
    public float secPerBeat;
    public float totalTime;
    public float dsptimesong;//记录歌曲开始播放的时间
    //BeatLoad BL;
    public GameObject[] Key = new GameObject[5];//存放按键对应声音的GameObject
    public GameObject PressJudge;//存放有按键相关判定的GameObject
    public GameObject Bar;
    //public JudgeMove JM;
    public KeyJudge KJ;

    //public bool PressRightKey = false;
    public bool PassTheBeat = false;//是否通过判定
    public GameObject[] Judge;//判定的图片 
    public int cool;
    public int fine;
    public bool IsPause;
    float PauseTime = 0;
    public GameObject PauseButton;



    
    void Start()
    {
        //BL = GameObject.Find("BeatManager").GetComponent<BeatLoad>();
        //bpm = 51;
        //Offset = 0.56f;
        IsPause = false;
        beat1 = false;
        beat0 = false;
        //PressRightKey = false;
        PassTheBeat = false;
        songPosition = 0;
        songPosInBeats = 0;
        cool = 0;
        fine = 0;
        totalTime = GetComponent<AudioSource>().clip.length;
        GameObject.Find("MainAudio").GetComponent<CountDown>().enabled = true;
        Invoke("PlayTheMusic", 3.0f);
    }

    void PlayTheMusic(){
        dsptimesong = (float) AudioSettings.dspTime;
        GetComponent<AudioSource>().Play();
        beat0 = true;
        PauseButton.SetActive(true);
    }
    public void Restart(){
        beat1 = false;
        beat0 = false;
        PassTheBeat = false;
        songPosition = 0;
        songPosInBeats = 0;
        cool = 0;
        fine = 0;
        IsPause = false;
        Bar.transform.localPosition = new Vector3(- 6.8f, Bar.transform.localPosition.y,Bar.transform.localPosition.z);
        GameObject.Find("MainAudio").GetComponent<CountDown>().enabled = true;
        GameObject.Find("PressJudge").GetComponent<DishJudge>().PresentIngredients = new List<string>();
        Invoke("PlayTheMusic", 3.0f);
    }
    public void Pause(){
        IsPause = !IsPause;
        if(IsPause){
            PauseTime = (float)AudioSettings.dspTime;
        }
        else{
            dsptimesong += (float)AudioSettings.dspTime - PauseTime;
        }
    }
    
    void Update()
    {
        if (beat0){
           if(!IsPause){
            songPosition = (float) (AudioSettings.dspTime - dsptimesong);
            Bar.transform.localPosition = new Vector3(- 6.8f + 6.8f * (songPosition/totalTime), Bar.transform.localPosition.y,Bar.transform.localPosition.z);
           }
        }
        if (songPosition - Offset >= 0){
            songPosInBeats = (songPosition - Offset) / secPerBeat;
            if (!beat1){
                beat1 = true;
            };
        }
        else{
            songPosInBeats = 0;
        }
        
        if(nextIndex <= songPosInBeats && beat1){
           nextIndex ++;
           KJ.IsPress = false;
           KJ.IsOtherKey = false;//更新拍子的时重置ispress 
        }
        
        PressJudge.GetComponent<StepJudge>().PassIngredientKey((int)Round(songPosInBeats) % 4);
        
        if( Round(songPosInBeats) % 4 == 0){
            PressJudge.GetComponent<KeyJudge>().key1 = KeyCode.Q;
            PressJudge.GetComponent<KeyJudge>().keyvalue = 0;
        }
        else if( Round(songPosInBeats) % 4 == 1){
            PressJudge.GetComponent<KeyJudge>().key1 = KeyCode.W;
            PressJudge.GetComponent<KeyJudge>().keyvalue = 1;
        }
        else if( Round(songPosInBeats) % 4 == 2){
            PressJudge.GetComponent<KeyJudge>().key1 = KeyCode.A;
            PressJudge.GetComponent<KeyJudge>().keyvalue = 2;
        }
        else if( Round(songPosInBeats) % 4 == 3){
            PressJudge.GetComponent<KeyJudge>().key1 = KeyCode.S;
            PressJudge.GetComponent<KeyJudge>().keyvalue = 3;
        }
        
        //JM.beatnum = (int)Round(songPosInBeats) % 4;


        /* if (PressRightKey){
            if ((nextIndex - songPosInBeats <= 0.05) || (nextIndex - songPosInBeats >= 0.95)){
                Debug.Log("Perfect"+ (nextIndex - songPosInBeats));
            }
            else if ((nextIndex - songPosInBeats > 0.4) && (nextIndex - songPosInBeats < 0.6))
            {
                Debug.Log("Fail"+ (nextIndex - songPosInBeats));
            }
            else{
                Debug.Log("Good"+ (nextIndex - songPosInBeats));
            }
            PressRightKey = false;
        } */
             

        /*if(nextIndex <= songPosInBeats && beat1){
            for (int i = 1;i <= BL.TotalNote;i ++){
                if(BeatTable.Instance[i].earliest == nextIndex){
                  switch(BeatTable.Instance[i].place){
                    case "sink":
                        Key[0].GetComponent<AudioSource>().Play();
                    break;
                    case "board":
                        Key[1].GetComponent<AudioSource>().Play();
                    break;
                    case "pan":
                        Key[2].GetComponent<AudioSource>().Play();
                    break;
                    case "bowl":
                        Key[3].GetComponent<AudioSource>().Play();
                    break;
                    case "bell":
                        Key[4].GetComponent<AudioSource>().Play();
                    break;
                  } 
                }
            }
            //Key[2].GetComponent<AudioSource>().Play();
            nextIndex ++;
        }*/
         if(Input.GetKeyDown(KeyCode.Q)){
            Key[0].GetComponent<AudioSource>().Play();
        }
        if(Input.GetKeyDown(KeyCode.W)){
            Key[1].GetComponent<AudioSource>().Play();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            Key[2].GetComponent<AudioSource>().Play();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            Key[3].GetComponent<AudioSource>().Play();
        } 
    }

    public void BeatJudgeResult(){
            if ((nextIndex - songPosInBeats <= 0.05) || (nextIndex - songPosInBeats >= 0.95)){
                Debug.Log("Perfect"+ (nextIndex - songPosInBeats));
                Judge[0].SetActive(true);
                cool ++;
                PassTheBeat = true;
            }
            else if ((nextIndex - songPosInBeats > 0.4) && (nextIndex - songPosInBeats < 0.6))
            {
                Debug.Log("Fail"+ (nextIndex - songPosInBeats));
                PassTheBeat = false;
            }
            else{
                Debug.Log("Good"+ (nextIndex - songPosInBeats));
                Judge[1].SetActive(true);
                fine ++;
                PassTheBeat = true;
            }
            //PressRightKey = false;
        }

    float Round(float value, int digits = 0){
            if (value == 0)
            {
                return 0;
            }
            float multiple = Mathf.Pow(10, digits);
            float tempValue = value > 0 ? value * multiple + 0.5f : value * multiple - 0.5f;
            tempValue = Mathf.FloorToInt(tempValue);
            return tempValue / multiple;
    }
}
