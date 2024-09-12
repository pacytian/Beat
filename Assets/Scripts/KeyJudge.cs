using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyJudge : MonoBehaviour
{
    public KeyCode key1;//接收来自beatjudge的值
    public KeyCode key2;//接收来自stepjudge的值
    int keyframe1 = 0;
    bool onepress = false;
    int keyframe2 = 0;
    bool twopress = false;
    public int keyvalue;
    public GameObject Step;

    public bool IsPress;//用来存放这一拍是否已经被按下过

    public bool IsOtherKey;

    public BeatJudge BJ;
    void Start()
    {
        IsPress = false;
        IsOtherKey = false;
    }
    
    void Update()
    {
        if (Input.anyKeyDown){
            if(Input.GetKeyDown(key1) || Input.GetKeyDown(key2)){
                //Debug.Log("  a  "); 
            }
            else{
                IsOtherKey = true;
            }
        }

        if (onepress){
            keyframe1++;
        }
        if(Input.GetKeyDown(key1) || Input.GetKeyDown(key2)){
            onepress = true;
        }
        else if(Input.GetKeyUp(key1) || Input.GetKeyUp(key2)){
            onepress = false;
            keyframe1 = 0;
        }
        

        if( (Input.GetKey(key1) && Input.GetKeyDown(key2)) ||
            (Input.GetKey(key2) && Input.GetKeyDown(key1)) ){
                if(keyframe1 < 10 && keyframe1 > 0){
                    //Debug.Log("Press two key in " + keyframe1);
                    twopress = true;
                    if(!IsOtherKey){
                        BJ.BeatJudgeResult();
                        if (!IsPress){
                            IsPress = true;
                            if (BJ.PassTheBeat){
                                Step.GetComponent<StepJudge>().key = keyvalue;
                            }
                        }
                    }
                }
        }

        if (Input.GetKeyDown(key1) && Input.GetKeyDown(key2)){
            //Debug.Log("Press two key in 0");
            twopress = true;
            if(!IsOtherKey){
                BJ.BeatJudgeResult();
                if (!IsPress){
                    IsPress = true;
                    if (BJ.PassTheBeat){
                        Step.GetComponent<StepJudge>().key = keyvalue;
                    }
                }
            }
        }
        
        /*else if(Input.GetKeyDown(key1)){
            Debug.Log("Press key1");
        }
        else if(Input.GetKeyDown(key2)){
            Debug.Log("Press key2");
        }*/
        if (Input.GetKey(key1) && Input.GetKey(key2)){
            keyframe2++;
        }
        if(twopress && (Input.GetKeyUp(key1) || Input.GetKeyUp(key2))){
            if(keyframe2 > 10){
            //Debug.Log("Two keys down for"+ keyframe2);
            }
            keyframe2 = 0;
            twopress = false;
        }
    }
}
