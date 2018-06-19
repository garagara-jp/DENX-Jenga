using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// ハイスコアの取得・保存を行い、配列を作成
/// GameManagerにアタッチ
/// </summary>
public class HighScoreClient : MonoBehaviour
{
    const string LAYER_SCORES_KEY = "LayerScores";
    const string PIECE_SCORES_KEY = "PieceScores";
    const string COUNT_KEY = "CountKey";

    [SerializeField]
    CreatJengaTower creatJengaTower;

    int[] layerScores;
    int[] pieceScores;

    public int[] _layerScores { get { return layerScores; } }
    public int[] _pieceScores { get { return pieceScores; } }

    void Start()
    {
        // データの初期化
        layerScores = PlayerPrefsX.GetIntArray("LayerScores", 0, 5);
        pieceScores = PlayerPrefsX.GetIntArray("PieceScores", 0, 5);
    }

    void Update()
    {
        SaveHighScore();
    }

    /// <summary>
    /// スコアの保存
    /// </summary>
    void SaveHighScore()
    {
        // スコアを監視
        if (creatJengaTower._layerCount <= layerScores[layerScores.Length - 1])
            return;
        if (creatJengaTower._replaceCount <= pieceScores[pieceScores.Length - 1])
            return;

        // ハイスコアが出た場合配列を更新
        layerScores[0] = creatJengaTower._layerCount;
        pieceScores[0] = creatJengaTower._replaceCount;
        for (int i = 1; i < layerScores.Length - 1; i++)
        {
            layerScores[i] = layerScores[i + 1];
            pieceScores[i] = pieceScores[i + 1];
        }

        var unsortedScores = layerScores;
        Array.Sort(layerScores, (a, b) => b - a);

        // 配列を保存
        PlayerPrefsX.SetIntArray("LayerScores", layerScores);
        PlayerPrefsX.SetIntArray("PieceScores", pieceScores);
        PlayerPrefs.Save();
    }
}