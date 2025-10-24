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

        //初期フェーズでは表示
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
            //観察中はは表示&当たり判定あり
            if (render) render.enabled = true;
            if (collider) collider.enabled = true;
        }
        else
        {
            //プレイ中以降は非表示&当たり判定なし
            if (render) render.enabled = false;
            if (collider) collider.enabled = false;
        }

    }
}
