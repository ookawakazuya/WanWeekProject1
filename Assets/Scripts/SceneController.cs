using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneController : MonoBehaviour
{
    [Header("�V�[���ݒ�")]
    [Tooltip("�^�C�g���V�[����")]
    public string titleScene = "";

    [Tooltip("���C���Q�[���V�[����")]
    public string mainScene = "";

    [Tooltip("���U���g�V�[����")]
    public string resultScene = "";

    [Tooltip("�Q�[���I�[�o�[�V�[����")]
    public string gameOverScene = "";

    public static SceneController instance { get; private set; }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // �V�[�����܂����ł��ێ�
    }
    public void GoToTitle()
    {
        LoadScene(titleScene);
    }

    public void StartGame()
    {
        LoadScene(mainScene);
    }

    public void GoToResult()
    {
        LoadScene(resultScene);
    }

    public void GoToGameOver()
    {
        LoadScene(gameOverScene);
    }

    public void RetryGame()
    {
        LoadScene(mainScene);
    }

    public void QuitGame()
    {
        Debug.Log("�A�v�����I�����܂�");
        Application.Quit();
    }


    // ���ۂ̓ǂݍ��ݏ���
    private void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogWarning("�V�[�������ݒ肳��Ă��܂���I");
            return;
        }

        Debug.Log($"�V�[�������[�h: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }


}
