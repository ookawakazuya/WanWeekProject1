using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum StageState { Observing,Playing,Cleared,Failed}

public class StageManager : MonoBehaviour
{
    [Header("足場の設定")]
    public List<GameObject> platforms;

    [Header("観察時間")]
    public float observeDuration = 3f;

    [Header("観察終了後の猶予時間")]
    public float prePlayDelay = 1.0f;

    [Header("UI表示")]
    [SerializeField] Text infoText;
    [SerializeField] Text timerText;

    public StageState CurrentState { get; private set; } = StageState.Observing;

    [SerializeField] CameraSwitch cameraSwitch;
    [SerializeField] PlayerController playerController;

    float timer;

    void Start()
    {

        StartCoroutine(GameFlow());
        cameraSwitch.SetToOverview();
    }

    IEnumerator GameFlow()
    {
        //  観察フェーズ
        CurrentState = StageState.Observing ;
        playerController.enabled = false;
        infoText.text = "観察フェーズ";
        SetPlatFormsVisible(true);

        timer = observeDuration;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "残り時間:" + timer.ToString("F0");
            yield return null;
        }

        //  プレイフェーズ
        infoText.text = "プレイフェーズ";
        yield return new WaitForSeconds(prePlayDelay);

        SetPlatFormsVisible(false);
        StartPlayMode();
        timerText.text = "";
    }


    //床の表示設定
    void SetPlatFormsVisible(bool visible)
    {
        foreach (var p in platforms)
        {
            var renderer = p.GetComponent<Renderer>();
            if (renderer != null) renderer.enabled = visible;
        }
    }

    void StartPlayMode()
    {
        CurrentState = StageState.Playing;
        cameraSwitch.EnterThirdPersonMode(); // カメラを切り替える
        playerController.enabled = true;     // プレイヤー操作を有効化
        Debug.Log("プレイ開始！");
    }

    public void OnStageCleared()
    {
        CurrentState = StageState.Cleared;
        infoText.text = "クリア！";
        // 少し待ってからリザルトへ
        StartCoroutine(GoToResultAfterDelay(2f));
    }

    public void OnPlayerFailed()
    {
        if (CurrentState == StageState.Playing)
        {
            CurrentState = StageState.Failed;
            infoText.text = "失敗";

            StartCoroutine(GoToGameOverAfterDelay(2f));
        }
    }

    IEnumerator GoToResultAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneController.instance.GoToResult();
    }

    IEnumerator GoToGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneController.instance.GoToGameOver();
    }
}
