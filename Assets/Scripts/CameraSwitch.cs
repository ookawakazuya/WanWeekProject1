using System.Xml.Serialization;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [Header("�v���C���[�ݒ�")]
    [SerializeField]Transform player;

    [Header("���ՃJ�����ݒ�")]
    [SerializeField]Vector3 overviewPosition ;      //���Վ��_�J�����̈ʒu
    [SerializeField] Vector3 overviewRotation ;     //���Վ��_�J�����̊p�x

    [Header("")]
    [SerializeField] Vector3 thirdPersonOffset = new Vector3(0, 3.5f, -4f);
    [SerializeField] Vector3 thirdPersonRotation = new Vector3(45, 0, 0);
    [SerializeField] float followSmooth = 5f;       //�J�����̒Ǐ]���x
    bool isOverview = true;


    void Start()
    {
        SetToOverview();
    }


    /// <summary>
    /// �ώ@�t�F�[�Y�p�J����
    /// </summary>
   public  void SetToOverview()
    {
        //�Q�[���J�n���͘��Վ��_
        transform.position = overviewPosition;
        transform.rotation = Quaternion.Euler(overviewRotation);
        isOverview = true;
    }

    //StageManager���甭��
    /// <summary>
    /// �O�l�̎��_���[�h�֕ύX
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
    /// �v���C���[�̔w��ɃJ�������Œ�
    /// </summary>
    void UpdatethirdPresonCamera()
    {
        // �v���C���[�̔w��ɌŒ�z�u
        transform.position = player.position + player.TransformDirection(thirdPersonOffset);
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
