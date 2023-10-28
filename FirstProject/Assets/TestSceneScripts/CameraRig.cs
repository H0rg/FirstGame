using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public float m_Sensitivity = 2;


    private float m_InputMouseX = 0f;
    private float m_InputMouseY = 0f;

    private bool m_Rotate = false;

    private void GetInput()
    {
        m_Rotate = Input.GetMouseButton(1);
    }

    private void Update()
    {
        GetInput();

        if (m_Rotate)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            m_InputMouseX += Input.GetAxis("Mouse X") * m_Sensitivity;
            m_InputMouseY += Input.GetAxis("Mouse Y") * m_Sensitivity;

            m_InputMouseY = Mathf.Clamp(m_InputMouseY, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(m_InputMouseX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(m_InputMouseY, Vector3.left);
        }else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
}
