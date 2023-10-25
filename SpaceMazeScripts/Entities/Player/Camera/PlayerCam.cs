using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] Transform playerTsf, cameraTsf;

    static Vector2 mouseInput;

    float XRotation = 0;
    const float X_CLAMP = 80;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CamRotate();
    }

    void CamRotate()
    {
        mouseInput.x = Input.GetAxis("Mouse X");
        mouseInput.y = Input.GetAxis("Mouse Y");

        playerTsf.Rotate(Vector3.up, mouseInput.x * Time.deltaTime * GameSettingData.instance.camSensitivity);

        XRotation -= mouseInput.y * Time.deltaTime * GameSettingData.instance.camSensitivity;
        XRotation = Mathf.Clamp(XRotation, -X_CLAMP, X_CLAMP);

        cameraTsf.localEulerAngles = new Vector3(XRotation, 0, 0);
    }

}
