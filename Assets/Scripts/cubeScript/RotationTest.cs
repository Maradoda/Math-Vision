//マウスカーソルで図形を動かすプログラム

using UnityEngine;
using UnityEngine.EventSystems;

public class RotationTest : MonoBehaviour
{
    [Header("ターゲット設定")]
    [SerializeField, Tooltip("回転させたいオブジェクト")]
    private Transform _target;

    [Header("操作設定")]
    [SerializeField, Tooltip("マウスドラッグ時の回転速度")]
    private float _speed = 5f;

    [Header("ドラッグ可能エリア")]
    [SerializeField, Tooltip("回転操作が有効なUIエリア（RectTransform）")]
    private RectTransform _dragArea;

    private bool _isDragging = false;

    private void Update()
    {
        if (_target == null || _dragArea == null)
            return;

        // マウスが押された瞬間にドラッグエリア内かを確認
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(_dragArea, mousePos, null))
                _isDragging = true;
            else
                _isDragging = false;
        }

        // マウスボタンを離したらドラッグ終了
        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }

        // ドラッグ中のみ回転処理
        if (_isDragging && Input.GetMouseButton(0))
        {
            float rotX = Input.GetAxis("Mouse Y") * _speed ; // 上下方向
            float rotY = -Input.GetAxis("Mouse X") * _speed;      // 左右方向
            Vector3 rotation = new Vector3(rotX, rotY, 0);
            _target.Rotate(rotation, Space.World);
        }
    }
}
