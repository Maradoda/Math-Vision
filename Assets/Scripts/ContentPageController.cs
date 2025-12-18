using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ContentPageController : MonoBehaviour
{
    // --- 共通UI ---
    public TMP_Text yearTitleText;
    public TMP_Text questionBodyText;

    // --- 問題ページ専用UI ---
    public Image questionImageDisplay;
    public List<Button> questionButtons;     // 問1, 問2...の画像表示ボタン
    public List<TMP_InputField> inputFields;
    public Button scoringButton;
    public List<Image> individualResultImages;
    public Sprite correctSprite;
    public Sprite incorrectSprite;

    // --- 解説ページ専用UI ---
    [Header("Answer Scene UI")]
    public TMP_Text explanationTextDisplay; 
    
    // 解説用の内部変数
    private int currentExplanationIndex = 0; // 0=問1, 1=問2...
    private YearQuestionData currentData;

    void Start()
    {
        // データ取得
        currentData = GameManager.Instance?.currentYearData;

        // タイトルなどの表示設定
        if (currentData != null)
        {
            if (yearTitleText != null)
            {
                string yearNumber = currentData.yearIdentifier.Replace("R", "");
                yearTitleText.text = $"令和{yearNumber}年度";
            }
            if (questionBodyText != null)
            {
                questionBodyText.text = currentData.questionText;
            }
        }

        // --- 問題シーンのセットアップ（ここを修正！） ---
        SetupQuestionSceneUI();

        // --- 解説シーンの初期表示（手動設定したボタンで動くのでこれだけでOK） ---
        ShowExplanation(0, 0);
    }

    // ==================================================
    // 解説ページ用：手動で呼び出すための関数（前回と同じ）
    // ==================================================

    public void OnClickQuestion1() 
    { 
        currentExplanationIndex = 0; 
        ShowExplanation(currentExplanationIndex, 0);
    }

    public void OnClickQuestion2() 
    { 
        currentExplanationIndex = 1; 
        ShowExplanation(currentExplanationIndex, 0);
    }

    public void OnClickStep1() { ShowExplanation(currentExplanationIndex, 0); }
    public void OnClickStep2() { ShowExplanation(currentExplanationIndex, 1); }
    public void OnClickStep3() { ShowExplanation(currentExplanationIndex, 2); }

    // 解説文を表示する処理
    private void ShowExplanation(int questionIndex, int stepIndex)
    {
        if (currentData == null || currentData.explanationList == null) return;
        if (explanationTextDisplay == null) return;

        if (questionIndex >= currentData.explanationList.Count)
        {
            explanationTextDisplay.text = "この問題の解説データはありません";
            return;
        }

        var targetQuestion = currentData.explanationList[questionIndex];
        
        if (targetQuestion.stepTexts != null && stepIndex < targetQuestion.stepTexts.Count)
        {
            explanationTextDisplay.text = targetQuestion.stepTexts[stepIndex];
        }
        else
        {
            explanationTextDisplay.text = "解説文が登録されていません";
        }
    }


    // ==================================================
    // ★★★ 今回の修正箇所：問題ページの画像表示機能 ★★★
    // ==================================================
    void SetupQuestionSceneUI()
    {
        // 画像表示エリアを一旦透明にする（画像がないとき用）
        if (questionImageDisplay != null) questionImageDisplay.color = Color.clear;

        // ボタンの設定（ここが消えていました！復活させます）
        if (questionButtons != null && currentData != null)
        {
            // まず全部のボタンを隠す
            foreach (var btn in questionButtons) 
            {
                if(btn != null) btn.gameObject.SetActive(false);
            }

            // 画像データの数だけボタンを表示し、機能を割り当てる
            for (int i = 0; i < currentData.questionImages.Count; i++)
            {
                // UI上のボタンが足りない場合はループを抜ける
                if (i >= questionButtons.Count) break;

                Button button = questionButtons[i];
                Sprite imageToShow = currentData.questionImages[i]; // 表示する画像

                if (button != null)
                {
                    button.gameObject.SetActive(true);
                    
                    // ★重要：ボタンを押したときに画像を切り替える処理
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() =>
                    {
                        if (questionImageDisplay != null)
                        {
                            questionImageDisplay.sprite = imageToShow;
                            questionImageDisplay.color = Color.white; // 画像を表示するので白（不透明）にする
                        }
                    });
                }
            }
        }

        // 入力フィールドの設定
        if (inputFields != null && currentData != null)
        {
            foreach (var field in inputFields) field.gameObject.SetActive(false);
            for (int i = 0; i < currentData.questionAnswers.Count; i++)
            {
                if (i < inputFields.Count)
                {
                    inputFields[i].gameObject.SetActive(true);
                    inputFields[i].text = "";
                }
            }
        }

        // 採点ボタンの設定
        if (scoringButton != null)
        {
            scoringButton.onClick.RemoveAllListeners();
            scoringButton.onClick.AddListener(CheckAnswers);
        }

        // 結果画像（○×）を初期化
        if (individualResultImages != null)
        {
            foreach (var img in individualResultImages) img.gameObject.SetActive(false);
        }
    }

    public void CheckAnswers()
    {
        if (currentData == null) return;
        for (int i = 0; i < currentData.questionAnswers.Count; i++)
        {
            if (i >= inputFields.Count || i >= individualResultImages.Count) break;
            string userAns = inputFields[i].text.Trim();
            string correct = currentData.questionAnswers[i];
            individualResultImages[i].gameObject.SetActive(true);
            individualResultImages[i].sprite = (userAns == correct) ? correctSprite : incorrectSprite;
        }
    }

    public void OnClickTopButton() { GameManager.Instance?.LoadTopPageScene(); }
    public void OnClickQuestionButton() { GameManager.Instance?.LoadQuestionScene(); }
    public void OnClickAnswerButton() { GameManager.Instance?.LoadAnswerScene(); }
}