using UnityEngine;

public class ControlViewTop : MonoBehaviour
{
    [Header("対象の図形オブジェクト")]
    public GameObject targetObject;
        void Start()
    {
        // ★ シーン開始時に「ボタンを押した状態」にする
        OnClick();
    }

    public void OnClick()
    {
        if (targetObject == null) return;

        // ② 上から見えるように回転
        ViewFromTop(targetObject);
    }

    // 上から見る（X軸を90度回転）
    void ViewFromTop(GameObject obj)
    {
        obj.transform.rotation = Quaternion.Euler(90f, 270f, 90f);
    }
}
