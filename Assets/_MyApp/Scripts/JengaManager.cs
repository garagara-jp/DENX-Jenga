using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaManager : MonoBehaviour
{
    [SerializeField]
    CreatJengaTower creatJengaTower;
    [SerializeField]
    GameOverManager gameOverManager;
    [SerializeField]
    Transform playPosition;
    [SerializeField]
    GameObject room;        // JengaをDestroyするオブジェクト(tag取得用)

    GameObject clickedJenga;

    public CreatJengaTower _creatJengaTower { get { return creatJengaTower; } }
    public GameOverManager _gameOverManager { get { return gameOverManager; } }
    public Transform _playPosition { get { return playPosition; } }
    public GameObject _clickedJenga
    {
        get { return clickedJenga; }
        set { clickedJenga = value; }
    }
    private void Start()
    {
        
    }

    private void Update()
    {

    }

    /// <summary>
    /// ステージに当たったジェンガをDestroyする
    /// </summary>
    public void Destroy(Collision col, GameObject collisionJenga)
    {
        // Jengaがぶつかったオブジェクトと設定したstageのオブジェクトのタグを比較
        if (col.gameObject.tag == room.tag　&& collisionJenga == clickedJenga)
        {
            creatJengaTower.ReplaceJenga();
            Destroy(collisionJenga, 0.1f);
        }
    }
}
