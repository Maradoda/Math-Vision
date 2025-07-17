using UnityEngine;
using UnityEngine.EventSystems; // UIのドラッグイベントを扱うために必要

public class RotateModel : MonoBehaviour, IDragHandler
{
    // Inspectorから回転させたい3Dオブジェクトを設定
    [Tooltip("回転させる3Dオブジェクトをここに設定")]
    public Transform targetObject;

    // 回転の速さ
    [Tooltip("ドラッグした際の回転速度")]
    public float rotationSpeed = 2.0f;

    // UIがドラッグされたときに毎フレーム呼ばれる関数
    public void OnDrag(PointerEventData eventData)
    {
        if (targetObject == null) return;

        // Y軸周りの回転（水平方向のドラッグ）
        targetObject.Rotate(Vector3.up, -eventData.delta.x * rotationSpeed, Space.World);

        // X軸周りの回転（垂直方向のドラッグ）
        targetObject.Rotate(Vector3.right, eventData.delta.y * rotationSpeed, Space.World);
    }
}