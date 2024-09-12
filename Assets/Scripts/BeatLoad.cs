using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Load the BeatTable and divid it into arrays
public class BeatLoad : MonoBehaviour
{
    public int BPM;
    public float Offset;
    public int KindOfIn;//原材料种类
    public int TotalNote;//节拍总数
    public int[][] IDArray;//原材料步骤id
    public string[] Place = {"sink","board","pan","bowl","bell"};//位置，固定五个
    public string[] Ingredient;
    void Start()
    {
        Debug.Log("  BPM: "+ BeatTable.Instance[0].earliest
                + "  Song Offset: " + BeatTable.Instance[0].place
                + "  Kind of ingredient: " + BeatTable.Instance[0].ingredient
                + "  Total Note " + BeatTable.Instance[0].order);
        BPM = BeatTable.Instance[0].earliest;
        Offset = float.Parse(BeatTable.Instance[0].place);
        KindOfIn = int.Parse(BeatTable.Instance[0].ingredient);
        TotalNote = BeatTable.Instance[0].order;
        IDArray = new int [KindOfIn][];
        
        Ingredient = new string[KindOfIn];
        int num = 0;
        //储存原材料名单，注意在表的开头必须每个材料不重复出现一轮方便记录
        for (int i = 1;i <= TotalNote;i ++){
            if(BeatTable.Instance[i].ingredient != Ingredient[num]){
                Ingredient[num] = BeatTable.Instance[i].ingredient;
                num ++;
            }
            if(num == KindOfIn){
                break;
            }
        }

        for (int i = 1;i <= TotalNote;i ++){
            for(int j = 1;j < KindOfIn;j ++){
                if (BeatTable.Instance[i].ingredient == Ingredient[j]){

                }
            }
        }
    }

    void Update()
    {
    }
}
