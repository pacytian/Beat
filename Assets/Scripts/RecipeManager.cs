using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public int StageNum;
    public StepJudge SJ;
    public DishJudge DJ;
    public GameObject[] Di = new GameObject[3];
    public GameObject[] In = new GameObject[9];
    GameObject pref;
    int n = 0;
    void Start()
    {
        StageNum = PlayerPrefs.GetInt("StageNum") + 1;
        switch (StageNum){
        case 0:
        {
            Object chicken = Resources.Load<GameObject>("chicken");
            Object pepper = Resources.Load<GameObject>("pepper");
            Object potato = Resources.Load<GameObject>("potato");
            Object lzj = Resources.Load<GameObject>("lzj");
            Object sltds = Resources.Load<GameObject>("sltds");
            Object keyj = Resources.Load<GameObject>("J");
            Object keyk = Resources.Load<GameObject>("K");
            Object keyl = Resources.Load<GameObject>("L");
            
            SJ.Ingredient = new string[]{"chicken","potato","pepper"};
            SJ.KeyIn = new KeyCode[]{KeyCode.J, KeyCode.L, KeyCode.K};
            SJ.Recipe = new List<string> {"saute diced chicken with hot peppers","potato shreds with vinegar"};
            SJ.KindOfIn = 3;
            SJ.Food = new Object[] {chicken,potato,pepper};
            SJ.Dish = new Object[] {lzj,sltds};
            Object[] Ingredient = new Object[]{chicken,pepper,null,potato,null,null,null,null,null};
            SJ.Keys = new Object[] {keyj, keyl, keyk};
            SJ.IDArray  = new int [][]{
                new int[]{0,1,2,3,2,3,4},
                new int[]{0,1,2,3,2,0,3,4}, 
                new int[]{1,2,3,4},
            };
            
            DJ.Recipes.Add("saute diced chicken with hot peppers", new List<string> { "chicken", "pepper" });
            DJ.Recipes.Add("potato shreds with vinegar", new List<string> {"potato"});
            
            for (int i = 0; i < SJ.Dish.Length;i++){
                pref = Instantiate(SJ.Dish[i]) as GameObject;
                pref.transform.parent = Di[i].transform;
                pref.transform.localPosition = new Vector3(0,0.45f,-0.1f);
                pref.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
                SJ.DiIconSp[i] = pref.GetComponent<SpriteRenderer>();
            }
            
            for (int i = 0; i < Ingredient.Length;i++){
                if (Ingredient[i] != null){
                    pref = Instantiate(Ingredient[i]) as GameObject;
                    pref.transform.parent = In[i].transform;
                    pref.transform.localPosition = new Vector3(0,0,-0.1f);
                    pref.transform.localScale = new Vector3(0.65f,0.65f,1);
                    pref.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
                    SJ.InIconSp[n] = pref.GetComponent<SpriteRenderer>();
                    n++;
                }
                else{
                    In[i].gameObject.SetActive(false);
                }
            }
        }
        break;
        case 1:
        {
            Object NapaCabbage = Resources.Load<GameObject>("NapaCabbage");
            Object Tomato = Resources.Load<GameObject>("Tomato");
            Object Egg = Resources.Load<GameObject>("Egg");
            Object SourCabbage = Resources.Load<GameObject>("SourCabbage");
            Object Pork = Resources.Load<GameObject>("Pork");
            Object qtbc = Resources.Load<GameObject>("qtbc");
            Object fqcd = Resources.Load<GameObject>("fqcd");
            Object scbr = Resources.Load<GameObject>("scbr");
            Object keyu = Resources.Load<GameObject>("U");
            Object keyh = Resources.Load<GameObject>("H");
            Object keyj = Resources.Load<GameObject>("J");    
            Object keyb = Resources.Load<GameObject>("B");
            Object keyn = Resources.Load<GameObject>("N");
            
            SJ.Ingredient = new string[]{"NapaCabbage","Tomato","Egg","SourCabbage","Pork"};
            SJ.KeyIn = new KeyCode[]{KeyCode.U, KeyCode.H, KeyCode.J, KeyCode.B, KeyCode.N};
            SJ.Recipe = new List<string> {"Napa Cabbage in Clear Broth","Tomato and Egg Stir-fry","Pork Stewed with Sour Cabbage"};
            SJ.KindOfIn = 5;
            SJ.Food = new Object[] {NapaCabbage,Tomato,Egg,SourCabbage,Pork};
            SJ.Dish = new Object[] {qtbc,fqcd,scbr};
            Object[] Ingredient = new Object[]{NapaCabbage,null,null,Tomato,Egg,null,SourCabbage,Pork,null};
            SJ.Keys = new Object[] {keyu, keyh, keyj, keyb, keyn};
            SJ.IDArray  = new int [][]{
                new int[]{0,1,2,0,2,3,4},
                new int[]{0,3,1,2,3,4}, 
                new int[]{2,3,2,3,4},
                new int[]{1,2,3,4},
                new int[]{0,3,2,1,2,3,4},
            };
            
            DJ.Recipes.Add("Napa Cabbage in Clear Broth", new List<string> {"NapaCabbage"});
            DJ.Recipes.Add("Tomato and Egg Stir-fry", new List<string> {"Tomato","Egg"});
            DJ.Recipes.Add("Pork Stewed with Sour Cabbage", new List<string> {"SourCabbage","Pork"});
            
            for (int i = 0; i < SJ.Dish.Length;i++){
                pref = Instantiate(SJ.Dish[i]) as GameObject;
                pref.transform.parent = Di[i].transform;
                pref.transform.localPosition = new Vector3(0,0.45f,-0.1f);
                pref.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
                SJ.DiIconSp[i] = pref.GetComponent<SpriteRenderer>();
            }
            
            for (int i = 0; i < Ingredient.Length;i++){
                if (Ingredient[i] != null){
                    pref = Instantiate(Ingredient[i]) as GameObject;
                    pref.transform.parent = In[i].transform;
                    pref.transform.localPosition = new Vector3(0,0,-0.1f);
                    pref.transform.localScale = new Vector3(0.65f,0.65f,1);
                    pref.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
                    SJ.InIconSp[n] = pref.GetComponent<SpriteRenderer>();
                    n++;
                }
                else{
                    In[i].gameObject.SetActive(false);
                }
            }
        }
        break;
        case 2:
        {
            Object ChineseToon = Resources.Load<GameObject>("ChineseToon");
            Object Tofu = Resources.Load<GameObject>("Tofu");
            Object BitterMelon = Resources.Load<GameObject>("BitterMelon");
            Object Egg = Resources.Load<GameObject>("Egg");
            Object Mushroom = Resources.Load<GameObject>("Mushroom");
            Object Pork = Resources.Load<GameObject>("Pork");
            Object xcdf = Resources.Load<GameObject>("xcdf");
            Object kgcjd = Resources.Load<GameObject>("kgcjd");
            Object hsr = Resources.Load<GameObject>("hsr");
            Object keyu = Resources.Load<GameObject>("U");
            Object keyi = Resources.Load<GameObject>("I");
            Object keyh = Resources.Load<GameObject>("H");            
            Object keyj = Resources.Load<GameObject>("J");
            Object keyb = Resources.Load<GameObject>("B");
            Object keyn = Resources.Load<GameObject>("N");
            
            SJ.Ingredient = new string[]{"ChineseToon","Tofu","BitterMelon","Egg","Mushroom","Pork"};
            SJ.KeyIn = new KeyCode[]{KeyCode.U, KeyCode.I, KeyCode.H, KeyCode.J, KeyCode.B, KeyCode.N};
            SJ.Recipe = new List<string> {"Tofu Salad with Chinese Toon","Bitter Melon Stir-fried with Eggs","Braised Pork Belly with Mushrooms"};
            SJ.KindOfIn = 6;
            SJ.Food = new Object[] {ChineseToon,Tofu,BitterMelon,Egg,Mushroom,Pork};
            SJ.Dish = new Object[] {xcdf,kgcjd,hsr};
            Object[] Ingredient = new Object[]{ChineseToon,Tofu,null,BitterMelon,Egg,null,Mushroom,Pork,null};
            SJ.Keys = new Object[] {keyu, keyi, keyh, keyj, keyb, keyn};
            SJ.IDArray  = new int [][]{
                new int[]{0,3,2,1,2,4},
                new int[]{1,2,3,2,4}, 
                new int[]{0,1,2,0,3,2,3,4},
                new int[]{2,3,2,3,4},
                new int[]{2,0,1,2,3,4},
                new int[]{0,1,2,3,2,0,3,4},
            };
            
            DJ.Recipes.Add("Tofu Salad with Chinese Toon", new List<string> {"ChineseToon","Tofu"});
            DJ.Recipes.Add("Bitter Melon Stir-fried with Eggs", new List<string> {"BitterMelon","Egg"});
            DJ.Recipes.Add("Braised Pork Belly with Mushrooms", new List<string> {"Mushroom","Pork"});
            
            for (int i = 0; i < SJ.Dish.Length;i++){
                pref = Instantiate(SJ.Dish[i]) as GameObject;
                pref.transform.parent = Di[i].transform;
                pref.transform.localPosition = new Vector3(0,0.45f,-0.1f);
                pref.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
                SJ.DiIconSp[i] = pref.GetComponent<SpriteRenderer>();
            }
            
            for (int i = 0; i < Ingredient.Length;i++){
                if (Ingredient[i] != null){
                    pref = Instantiate(Ingredient[i]) as GameObject;
                    pref.transform.parent = In[i].transform;
                    pref.transform.localPosition = new Vector3(0,0,-0.1f);
                    pref.transform.localScale = new Vector3(0.65f,0.65f,1);
                    pref.GetComponent<SpriteRenderer>().color = new Color(0,0,0,0.5f);
                    SJ.InIconSp[n] = pref.GetComponent<SpriteRenderer>();
                    n++;
                }
                else{
                    In[i].gameObject.SetActive(false);
                }
            }
        }
        break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
