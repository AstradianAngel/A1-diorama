using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float fastSpeedMultiplier = 3f;

    [Header("Mouse Look Settings")]
    public float lookSensitivity = 2f;
    public float maxLookX = 90f;
    public float minLookX = -90f;

    private float rotX;

    void Start()
    {
        // Lock and hide the cursor for FPS-style control
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        CameraLook();
    }

    void Move()
    {
        float speed = moveSpeed;

        // Hold Left Shift to move faster
        if (Input.GetKey(KeyCode.LeftShift))
            speed *= fastSpeedMultiplier;

        // WASD movement
        float x = Input.GetAxisRaw("Horizontal");   // A/D
        float z = Input.GetAxisRaw("Vertical");     // W/S

        // Arrow keys movement
        if (Input.GetKey(KeyCode.LeftArrow)) x = -1;
        if (Input.GetKey(KeyCode.RightArrow)) x = 1;
        if (Input.GetKey(KeyCode.UpArrow)) z = 1;
        if (Input.GetKey(KeyCode.DownArrow)) z = -1;

        Vector3 moveDir = (transform.forward * z + transform.right * x).normalized;

        transform.position += moveDir * speed * Time.deltaTime;
    }

    void CameraLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // Rotate camera up/down
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);

        // Apply rotation
        transform.localRotation = Quaternion.Euler(rotX, transform.localEulerAngles.y + mouseX, 0f);
    }
}

