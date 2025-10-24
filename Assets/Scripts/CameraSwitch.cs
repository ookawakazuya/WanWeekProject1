using System.Xml.Serialization;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [Header("プレイヤー設定")]
    [SerializeField]Transform player;

    [Header("俯瞰カメラ設定")]
    [SerializeField]Vector3 overviewPosition ;      //俯瞰視点カメラの位置
    [SerializeField] Vector3 overviewRotation ;     //俯瞰視点カメラの角度

    [Header("")]
    [SerializeField] Vector3 thirdPersonOffset = new Vector3(0, 3.5f, -4f);
    [SerializeField] Vector3 thirdPersonRotation = new Vector3(45, 0, 0);
    [SerializeField] float followSmooth = 5f;       //カメラの追従速度
    bool isOverview = true;


    void Start()
    {
        SetToOverview();
    }


    /// <summary>
    /// 観察フェーズ用カメラ
    /// </summary>
   public  void SetToOverview()
    {
        //ゲーム開始時は俯瞰視点
        transform.position = overviewPosition;
        transform.rotation = Quaternion.Euler(overviewRotation);
        isOverview = true;
    }

    //StageManagerから発動
    /// <summary>
    /// 三人称視点モードへ変更
    /// </summary>
    public void EnterThirdPersonMode()
    {
        UpdatethirdPresonCamera();
        isOverview = false;
    }

    void LateUpdate()
    {
        if (!isOverview && player != null)
        {
            UpdatethirdPresonCamera();
        }
    }

    /// <summary>
    /// プレイヤーの背後にカメラを固定
    /// </summary>
    void UpdatethirdPresonCamera()
    {
        // プレイヤーの背後に固定配置
        transform.position = player.position + player.TransformDirection(thirdPersonOffset);
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
