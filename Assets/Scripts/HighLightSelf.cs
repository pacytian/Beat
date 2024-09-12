using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLightSelf : MonoBehaviour
{
    SpriteRenderer sp;
    public  float SetTimer;
    float timer;
    public GameObject Other;
    void Start()
    {
        sp = this.GetComponent<SpriteRenderer>();
    }

    void OnEnable() {
        if (Other.activeSelf){
            Other.SetActive(false);
        }
        timer = SetTimer;
        sp.color = new Color(sp.color.r, sp.color.g, sp.color.b,1);
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
		if (timer < 0.5 && timer > 0){
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b,timer * 2);
        }
        else if (timer <= 0){
            timer = SetTimer;
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b,1);
            gameObject.SetActive(false);
        }
    }
}
