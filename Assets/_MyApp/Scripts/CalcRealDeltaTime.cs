using UnityEngine;
using System.Collections;

// GameSystemにアタッチ
// 現実時間基準でΔ時間を求める

public class CalcRealDeltaTime : MonoBehaviour {

    float real_deltatime;   // 現実時間基準のΔ時間
    float last_realtime;    // 現実時間基準の前フレーム時点での経過時間

    float count_deltatime;
    float count_second;
    float real_count;

    public float realDeltaTime { get { return real_deltatime; } }

    void Update()
    {
        CalcTime();
    }

    void CalcTime()
    {
        if (last_realtime == 0)
        {
            last_realtime = Time.realtimeSinceStartup;
        }
        real_deltatime = Time.realtimeSinceStartup - last_realtime;
        last_realtime = Time.realtimeSinceStartup;
    }

}
