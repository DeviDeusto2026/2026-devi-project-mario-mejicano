using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform playerBody;

    float xRotation = 0f;
    float yRotation = 0f;

    bool paused = false;

    public void setPaused(bool paused)
    {
        this.paused = paused;
    }

    void Update()
    {
        if (!paused)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            yRotation += mouseX;

            
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            
            playerBody.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}

