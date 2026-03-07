using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager : MonoBehaviour
{
    private bool m_isDecided = false;//Œˆ’èƒ{ƒ^ƒ“‚ð‰Ÿ‚µ‚½‚©‚Ç‚¤‚©

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

    public bool IsDecided()
    {
        return m_isDecided;
    }

    private void Debug()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            m_isDecided = true;
        }
    }
}
