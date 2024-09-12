using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StepJudge : MonoBehaviour
{
    //{"sink","board","bowl","pan","bell"};
    public Queue<int> quesink = new Queue<int>();
    public Queue<int> queboard = new Queue<int>();
    public Queue<int> quebowl = new Queue<int>();
    public Queue<int> quepan = new Queue<int>();
    public Queue<int> quebell = new Queue<int>();
    public int[] quecount = {0,0,0,0,0};//四个位置队列内现有元素的数量
    public int[][] IDArray;//每个食材的步骤，顺序和食材顺序一致[食材（0~2）][对应的步骤（位置id）]
    public int[] index;//每种食材下一步应该进行的步骤
    public int[] count;//每种食材的最大步骤数
    public string[] Ingredient ;//= {"chicken","potato","pepper"};
    public KeyCode[] KeyIn ;//= {KeyCode.J, KeyCode.L, KeyCode.K};//食材对应的按键
    public List<string> Recipe ;//= new List<string> {"saute diced chicken with hot peppers","potato shreds with vinegar"};
    public int KindOfIn;//食材的数量
    //public bool[] change;
    public int key;//收到的按键编号，01234对应qwasd
    public int first,second;//存放每个位置队列中的第一个和第二个元素是什么食材
    //public bool[] inpan;
    public Object[] Food;//存放食材的预制体
    public Object[] Dish;//存放菜肴的预制体
    public Object[] Keys;
    public GameObject[] Steps;//存放五个前背板的位置
    public GameObject[] StepBehind;//存放四个后背板的位置
    public SpriteRenderer[] InIconSp;//存放上方的食材UI
    public SpriteRenderer[] DiIconSp;//存放上方的菜式UI
    public List<int> FinishDishes = new List<int>();//存放做好了的菜的id
    Color spcolor;//存放背板固有色
    //int dishpositiony = 1;//存放菜肴位置的y轴坐标
    //public string DishName;
    GameObject pref;//用于生成预制体，临时使用
    //public int BeatNum;//接受beatjudge传来的当前到了哪个位置的拍子，取值为0123； 

    void Start()
    {

        
        //Food = new Object[] {chicken,potato,pepper};
        //Dish = new Object[] {lzj,sltds};
        //Keys = new Object[] {keyj, keyl, keyk};
        //IDArray = new int [][]{
            //new int[]{0,1,2,3,2,3,4},
            //new int[]{0,1,2,3,2,0,3,4}, 
            //new int[]{1,2,3,4},
        //};
        //KindOfIn = 3;
        //change = new bool[5]{false,false,false,false,false};
        index = new int[KindOfIn]; 
        count = new int[KindOfIn];
        key = 5;//初始化
        first = 100;//初始化
        second = 100;//初始化
        FirstShow();//每种食材的第一个位置入队列
    }

    /*pref = Instantiate(array[Choose(prob)]) as GameObject;
    pref.transform.parent = this.transform;
    pref.transform.localPosition = new Vector3(posx,3.0f,-3.0f);*/
    
    // Update is called once per frame

    public void Restart(){
        quesink = new Queue<int>();
        queboard = new Queue<int>();
        quebowl = new Queue<int>();
        quepan = new Queue<int>();
        quebell = new Queue<int>();
        quecount = new int[]{0,0,0,0,0};
        index = new int[KindOfIn]; 
        count = new int[KindOfIn];
        key = 5;
        first = 100;
        second = 100;
        FinishDishes = new List<int>();
        //把背板顔色還原成半透明並清除食材 + finish的菜清空
        for(int i = 0;i < Steps.Length; i++)
        {
            RemoveAllChildren(Steps[i]);
            if (i < 4){
                Steps[i].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 0.4f); 
            }
        } 
        for(int i = 0;i < StepBehind.Length; i++)
        {
            RemoveAllChildren(StepBehind[i]);
            StepBehind[i].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 0.4f); 
        } 
        //把ui顔色還原成半灰色
        for(int i = 0;i < InIconSp.Length; i++)
        {
            if (InIconSp[i] != null)
            {
                InIconSp[i].color = new Color(0,0,0,0.5f); 
            }  
        }
        for(int i = 0;i < DiIconSp.Length; i++)
        {
            if (DiIconSp[i] != null)
            {
                DiIconSp[i].color = new Color(0,0,0,0.5f); 
            }  
        }
        FirstShow();
    }
    //每种食材的第一个位置入队列
    public void FirstShow(){
        for (int i = 0;i < 5;i++){
            for (int j = 0;j < KindOfIn;j++){
                if (IDArray[j][0] == i){
                    switch (i){
                        case 0:
                            quesink.Enqueue(j);
                            quecount[i]++;
                            break;
                        case 1:
                            queboard.Enqueue(j);
                            quecount[i]++;
                            break;
                        case 2:
                            quebowl.Enqueue(j);
                            quecount[i]++;
                            break;
                        case 3:
                            quepan.Enqueue(j);
                            quecount[i]++;
                            break;
                        case 4:
                            break;
                    }
                    index[j]++;
                    //break;
                }
            }
        }
        // 每个位置显示第一波食材
        if (quesink.Count > 0){
        pref = Instantiate(Food[quesink.Peek()]) as GameObject;
        spcolor = Steps[0].GetComponent<SpriteRenderer>().color;
        Steps[0].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 1);
        pref.transform.parent = Steps[0].transform;
        pref.transform.localPosition = new Vector3(0,0,-0.1f);
        pref = Instantiate(Keys[quesink.Peek()]) as GameObject;
        pref.transform.parent = Steps[0].transform;
        pref.transform.localPosition = new Vector3(0.65f,0.75f,-0.4f);  
        }
        if (queboard.Count > 0){
        pref = Instantiate(Food[queboard.Peek()]) as GameObject;
        Steps[1].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 1);
        pref.transform.parent = Steps[1].transform;
        pref.transform.localPosition = new Vector3(0,0,-0.1f);
        pref = Instantiate(Keys[queboard.Peek()]) as GameObject;
        pref.transform.parent = Steps[1].transform;
        pref.transform.localPosition = new Vector3(0.65f,0.75f,-0.4f);  
        }
        if (quebowl.Count > 0){
        pref = Instantiate(Food[quebowl.Peek()]) as GameObject;
        Steps[2].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 1);
        pref.transform.parent = Steps[2].transform;
        pref.transform.localPosition = new Vector3(0,0,-0.1f);
        pref = Instantiate(Keys[quebowl.Peek()]) as GameObject;
        pref.transform.parent = Steps[2].transform;
        pref.transform.localPosition = new Vector3(0.65f,0.75f,-0.4f);  
        }
        if (quepan.Count > 0){
        pref = Instantiate(Food[quepan.Peek()]) as GameObject;
        Steps[3].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 1);
        pref.transform.parent = Steps[3].transform;
        pref.transform.localPosition = new Vector3(0,0,-0.1f);
        pref = Instantiate(Keys[quepan.Peek()]) as GameObject;
        pref.transform.parent = Steps[3].transform;
        pref.transform.localPosition =new Vector3(0.65f,0.75f,-0.4f);  
        }
    }
    void Update()
    {
        
        /*if(Input.GetKeyDown(KeyCode.Q)){
            key = 0;
            }
        if(Input.GetKeyDown(KeyCode.W)){
            key = 1;
            }
        if(Input.GetKeyDown(KeyCode.A)){
            key = 2;
            }
        if(Input.GetKeyDown(KeyCode.S)){
            key = 3;
            }
        if(Input.GetKeyDown(KeyCode.D)){
            key = 4;
            }
            */
        
        //存放每种食材的最大步骤数用来对照是否完成全部步骤
        for (int i = 0;i < KindOfIn;i ++ ){
            count[i] = IDArray[i].Length;
        }

        if (key != 5 && Steps[key].transform.childCount > 0){
            //Debug.Log(key);
            
            // 对该位置的队列进行操作
            switch (key){
                case 0:
                    if (quesink.Count > 0){
                        first = quesink.Dequeue();
                        quecount[key]--;
                        if (quesink.Count > 0){
                            second = quesink.Peek(); 
                        }
                    }
                    break;
                case 1:
                    if (queboard.Count > 0){
                        first = queboard.Dequeue();
                        quecount[key]--;
                        if (queboard.Count > 0){
                            second = queboard.Peek(); 
                        }
                    }
                    
                    break;
                case 2:
                    if (quebowl.Count > 0){
                        first = quebowl.Dequeue();
                        quecount[key]--;
                        if (quebowl.Count > 0){
                            second = quebowl.Peek(); 
                        }
                    }
                    
                    break;
                case 3:
                    if (quepan.Count > 0){
                        first = quepan.Dequeue();
                        quecount[key]--;
                        if (quepan.Count > 0){
                            second = quepan.Peek(); 
                        }
                    }
                    
                    break;
                case 4:
                    break;
            }
            //Debug.Log("Queue: No. " + first + " ingredient get out, and No. "+ second +" ingredient ready to get in.");
            
            //如果该食材有下个位置则加入下个位置的队列，如果下个位置无原有食材，则在下个位置生成该食材
            if (index[first]<count[first]){
                int i = IDArray[first][index[first]];
                switch (i){
                case 0:
                    quesink.Enqueue(first);
                    quecount[i]++;
                    break;
                case 1:
                    queboard.Enqueue(first);
                    quecount[i]++;
                    break;
                case 2:
                    quebowl.Enqueue(first);
                    quecount[i]++;
                    break;
                case 3:
                    quepan.Enqueue(first);
                    quecount[i]++;
                    break;
                case 4:
                    quecount[i]++;
                    this.GetComponent<DishJudge>().PutIngredientInFurnace(Ingredient[first]);
                    InIconSp[first].color = new Color(1, 1, 1, 1);
                    //Debug.Log("Finish: No. " + first + " ingredient.");
                    //first = 100;

                    if(quecount[4] == Ingredient.Length){
                        GameObject.Find("MainAudio").GetComponent<AudioSource>().Stop();
                    }
                    break;
                }                        
                if (quecount[i] == 1 && first != 100 && i != 4){ 
                    pref = Instantiate(Food[first]) as GameObject;
                    pref.transform.parent = Steps[i].transform;
                    pref.transform.localPosition = new Vector3(0,0,-0.1f);
                    pref = Instantiate(Keys[first]) as GameObject;
                    pref.transform.parent = Steps[i].transform;
                    pref.transform.localPosition = new Vector3(0.65f,0.75f,-0.4f);  
                    index[first]++;
                    //Debug.Log("First: Step No. " + i + " shows No. " + first + " ingredient.");
                    first = 100;
                }
                else{
                    index[first]++;
                    //Debug.Log("First: Step No. " + i + " get No. " + first + " ingredient in the queue.");
                }
            }
            
            // 删除该位置的食材
            RemoveAllChildren(Steps[key]);
            //Debug.Log("Remove: Step No. " + key);
            
            //如果该位置的队列中有下个食材，在该位置填上新的食材
            if (second != 100){
                pref = Instantiate(Food[second]) as GameObject;
                pref.transform.parent = Steps[key].transform;
                pref.transform.localPosition = new Vector3(0,0,-0.1f);
                pref = Instantiate(Keys[second]) as GameObject;
                pref.transform.parent = Steps[key].transform;
                pref.transform.localPosition = new Vector3(0.65f,0.75f,-0.4f); 

                //index[second]++;
                //Debug.Log("Second: Step No. " + key + " shows No. " + second + " ingredient.");
                second = 100;
            }
            else{
                //Debug.Log("Second: Step No. " + key + " is empty.");
            }
            key = 5;
        }
        else if (key != 5){
            //Debug.Log("No thing happened.");
            key = 5;
        }
        
        //调整全部背景板颜色
        for (int i = 0;i < 4;i ++){
            if(quecount[i] == 0){
                Steps[i].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 0.4f);
            }
            else{
                Steps[i].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 1);
            }
            if(quecount[i] > 1){
                StepBehind[i].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 1);
            }
            else{
                StepBehind[i].GetComponent<SpriteRenderer>().color = new Color(spcolor.r , spcolor.g , spcolor.b , 0.4f);
            }
        }

            /*for (int i = 0;i < KindOfIn;i ++ ){
                if (index[i] < count[i]){ 
                    if (a == IDArray[i][index[i]]){
                        place[a] = i + 1;
                        //index[i]++;
                        break;
                    }
                }
            
            }*/
        
    }

    public static void RemoveAllChildren(GameObject parent){
            Transform transform;
            for(int i = 0;i < parent.transform.childCount; i++)
            {
                transform = parent.transform.GetChild(i);
                GameObject.Destroy(transform.gameObject);
            }
    }

    void GetNameOfDish(string dishname){
        int i = Recipe.IndexOf(dishname);
        DiIconSp[i].color = new Color(1, 1, 1, 1);
        FinishDishes.Add(i);
        if (i > -1){    
            //Debug.Log("2.Finish the dish No. " + i + " : " + dishname);          

            pref = Instantiate(Dish[i]) as GameObject;
            pref.transform.parent = Steps[i + 4].transform;
            pref.transform.localPosition = new Vector3(0,0,0);
            //dishpositiony --;
        }
    }
    // 根据当前节拍进行到的位置上的食材给keyjudge传key2的值
    public void PassIngredientKey(int BeatNum){
        int a;
        switch (BeatNum){
                case 0:
                    if(quesink.Count > 0){
                        a = quesink.Peek();
                        this.GetComponent<KeyJudge>().key2 = KeyIn[a];
                    }
                    else{
                        this.GetComponent<KeyJudge>().key2 = KeyCode.None;
                    }         
                    break;
                case 1:
                    if(queboard.Count > 0){
                        a = queboard.Peek();
                        this.GetComponent<KeyJudge>().key2 = KeyIn[a];
                    }
                    else{
                        this.GetComponent<KeyJudge>().key2 = KeyCode.None;
                    }
                    break;
                case 2:
                    if(quebowl.Count > 0){
                        a = quebowl.Peek();
                        this.GetComponent<KeyJudge>().key2 = KeyIn[a];
                    }
                    else{
                        this.GetComponent<KeyJudge>().key2 = KeyCode.None;
                    }
                    
                    break;
                case 3:
                    if(quepan.Count > 0){
                        a = quepan.Peek();
                        this.GetComponent<KeyJudge>().key2 = KeyIn[a];
                    }
                    else{
                        this.GetComponent<KeyJudge>().key2 = KeyCode.None;
                    }
                    break;
                }    
    }
}
