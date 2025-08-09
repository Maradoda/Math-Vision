using UnityEngine;
using UnityEngine.UI;

public class TopPageController : MonoBehaviour
{
    // 中央に年度の図形を表示するImage
    public Image displayImage;

    // --- 起動時に実行 ---
    void Start()
    {
        // 起動時に画像を透明にして非表示にする
        if (displayImage != null)
        {
            displayImage.color = Color.clear;
        }
    }

    // 年度選択ボタンが押されたときに呼ばれる
    public void SetSelectedYear(string yearIdentifier)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetCurrentYear(yearIdentifier);
        }
    }

    // 中央の画像を指定されたスプライトに変更する
    public void ChangeDisplayImage(Sprite newImage)
    {
        if (displayImage != null)
        {
            displayImage.sprite = newImage;
            // 透明にした色を白（不透明）に戻して表示
            displayImage.color = Color.white;
        }
    }

    // 問題ボタンが押されたときに呼ばれる
    public void OnClickQuestionButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadQuestionScene();
        }
    }

    // 解説ボタンが押されたときに呼ばれる
    public void OnClickAnswerButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadAnswerScene();
        }
    }
}