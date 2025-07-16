using UnityEngine;
using UnityEngine.UI; // Imageを扱うために必要

public class TopPageController : MonoBehaviour
{
    // Inspectorから設定する、中央に表示するImage UI
    public Image displayImage;

    // 年度選択ボタンが押されたときに呼ばれる関数
    // 引数は "R7" のような識別子
    public void SetSelectedYear(string yearIdentifier)
    {
        if (GameManager.Instance != null)
        {
            // GameManagerに現在の年度識別子をセットする
            GameManager.Instance.SetCurrentYear(yearIdentifier);
        }
    }

    // 中央の画像を指定されたスプライトに変更する
    public void ChangeDisplayImage(Sprite newImage)
    {
        if (displayImage != null)
        {
            displayImage.sprite = newImage;
            // 画像が透明な場合は不透明に戻す
            if (displayImage.color.a < 1)
            {
                displayImage.color = Color.white;
            }
        }
    }

    // 問題ボタンが押されたときに呼ばれる関数
    public void OnClickQuestionButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadQuestionScene();
        }
    }

    // 解説ボタンが押されたときに呼ばれる関数
    public void OnClickAnswerButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoadAnswerScene();
        }
    }
}