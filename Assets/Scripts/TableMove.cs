using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableMove: MonoBehaviour
{
    BeatJudge BJ;
    public float offset4;
    void Start()
    {
        BJ = GameObject.Find("MainAudio").GetComponent<BeatJudge>();
    }

    void Update()
    {
        offset4 = 4 - (BJ.nextIndex % 4) + BJ.nextIndex - BJ.songPosInBeats;
        transform.rotation = Quaternion.Euler(0,offset4 * 90 + 90,0);
    }

    float Remap(float x, float t1, float t2, float s1, float s2){
        return (s2 - s1) / (t2 - t1) * (x - t1) + s1;
    }
}
