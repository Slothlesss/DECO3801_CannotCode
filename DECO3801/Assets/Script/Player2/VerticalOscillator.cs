using UnityEngine;

public class VerticalOscillator : MonoBehaviour
{
    public float speed = 2f;
    public float height = 1f;

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Vector3 offset = new Vector3(0, Mathf.Sin(Time.time * speed) * height, 0);
            transform.position = GetRightEdgePosition() + offset;
        }
    }

    private Vector3 GetRightEdgePosition()
    {
        Camera cam = Camera.main;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float rightX = cam.transform.position.x + camWidth / 2 - 1.5f;
        float centerY = cam.transform.position.y + 1.15f;

        return new Vector3(rightX, centerY, 0);
    }
}
