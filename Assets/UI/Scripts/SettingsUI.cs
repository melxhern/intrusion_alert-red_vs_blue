using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private Button MouseControllButton;
    [SerializeField]
    private Button KeyboardMouseControllButton;
    [SerializeField]
    private Button JoyStickButton;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        switch (PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                MouseControllButton.image.color = Color.green;
                KeyboardMouseControllButton.image.color = Color.white;
                JoyStickButton.image.color = Color.white;
                break;

            case EControlType.KeyboardMouse:
                KeyboardMouseControllButton.image.color = Color.green;
                MouseControllButton.image.color = Color.white;
                JoyStickButton.image.color = Color.white;
                break;

            case EControlType.JoyStick:
                JoyStickButton.image.color = Color.green;
                MouseControllButton.image.color = Color.white;
                KeyboardMouseControllButton.image.color = Color.white;
                break;
        }
    }

    public void SetControllMode(int controlType)
    {
        PlayerSettings.controlType = (EControlType)controlType;
        switch (PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                MouseControllButton.image.color = Color.green;
                KeyboardMouseControllButton.image.color = Color.white;
                JoyStickButton.image.color = Color.white;
                break;

            case EControlType.KeyboardMouse:
                KeyboardMouseControllButton.image.color = Color.green;
                MouseControllButton.image.color = Color.white;
                JoyStickButton.image.color = Color.white;
                break;

            case EControlType.JoyStick:
                JoyStickButton.image.color = Color.green;
                MouseControllButton.image.color = Color.white;
                KeyboardMouseControllButton.image.color = Color.white;
                break;
        }
    }

    public virtual void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");
    }
}
