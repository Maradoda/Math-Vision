//キューブの中身を横に出す
//制作：ルーク

using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    // Inspectorから移動させたい子オブジェクトを指定します
    public Transform childObject;

    // Inspectorから移動させたい目標座標を指定します
    public Vector3 targetPosition;

    // ゲームが開始した時に一度だけ呼ばれます
    void Start()
    {
        // 子オブジェクトが指定されていなければ、何もしない
        if (childObject == null)
        {
            Debug.LogError("子オブジェクトが指定されていません！");
            return;
        }

        // 子オブジェクトのワールド座標を直接 targetPosition に設定します
        // .position はワールド座標を指します
        childObject.position = targetPosition;
    }

    // ▼ボタンなどで好きな時に動かしたい場合は、以下のような関数を作ります
    // （この関数は今の時点では自動では動きません）
    public void MoveChild()
    {
        if (childObject != null)
        {
            childObject.position = targetPosition;
        }
    }
}