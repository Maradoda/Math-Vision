//表示されている場合、非表示にする

using UnityEngine;

public class ToggleVisibility : MonoBehaviour
{
    public GameObject targetObject; // 表示・非表示を切り替えたいオブジェクト

    public void OnClick()
    {
        if (targetObject == null) return;

        // オブジェクトが表示されている場合のみ非表示にする
        if (targetObject.activeSelf)
        {
            targetObject.SetActive(false);
            Debug.Log($"{targetObject.name} を非表示にしました");
        }
        else
        {
            Debug.Log($"{targetObject.name} はすでに非表示です");
        }
    }
}

