using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバーの判定をする
/// Tenbanにアタッチ
/// </summary>
public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    JengaManager jengaManager;

    [SerializeField]
    int finishNum = 10;

    bool isFinish;
    List<GameObject> list = new List<GameObject>();

    public bool _isFinish { get { return isFinish; } }

    void Start()
    {
        isFinish = false;
    }

    void Update()
    {
        FinishGame();
        //Debug.Log(list.Count);
    }

    void OnCollisionEnter(Collision col)
    {
        // 自分に当たったジェンガが抜き取られたジェンガでないかを判定
        if(col.gameObject != jengaManager._clickedJenga)
            list.Add(col.gameObject); 
    }

    void FinishGame()
    {
        // 自分の上に一定数以上のジェンガが落ちたら終了判定を出す
        if (list.Count >= finishNum)
        {
            isFinish = true;
        }
    }
}
