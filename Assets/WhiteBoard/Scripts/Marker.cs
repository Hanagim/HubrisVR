using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [Header("Marker Settings")]
    [SerializeField] private Transform tip;
    [SerializeField] private int penSize = 5;

    // Internal state
    private Renderer tipRenderer;
    private Color[] penColors;
    private float tipHeight;

    // Touch state
    private RaycastHit touchInfo;
    private WhiteBoard whiteboard;
    private Vector2 touchUV, lastTouchUV;
    private bool hasTouchedLastFrame;
    private Quaternion lastTouchRotation;

    void Start()
    {
        if (tip == null)
        {
            Debug.LogError("Marker tip not assigned.");
            enabled = false;
            return;
        }

        tipRenderer = tip.GetComponent<Renderer>();
        if (tipRenderer == null)
        {
            Debug.LogError("Marker tip is missing a Renderer component.");
            enabled = false;
            return;
        }

        penColors = Enumerable.Repeat(tipRenderer.material.color, penSize * penSize).ToArray();
        tipHeight = tip.localScale.y;
    }

    void Update()
    {
        TryDraw();
    }

    private void TryDraw()
    {
        if (Physics.Raycast(tip.position, transform.up, out touchInfo, tipHeight))
        {
            if (touchInfo.transform.CompareTag("Whiteboard"))
            {
                HandleTouchOnWhiteboard();
                return;
            }
        }

        // Reset if not touching a whiteboard
        whiteboard = null;
        hasTouchedLastFrame = false;
    }

    private void HandleTouchOnWhiteboard()
    {
        if (whiteboard == null)
        {
            whiteboard = touchInfo.transform.GetComponent<WhiteBoard>();
            if (whiteboard == null)
            {
                Debug.LogWarning("Whiteboard tag found but no WhiteBoard component.");
                return;
            }
        }

        touchUV = touchInfo.textureCoord;

        int x = Mathf.Clamp((int)(touchUV.x * whiteboard.textureSize.x - (penSize / 2)), 0, (int)whiteboard.textureSize.x);
        int y = Mathf.Clamp((int)(touchUV.y * whiteboard.textureSize.y - (penSize / 2)), 0, (int)whiteboard.textureSize.y);

        if (hasTouchedLastFrame)
        {
            whiteboard.texture.SetPixels(x, y, penSize, penSize, penColors);

            // Interpolate between previous and current touch for smooth drawing
            for (float t = 0.01f; t < 1f; t += 0.01f)
            {
                int lerpX = (int)Mathf.Lerp(lastTouchUV.x, x, t);
                int lerpY = (int)Mathf.Lerp(lastTouchUV.y, y, t);
                whiteboard.texture.SetPixels(lerpX, lerpY, penSize, penSize, penColors);
            }

            transform.rotation = lastTouchRotation;
            whiteboard.texture.Apply();
        }

        lastTouchUV = new Vector2(x, y);
        lastTouchRotation = transform.rotation;
        hasTouchedLastFrame = true;
    }
}
