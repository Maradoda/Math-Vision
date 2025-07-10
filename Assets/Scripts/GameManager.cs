using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // このGameManagerの唯一のインスタンスを保持する静的変数
    public static GameManager Instance { get; private set; }

    // 選択された年度を保存する変数
    public string selectedYear;

    // ゲーム開始時（シーン読み込み時）に一度だけ呼ばれる
    private void Awake()
    {
        // インスタンスがまだなければ、このオブジェクトをインスタンスとして設定
        if (Instance == null)
        {
            Instance = this;
            // このオブジェクトは新しいシーンが読み込まれても破棄されないようにする
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // すでにインスタンスが存在する場合は、このオブジェクトを破棄する
            Destroy(gameObject);
        }
    }

    // --- シーンを読み込むための関数 ---

    public void LoadTopPageScene()
    {
        SceneManager.LoadScene("TopPage_scene");
    }

    public void LoadQuestionScene()
    {
        SceneManager.LoadScene("Question_scene");
    }

    public void LoadAnswerScene()
    {
        SceneManager.LoadScene("Answer_scene");
    }
}