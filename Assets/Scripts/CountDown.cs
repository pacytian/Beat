using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text countdownText;
    void OnEnable()
    {
        // 启动协程开始倒计时
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        SetCountdownText("3");
        yield return new WaitForSeconds(1f);
        SetCountdownText("2");
        yield return new WaitForSeconds(1f);
        SetCountdownText("1");
        yield return new WaitForSeconds(1f);
        //Debug.Log("倒计时结束！");
        SetCountdownText("");
        UnactiveSelf();
    }

    void SetCountdownText(string text)
    {
        // 设置 UI Text 显示的文本
        countdownText.text = text;
    }
    void UnactiveSelf(){
        this.GetComponent<CountDown>().enabled = false;
    }
}