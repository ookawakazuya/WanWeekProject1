using UnityEngine;

public class DisappearOnPlay : MonoBehaviour
{
    Collider collider;
    Renderer render;
    StageManager stageManager;
    
    void Start()
    {
        collider = GetComponent<Collider>();
        render = GetComponent<Renderer>();
        stageManager = FindObjectOfType<StageManager>();

        //�����t�F�[�Y�ł͕\��
        UpdateVisibittity();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVisibittity();
    }

    void UpdateVisibittity()
    {
        if(stageManager == null) return;

        if(stageManager.CurrentState == StageState.Observing)
        {
            //�ώ@���͕͂\��&�����蔻�肠��
            if (render) render.enabled = true;
            if (collider) collider.enabled = true;
        }
        else
        {
            //�v���C���ȍ~�͔�\��&�����蔻��Ȃ�
            if (render) render.enabled = false;
            if (collider) collider.enabled = false;
        }

    }
}
