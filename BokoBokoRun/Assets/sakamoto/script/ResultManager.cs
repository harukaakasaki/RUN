using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultManager : GameManagerBase
{
    private bool m_isBackTitle = false;//タイトルに戻るか

    private string m_bestPlayerTag;//一位になったプレイヤーのタグ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR 
        Debug();
#endif
    }

    public bool IsBackTitle()
    {
        return m_isBackTitle;
    }

    private void Debug()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            m_isBackTitle = true;
        }
    }

    public void SetBestPlayerTag(string bestPlayer)
    {
        m_bestPlayerTag = bestPlayer;
    }
}
