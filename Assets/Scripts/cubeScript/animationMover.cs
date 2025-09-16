//objectMoverにアニメーション追加
//制作：ルーク

using System.Collections; // Coroutineを使うために必要
using UnityEngine;

public class AnimatedMover : MonoBehaviour
{
    // [1] Inspectorから設定する項目
    public Transform childObject;       // 動かしたい子オブジェクト
    public Vector3 targetPosition;      // 目標地点
    public float moveDuration = 2.0f;   // 移動にかかる時間（秒）

    // ゲームが開始した時に呼ばれる
    void Start()
    {
        // 子オブジェクトが設定されていれば、移動のコルーチンを開始する
        if (childObject != null)
        {
            StartCoroutine(MoveToTarget());
        }
        else
        {
            Debug.LogError("子オブジェクトが指定されていません！");
        }
    }

    // [2] オブジェクトをスムーズに動かすためのコルーチン
    IEnumerator MoveToTarget()
    {
        Vector3 startPosition = childObject.position; // 移動開始地点
        float elapsedTime = 0f;                       // 経過時間

        // 経過時間が移動時間に達するまでループする
        while (elapsedTime < moveDuration)
        {
            // 開始地点と目標地点の間を、時間経過に応じて滑らかに補間する
            childObject.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);

            // 経過時間を加算
            elapsedTime += Time.deltaTime;

            // 1フレーム待つ
            yield return null;
        }

        // [3] ループ終了後、正確に目標地点に配置する
        childObject.position = targetPosition;
        Debug.Log("移動が完了しました。");
    }
}