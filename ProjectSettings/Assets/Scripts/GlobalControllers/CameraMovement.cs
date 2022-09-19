using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public float panSpeed = 10f;

    [Header("Camera Bounds")]
    public float northBound = 0f;
    public float southBound = 0f;
    public float eastBound = 0f;
    public float westBound = 0f;

    [Header("Other")]
    public float cameraBuffer = 10f;

    void Update()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - cameraBuffer)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("s") || Input.mousePosition.y <= cameraBuffer)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("a") || Input.mousePosition.x <= cameraBuffer)
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - cameraBuffer)
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
    }
}
