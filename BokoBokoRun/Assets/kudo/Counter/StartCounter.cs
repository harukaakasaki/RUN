using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_countUI;

     private int m_startCount = 3;
     private float m_interval = 1;
    private string m_startLavel = "Start!";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCount()
    {
        
    }

    private IEnumerator PlayCountDown()
    {
        for(int i = m_startCount; i >= 0;i--)
        {
            m_countUI.text = i.ToString();
            yield return new WaitForSeconds(m_interval); // 1秒待機
        }
    }
}
