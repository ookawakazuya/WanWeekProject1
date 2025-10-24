using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class FadingPlatform : MonoBehaviour
{
    [Header("�t�F�[�h�ݒ�")]
    [SerializeField] float visibleDuration = 3f;    //���S�Ɍ����鎞��
    [SerializeField] float invibleDuration = 3f;    //���S�ɏ����鎞��
    [SerializeField] float fadeSpeed = 3f;          //�t�F�[�h�̑���

    StageManager stageManager;
    Renderer renderer;
    Collider collider;
    Material fadeMaterial;

   [SerializeField] Color baseColor;

    float fadeValue = 1;     //1���\��,0������
    [SerializeField]bool isVisible = true;
    float timer;

    void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();

        fadeMaterial = renderer.material;
        Color colTemp = fadeMaterial.color;
        colTemp.a = 1f;
        fadeMaterial.color = colTemp;

        timer = visibleDuration;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (isVisible && timer <= 0f)
        {
            // ������^�C�~���O
            StartCoroutine(FadePlatform(false));
            isVisible = false;
            timer = invibleDuration;
        }
        else if (!isVisible && timer <= 0f)
        {
            // �����^�C�~���O
            StartCoroutine(FadePlatform(true));
            isVisible = true;
            timer = visibleDuration;
        }
    }

    private System.Collections.IEnumerator FadePlatform(bool show)
    {
        float targetAlpha = show ? 1f : 0f;
        float currentAlpha = fadeMaterial.color.a;
        collider.enabled = show;

        while (Mathf.Abs(currentAlpha - targetAlpha) > 0.01f)
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
            Color c = baseColor;
            c.a = currentAlpha;
            fadeMaterial.color = c;
            yield return null;
        }

        Color finalColor = baseColor;
        finalColor.a = targetAlpha;
        fadeMaterial.color = finalColor;
        collider.enabled = show;
    }
}
