using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ReturnToMainMenu : MonoBehaviour
{
    [Tooltip("Name of the Main Menu scene.")]
    public string mainMenuSceneName = "MainMenu";

    [Tooltip("Hold duration in seconds to trigger return.")]
    public float holdDuration = 2f;

    private float bButtonHoldTime = 0f;
    private bool returning = false;

    void Update()
    {
        if (returning) return;

        bool isBPressed = false;

#if ENABLE_INPUT_SYSTEM
        // Using Unity's new Input System
        var rightHand = XRController.rightHand;
        if (Gamepad.current != null)
        {
            // Fallback for gamepad (Quest via Link)
            isBPressed = Gamepad.current.buttonEast.isPressed;
        }
        else if (OculusTouchController.rightHand != null)
        {
            isBPressed = OculusTouchController.rightHand.buttonB.isPressed;
        }
#else
        // Using old input system fallback
        isBPressed = Input.GetKey(KeyCode.JoystickButton1); // B button on right controller
#endif

        if (isBPressed)
        {
            bButtonHoldTime += Time.deltaTime;
            if (bButtonHoldTime >= holdDuration)
            {
                ReturnToMenu();
            }
        }
        else
        {
            bButtonHoldTime = 0f;
        }
    }

    public void ReturnToMenu()
    {
        if (returning) return;
        returning = true;

        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
