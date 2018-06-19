using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

/// <summary>
/// ジェンガを積み上げるためのスクリプト
/// </summary>
public class CreatJengaTower : MonoBehaviour
{
    [SerializeField]
    GameObject jenga;
    [SerializeField]
    GameObject jengaManager;    // InstansiateしたJengaを親オブジェクトとして格納
    [SerializeField]
    GameObject anchor_position; // ジェンガを積み始めるスタートポイント

    int jenga_number = 0;   // 何個目のジェンガを示す
    string jenga_name;
    int layer_count = 0;    // ジェンガタワーの何層目かを示す
    int replace_count = 0;    // 何回再配置したかを示す
    int place_counter = 1;    // 1と-1で積み上げるジェンガの向き・位置を示す
    Vector3 jenga_position;
    Vector3 place_position;
    Vector3 replace_position;
    Quaternion jenga_rotation;
    Quaternion place_rotation;
    Quaternion replace_rotation;

    public int _layerCount { get { return layer_count; } }
    public int _replaceCount { get { return replace_count; } }

    /// <summary>
    /// プレイ開始時の配置位置と向きを取得
    /// </summary>
    void Start()
    {
        jenga_position = new Vector3();
        place_position = new Vector3();
        place_position = anchor_position.transform.position;
        replace_position = new Vector3();

        jenga_rotation = new Quaternion();
        place_rotation = new Quaternion();
        place_rotation = anchor_position.transform.rotation;
        replace_rotation = new Quaternion();
    }

    /// <summary>
    /// 3つ並びのジェンガを一段積む
    /// </summary>
    public void AlignJenga()
    {
        //配置する座標を設定
        jenga_position = place_position;
        jenga_position.y += jenga.transform.localScale.y * (layer_count + 1);
        //配置する回転角を設定
        jenga_rotation = place_rotation;

        //配置
        GameObject jenga_clone1 = Instantiate(jenga, jenga_position, jenga_rotation);
        jenga_name = "jenga " + (jenga_number + 1).ToString();
        jenga_number += 1;
        jenga_clone1.name = jenga_name;
        jenga_clone1.transform.parent = jengaManager.transform;

        if (place_counter == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                jenga_position.x += jenga.transform.localScale.x;
                GameObject jenga_clone2 = Instantiate(jenga, jenga_position, jenga_rotation);
                jenga_name = "jenga " + (jenga_number + 1).ToString();
                jenga_number += 1;
                jenga_clone2.name = jenga_name;
                jenga_clone2.transform.parent = jengaManager.transform;
            }
        }
        if (place_counter == -1)
        {
            for (int i = 0; i < 2; i++)
            {
                jenga_position.z -= jenga.transform.localScale.x;
                GameObject jenga_clone3 = Instantiate(jenga, jenga_position, jenga_rotation);
                jenga_name = "jenga " + (jenga_number + 1).ToString();
                jenga_number += 1;
                jenga_clone3.name = jenga_name;
                jenga_clone3.transform.parent = jengaManager.transform;
            }
        }

        CalcNextPlace();
    }

    /// <summary>
    /// 抜き取ったジェンガをタワーのてっぺんに再配置する
    /// </summary>
    public void ReplaceJenga()
    {
        // 配置情報の初期化
        if (replace_count == 0)
        {
            // 配置する座標を設定
            replace_position = place_position;
            replace_position.y += jenga.transform.localScale.y * (layer_count + 1);
            // 配置する回転角を設定
            replace_rotation = place_rotation;
        }

        // 配置
        GameObject jenga_clone = Instantiate(jenga, replace_position, replace_rotation);
        jenga_number += 1;
        jenga_name = "jenga " + jenga_number.ToString();
        jenga_clone.name = jenga_name;
        jenga_clone.transform.parent = jengaManager.transform;

        // 次に配置する座標と回転角を設定
        if (place_counter == 1)
        {
            replace_position.x += jenga_clone.transform.localScale.x;
        }
        if (place_counter == -1)
        {
            replace_position.z -= jenga_clone.transform.localScale.x;
        }

        // カウンターの処理
        replace_count += 1;
        if (replace_count >= 3)
        {
            CalcNextPlace();
            replace_count = 0;
        }
    }

    /// <summary>
    /// 次の配置情報を計算する
    /// </summary>
    void CalcNextPlace()
    {
        // 次に配置する座標を計算
        var move_dis = (jenga.transform.localScale.z / 2) - (jenga.transform.localScale.x / 2);
        place_position.x += move_dis * place_counter;
        place_position.z += move_dis * place_counter;

        // 次に配置する回転角を計算
        var rotate_euler = new Quaternion();
        rotate_euler = Quaternion.AngleAxis(45 + (45 * place_counter), Vector3.up);
        place_rotation = rotate_euler;

        // カウンターをそれぞれ次の値に変える
        place_counter = place_counter - (place_counter * 2);
        layer_count += 1;
    }
}