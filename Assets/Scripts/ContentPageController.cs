using UnityEngine;
using UnityEngine.UI;    // UI要素を扱うために必要
using TMPro;             // TextMeshProを扱うために必要
using System.Collections.Generic; // Listを扱うために必要

public class ContentPageController : MonoBehaviour
{
    // Inspectorから設定する共通のUI要素
    public TMP_Text yearTitleText;

    // Question_sceneでのみ使うUI要素
    public Image questionImageDisplay; // 問題図形を表示するImage
    public List<Button> questionButtons; // 「問1」「問2」などのボタンのリスト

    void Start()
    {
        // GameManagerが存在するか確認
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager not found!");
            return;
        }

        // GameManagerから現在の年度データを取得
        var currentData = GameManager.Instance.currentYearData;

        if (currentData == null) 
        {
            Debug.LogError("Current Year Data is not set in GameManager!");
            return;
        }

        // yearTitleTextの更新
        if (yearTitleText != null)
        {
            // 表示ルールに合わせて年度タイトルを設定 (例: R7 -> 令和7年)
            yearTitleText.text = "令和" + currentData.yearIdentifier.Substring(1) + "年度";
        }
        
        // 問題ページのセットアップ処理 (Question_sceneでのみ動作)
        if (questionButtons != null && questionButtons.Count > 0 && questionImageDisplay != null)
        {
            SetupQuestionButtons(currentData);
        }
    }

    void SetupQuestionButtons(YearQuestionData data)
    {
        // まず全てのボタンを非表示にする
        foreach (var btn in questionButtons)
        {
            btn.gameObject.SetActive(false);
        }
        
        // 最初の図形を自動で表示しておく
        if (data.questionImages.Count > 0)
        {
            questionImageDisplay.sprite = data.questionImages[0];
            questionImageDisplay.color = Color.white;
        }
        else
        {
            // 表示する図形がない場合は透明にする
            questionImageDisplay.color = new Color(1,1,1,0);
        }

        // データとして存在する図形の数だけボタンをセットアップする
        for (int i = 0; i < data.questionImages.Count; i++)
        {
            if (i >= questionButtons.Count) break;

            Button button = questionButtons[i];
            Sprite imageToShow = data.questionImages[i];

            button.gameObject.SetActive(true);
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => 
            {
                questionImageDisplay.sprite = imageToShow;
                questionImageDisplay.color = Color.white;
            });
        }
    }
    
    // --- シーン遷移の関数 ---
    public void OnClickTopButton()
    {
        GameManager.Instance?.LoadTopPageScene();
    }

    public void OnClickQuestionButton()
    {
        GameManager.Instance?.LoadQuestionScene();
    }

    public void OnClickAnswerButton()
    {
        GameManager.Instance?.LoadAnswerScene();
    }
}