using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class SingleCase<T> : MonoBehaviour
    where T : SingleCase<T>
{
    private static T m_instance = null;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if(m_instance == null)
                {
                    m_instance = new GameObject("Singleton of" +typeof(T).ToString(),typeof(T)).GetComponent<T>();
                    m_instance.init();
                }
            }
            return m_instance;
        }
    }
    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this as T;
        }
    }
    public virtual void init(){}
    private void OnApplicationQuit()
    {
        m_instance = null;
    }
}