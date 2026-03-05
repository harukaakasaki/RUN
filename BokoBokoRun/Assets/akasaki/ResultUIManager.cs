using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    public GameObject resultUI;

    // リザルトUIを表示する
    public void Show()
    {
        // UIオブジェクトをアクティブにして表示する
        resultUI.SetActive(true);
    }

    // リザルトUIを非表示する
    public void Hide()
    {
        // UIオブジェクトを非アクティブにして非表示する
        resultUI.SetActive(false);
    }

    public void OnStartButton()
    {
        Debug.Log("リザルト");
        // ここでUI切り替えなどを行う
    }
}
