using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class WheelPuzzle : MonoBehaviour
{

    public Transform wheel1;
    public Transform wheel2;
    public Transform wheel3;

    public Renderer targetRenderer; // assign in Inspector
    public float fillSmoothness = 0.01f;
    public UnityEngine.Color solvedColor = UnityEngine.Color.green;
    public UnityEngine.Color defaultColor = UnityEngine.Color.red;

    private Material materialInstance;

    public float wheel1ZRotation;
    public float wheel2ZRotation;
    public float wheel3ZRotation;

    public TextMeshProUGUI wheelText;

    void Start()
    {
        // Get a unique instance of the material so changes don't affect others
        materialInstance = targetRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {

        float redAngle = wheel1.localEulerAngles.z;
        float greenAngle = wheel2.localEulerAngles.z;
        float blueAngle = wheel3.localEulerAngles.z;

        int wheel1Z = Mathf.RoundToInt(wheel1.localEulerAngles.z);
        int wheel2Z = Mathf.RoundToInt(wheel2.localEulerAngles.z);
        int wheel3Z = Mathf.RoundToInt(wheel3.localEulerAngles.z);

        string currentRotation = $"{wheel1Z}, {wheel2Z}, {wheel3Z}";
        wheelText.SetText(currentRotation);

        float fill = ((wheel1Z + wheel2Z + wheel3Z) / 3f) / 360f;
        materialInstance.SetFloat("Fill", Mathf.Clamp01(fill));

        float r = Mathf.Clamp01(redAngle / 360f);
        float g = Mathf.Clamp01(greenAngle / 360f);
        float b = Mathf.Clamp01(blueAngle / 360f);

        // Optionally make HDR brighter with intensity multiplier (e.g., 2.0f)
        float intensity = 1.0f;
        UnityEngine.Color sideColor = new UnityEngine.Color(r, g, b) * intensity;

        // Set shader property
        materialInstance.SetColor("_SideColor", sideColor);
    }
}
