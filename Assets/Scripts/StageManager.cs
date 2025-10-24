using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum StageState { Observing,Playing,Cleared,Failed}

public class StageManager : MonoBehaviour
{
    [Header("����̐ݒ�")]
    public List<GameObject> platforms;

    [Header("�ώ@����")]
    public float observeDuration = 3f;

    [Header("�ώ@�I����̗P�\����")]
    public float prePlayDelay = 1.0f;

    [Header("UI�\��")]
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
        //  �ώ@�t�F�[�Y
        CurrentState = StageState.Observing ;
        playerController.enabled = false;
        infoText.text = "�ώ@�t�F�[�Y";
        SetPlatFormsVisible(true);

        timer = observeDuration;
        while(timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "�c�莞��:" + timer.ToString("F0");
            yield return null;
        }

        //  �v���C�t�F�[�Y
        infoText.text = "�v���C�t�F�[�Y";
        yield return new WaitForSeconds(prePlayDelay);

        SetPlatFormsVisible(false);
        StartPlayMode();
        timerText.text = "";
    }


    //���̕\���ݒ�
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
        cameraSwitch.EnterThirdPersonMode(); // �J������؂�ւ���
        playerController.enabled = true;     // �v���C���[�����L����
        Debug.Log("�v���C�J�n�I");
    }

    public void OnStageCleared()
    {
        CurrentState = StageState.Cleared;
        infoText.text = "�N���A�I";
        // �����҂��Ă��烊�U���g��
        StartCoroutine(GoToResultAfterDelay(2f));
    }

    public void OnPlayerFailed()
    {
        if (CurrentState == StageState.Playing)
        {
            CurrentState = StageState.Failed;
            infoText.text = "���s";

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
