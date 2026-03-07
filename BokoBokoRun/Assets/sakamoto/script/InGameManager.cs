using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGameManager : GameManagerBase
{
    private bool m_isEnd = false;   //ÉQÅ[ÉÄÇ™èIóπÇµÇΩÇ©

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

    public bool IsEnd()
    {
        return m_isEnd;
    }

    private void Debug()
    {
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            m_isEnd = true;
        }
    }
}
