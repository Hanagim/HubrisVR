using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    public InputActionAsset inputActions;
    public string actionMapName = "XRI RightHand"; // Change if needed
    public string bActionName = "BButtonHold";
    public string mainMenuScene = "MainMenu";
    public float holdTime = 2f;

    private InputAction bButtonAction;
    private float holdTimer = 0f;
    private bool returning = false;

    void OnEnable()
    {
        var map = inputActions.FindActionMap(actionMapName);
        bButtonAction = map.FindAction(bActionName);
        bButtonAction.Enable();
    }

    void OnDisable()
    {
        bButtonAction?.Disable();
    }

    void Update()
    {
        if (returning || bButtonAction == null) return;

        if (bButtonAction.IsPressed())
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= holdTime)
            {
                returning = true;
                Debug.Log("Returning to Main Menu...");
                SceneManager.LoadScene(mainMenuScene);
            }
        }
        else
        {
            holdTimer = 0f;
        }
    }

    public void ReturnToMainMenuFunction()
    {
        Debug.Log("Timeline signal received — loading MainMenu.");
        SceneManager.LoadScene("MainMenu");
    }
}
