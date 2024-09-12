using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerMove : MonoBehaviour
{
    BeatJudge BJ;
    public float offset;
    void Start()
    {
        BJ = GameObject.Find("MainAudio").GetComponent<BeatJudge>();
    }

    void Update()
    {
        offset = BJ.nextIndex - BJ.songPosInBeats;
        transform.localPosition = new Vector3(Mathf.Abs(40 * Mathf.Abs(offset - 0.25f) - 20) - 10 , 1 , 0);
    }
}