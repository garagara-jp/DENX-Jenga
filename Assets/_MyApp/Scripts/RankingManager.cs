using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコアを取得し、UIに表示する
/// </summary>
public class RankingManager : MonoBehaviour
{
    // 画面に表示するUI
    [SerializeField]
    GameObject ranking;
    [SerializeField]
    Text scoresText;

    int[] layerScores;
    int[] pieceScores;

    void Start()
    {
        // UIの初期化
        ranking.SetActive(false);

        // 配列の初期化
        layerScores = PlayerPrefsX.GetIntArray("LayerScores", 0, 5);
        pieceScores = PlayerPrefsX.GetIntArray("PieceScores", 0, 5);
    }

    /// <summary>
    /// ランキングを表示
    /// </summary>
    public void ShowRanking(bool isShowed)
    {
        if (isShowed)
        {
            scoresText.text = "";
            for (int i = 0; i < layerScores.Length; i++)
            {
                scoresText.text += layerScores[i].ToString() + "段 と " + pieceScores[i].ToString() + "個\r\n";
            }
            ranking.SetActive(true);
        }
        else
            ranking.SetActive(false);
    }
}
