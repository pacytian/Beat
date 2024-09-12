using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class MenuContent : MonoBehaviour
{
    public GameObject[] Images;
    public Text[] Texts;
    public GameObject[] Buttons;
    public string[] Strings;
    public int StageNum;
    // Start is called before the first frame update
    void Start()
    {
        StageNum = 0;
        PlayerPrefs.SetInt("Stage0", 1);
        PlayerPrefs.SetInt("Stage"+ (StageNum + 1 ).ToString(), 1);
        PlayerPrefs.Save();
    }

    void OnEnable(){
        for (int i= 0; i < Buttons.Length ; i ++){
            Buttons[i].GetComponent<Button>().interactable = false;
            if (PlayerPrefs.GetInt("Stage" + i.ToString()) != 0){
                Buttons[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Images[0].GetComponent<ShowImage>().ShowSprite(StageNum * 3);
        Images[1].GetComponent<ShowImage>().ShowSprite(StageNum * 3 + 1);
        Images[2].GetComponent<ShowImage>().ShowSprite(StageNum * 3 + 2);
        Texts[0].text = Strings[StageNum * 3];
        Texts[1].text = Strings[StageNum * 3 + 1];
        Texts[2].text = Strings[StageNum * 3 + 2];
        Texts[3].text = PlayerPrefs.GetInt("Cool"+ StageNum.ToString(),0).ToString();
        Texts[4].text = PlayerPrefs.GetInt("Fine"+ StageNum.ToString(),0).ToString();
    }
}
