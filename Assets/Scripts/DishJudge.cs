using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishJudge : MonoBehaviour
{
    // 丹药配方，Key是丹药的名称，Value是该丹药的配方（用List<string>表示药材）
    public Dictionary<string, List<string>> Recipes = new Dictionary<string, List<string>>();

    // 当前炉子中的药材列表
    public List<string> PresentIngredients = new List<string>();
    void Start()
    {
        // 初始化丹药配方
        //Recipes.Add("saute diced chicken with hot peppers", new List<string> { "chicken", "pepper" });
        //Recipes.Add("potato shreds with vinegar", new List<string> {"potato"});

        // 将药材放入炉子
        //PutIngredientInFurnace("药材1");
        //PutIngredientInFurnace("药材2");
    }

    public void PutIngredientInFurnace(string ingredient)
    {
        PresentIngredients.Add(ingredient);
        CheckDish();
    }

    void CheckDish()
    {
        foreach (var recipe in Recipes)
        {
            bool canMake = true;
            foreach (string ingredient in recipe.Value)
            {
                if (!PresentIngredients.Contains(ingredient))
                {
                    canMake = false;
                    break;
                }
            }

            if (canMake)
            {
                //Debug.Log("1.Finish the dish: " + recipe.Key);
                foreach (string ingredient in recipe.Value)
                {
                    PresentIngredients.Remove(ingredient);
                }
                this.GetComponent<StepJudge>().SendMessage("GetNameOfDish",recipe.Key);
            }
        }
    }
}