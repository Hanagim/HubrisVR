using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WheelPuzzle : MonoBehaviour
{

    public Transform wheel1;
    public Transform wheel2;
    public Transform wheel3;

    public float wheel1ZRotation;
    public float wheel2ZRotation;
    public float wheel3ZRotation;

    public TextMeshProUGUI wheelText;

    // Update is called once per frame
    void Update()
    {
        var currentRotation = wheel1.localEulerAngles.z.ToString() + ", " + wheel2.localEulerAngles.z.ToString() + ", " + wheel3.localEulerAngles.z.ToString();
        wheelText.SetText(currentRotation);
    }
}
