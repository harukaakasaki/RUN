using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    public GameObject titleUI;

    public void Show()
    {
        titleUI.SetActive(true);
    }

    public void Hide()
    {
        titleUI.SetActive(false);
    }

    public void OnStartButton()
    {
        Debug.Log("ゲームスタート");
    }
}
