using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightSettings : MonoBehaviour
{
    public bool setting = false;
    public bool lightSetting = true;
    public Light flashlight;
    public bool selected;

    [SerializeField]
    private InputActionReference flashlightActionReference;

    [SerializeField]
    private InputActionReference flashlightSettingActionReference;

    // Start is called before the first frame update
    void Start()
    {
        flashlight = GetComponent<Light>();
        flashlightActionReference.action.performed += OnFlashlight;
        flashlightSettingActionReference.action.performed -= OnFlashlightSetting;
    }

    // Update is called once per frame
    void Update()
    {
        if (setting)
        {
            flashlight.intensity = 25;
        }
        else
        {
            flashlight.intensity = 0;
        }
        if (lightSetting)
        {
            flashlight.color = Color.white;
            flashlight.intensity = 25;
        }
        else
        {
            flashlight.color = new Color(83, 0, 255);
        }
    }

    private void OnFlashlight(InputAction.CallbackContext obj)
    {
        setting = !setting;
    }

    private void OnFlashlightSetting(InputAction.CallbackContext obj)
    {
        lightSetting = !lightSetting;
    }
}
