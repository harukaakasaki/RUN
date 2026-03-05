using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    public GameObject resultUI;

    public void Show()
    {
        resultUI.SetActive(true);
    }

    public void Hide()
    {
        resultUI.SetActive(false);
    }

    public void OnStartButton()
    {
        Debug.Log("ƒŠƒUƒ‹ƒg");
    }
}
