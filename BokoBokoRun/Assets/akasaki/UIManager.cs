using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject titleUI;//タイトルUI
    [SerializeField] private GameObject selectUI;//セレクトUI
    [SerializeField] private GameObject inGameUI;//インゲームUI
    [SerializeField] private GameObject resultUI;//リザルトUI

    // UIをシーンごとに切り替える
    public void ChangeUI(GameFlowManager.Scene scene)
    {
        // 全部消す
        titleUI.SetActive(false);
        selectUI.SetActive(false);
        inGameUI.SetActive(false);
        resultUI.SetActive(false);

        // 必要に応じてUIを切り替える
        switch (scene)
        {
            case GameFlowManager.Scene.Title:
                titleUI.SetActive(true);
                break;

            case GameFlowManager.Scene.Select:
                selectUI.SetActive(true);
                break;

            case GameFlowManager.Scene.InGame:
                inGameUI.SetActive(true);
                break;

            case GameFlowManager.Scene.Result:
                resultUI.SetActive(true);
                break;
        }

    }

}
