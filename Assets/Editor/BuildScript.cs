using UnityEditor;
using UnityEditor.Build.Reporting;
using System;
using System.IO;
using UnityEngine;

public class BuildScript
{
    [MenuItem("Build/Build Android")]
    public static void BuildAndroid()
    {
        try
        {
            // --- ビルド設定 ---
            // Keystoreのパスワードなどを環境変数から取得
            PlayerSettings.Android.keystoreName = Environment.GetEnvironmentVariable("ANDROID_KEYSTORE_NAME");
            PlayerSettings.Android.keystorePass = Environment.GetEnvironmentVariable("ANDROID_KEYSTORE_PASS");
            PlayerSettings.Android.keyaliasName = Environment.GetEnvironmentVariable("ANDROID_KEYALIAS_NAME");
            PlayerSettings.Android.keyaliasPass = Environment.GetEnvironmentVariable("ANDROID_KEYALIAS_PASS");
            
            // ビルドターゲットをAndroidに設定
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);

            // ビルドオプションを設定
            var buildPlayerOptions = new BuildPlayerOptions
            {
                // ビルドに含めるシーンのリスト
                scenes = GetEnabledEditorScenes(),
                // ビルド成果物の出力先 (コマンドライン引数から取得)
                locationPathName = GetBuildPath(),
                target = BuildTarget.Android,
                // AAB (Google Play推奨) を出力する場合はこちら
                options = BuildOptions.None,
            };
            
            // --- ビルド実行 ---
            Debug.Log("ビルドを開始します...");
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            // --- 結果のハンドリング ---
            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"ビルド成功！サイズ: {summary.totalSize / 1024 / 1024} MB");
                EditorApplication.Exit(0); // 成功コードで終了
            }
            else
            {
                Debug.LogError($"ビルド失敗: {summary.result}");
                EditorApplication.Exit(1); // エラーコードで終了
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"ビルド中に例外が発生しました: {e.Message}\n{e.StackTrace}");
            EditorApplication.Exit(1); // エラーコードで終了
        }
    }

    // 有効なシーンを取得するヘルパーメソッド
    private static string[] GetEnabledEditorScenes()
    {
        var scenes = new System.Collections.Generic.List<string>();
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenes.Add(scene.path);
            }
        }
        return scenes.ToArray();
    }
    
    // コマンドライン引数からビルドパスを取得するヘルパーメソッド
    private static string GetBuildPath()
    {
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "-buildPath" && i + 1 < args.Length)
            {
                // 出力先ディレクトリが存在しない場合は作成
                string path = args[i + 1];
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                return path;
            }
        }
        // デフォルトパス
        return "Builds/Android/default.aab";
    }
}