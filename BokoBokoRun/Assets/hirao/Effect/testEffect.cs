using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testEffect : MonoBehaviour
{
    [SerializeField] private onEffectManager m_efManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.jKey.wasPressedThisFrame)
        {
            string name = "impact";
            //エフェクトを出す
            m_efManager.PlayEffect(new Vector3(0,0,0),name);
            Debug.Log("エフェクトを押したよ");
        }
    }
}
