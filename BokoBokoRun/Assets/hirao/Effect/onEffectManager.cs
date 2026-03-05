using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class onEffectManager : MonoBehaviour
{
    //[SerializeField] private GameObject m_testEf;
    //こうすることでinspector上でエフェクトの名前とプレハブをセットできるようになる
    [System.Serializable]
    public class  EffectData
    {
        public string name;
        public GameObject effectPrefab;
    }
    [SerializeField] private List<EffectData> m_effectList = new List<EffectData>();//エフェクトの名前とプレハブをセットするためのリスト
    [SerializeField] private Dictionary<string, GameObject> m_effectDictionary;//検索をかけるようの辞書

    // Start is called before the first frame update
    void Start()
    {
        m_effectDictionary = new Dictionary<string, GameObject>();

        foreach(var effect in m_effectList)//初期化//リストを回して、エフェクトの名前とプレハブを辞書に追加する
        {
            m_effectDictionary.Add(effect.name, effect.effectPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Effectを出す関数
    /// </summary>
    /// <param name="pos">エフェクトを出す位置</param>
    /// <param name="name">エフェクトの名前</param>
    public void PlayEffect(Vector3 pos,string name)
    {
        if(m_effectDictionary.TryGetValue(name,out GameObject effect))//TryGetValueはキーが存在するか確認して、あれば値を取り出し、結果をboolで返す
        {
            Instantiate(effect,pos,Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Effect not found : " + name);//エフェクトが見つからない場合の警告
        }

           
    }
}
