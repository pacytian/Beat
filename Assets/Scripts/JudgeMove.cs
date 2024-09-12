using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeMove : MonoBehaviour
{
    BeatJudge  BJ;
    public float offset;
    public int beatnum;
    public float  ShowField1;//显示区间最小值
    public float  ShowField2;//显示区间最大值
    public SpriteRenderer sp;
    void Start()
    {
        BJ= GameObject.Find("MainAudio").GetComponent<BeatJudge >();
        beatnum = 0;
    }

    void Update()
    {
        beatnum = Mathf.FloorToInt(BJ.songPosInBeats) % 4;
        offset = beatnum + BJ.songPosInBeats - Mathf.FloorToInt(BJ.songPosInBeats);
        //transform.rotation = Quaternion.Euler(0,0, - offset * 360 / 4);
        
        
        //offset = BJ.nextIndex - BJ.songPosInBeats;
        transform.rotation = Quaternion.Euler(0,0,-offset * 360);

        if (ShowField1 < ShowField2){
            if(offset>= ShowField1 && offset<= ShowField2){
                sp.enabled = true;
            }else{
                sp.enabled = false;
            }
        }
        else{
            if(offset>= ShowField1 || offset<= ShowField2){
                sp.enabled = true;
            }else{
                sp.enabled = false;
            }
        }
    }

    float Remap(float x, float t1, float t2, float s1, float s2){
        return (s2 - s1) / (t2 - t1) * (x - t1) + s1;
    }
}
