using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBase : MonoBehaviour
{
    [SerializeField] protected FadeManager m_fadeManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected virtual void OnEnable()
    {
        m_fadeManager.OnFadeIn();
    }
    protected virtual void OnDisable()
    {
        m_fadeManager.OnFadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
