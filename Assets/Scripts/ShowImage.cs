using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowImage : MonoBehaviour
{
    public Sprite[] spritesToDisplay; // 不同的Sprite数组
    private Image imageComponent;
    public int num;
    string PP;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        ShowSprite(num);
    }

    public void ShowSprite(int index)
    {
        if (index >= 0 && index < spritesToDisplay.Length)
        {
            imageComponent.sprite = spritesToDisplay[index];
            Vector2 imageSize = imageComponent.sprite.rect.size;
            RectTransform rectTransform = imageComponent.rectTransform;
            rectTransform.sizeDelta = imageSize;
            PP = "Dishes" + index.ToString();
            if (PlayerPrefs.GetInt(PP,0) == 0){
                imageComponent.color = new Color(0,0,0,0.5f);
            }else{
                imageComponent.color = new Color(1,1,1,1);
            }
        }
    }
}
