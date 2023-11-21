using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FlashlightSettings : MonoBehaviour
{
    public bool setting = false;
    public bool lightSetting = false;
    public Light flashlight;
    public GameObject flashlightObject;
    private XRGrabInteractable grabInteractable;
    public bool isPickedUp = false;

    [SerializeField]
    private InputActionReference flashlightActionReference;

    [SerializeField]
    private InputActionReference flashlightSettingActionReference;

    private bool isOn = false;
    private float flashlightTimer = 0f;
    private bool isRecharging = false;
    private float rechargeTime = 5f; // Recharge time in seconds

    // Start is called before the first frame update
    void Start()
    {
        flashlightActionReference.action.performed += OnFlashlight;
        flashlightSettingActionReference.action.performed += OnFlashlightSetting;
    }

    // Update is called once per frame
    void Update()
    {
        flashlightObject.SetActive(setting);

        if (isPickedUp && isOn)
        {
            flashlightTimer += Time.deltaTime;

            if (flashlightTimer >= 10f) // Turn off after 10 seconds
            {
                TurnOffFlashlight();
            }
        }

        if (isPickedUp)
        {
            if (!lightSetting)
            {
                flashlight.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                flashlight.color = new Color(0.325f, 0.0f, 1.0f, 1.0f);
            }
        }
    }

    private void OnFlashlight(InputAction.CallbackContext obj)
    {
        if (isPickedUp)
        {
            setting = !setting;
            if (setting)
            {
                isOn = true;
                flashlight.enabled = true;
                flashlightTimer = 0f;
            }
            else
            {
                TurnOffFlashlight();
            }
        }
    }

    private void TurnOffFlashlight()
    {
        isOn = false;
        flashlight.enabled = false;
        flashlightTimer = 0f;
        StartCoroutine(RechargeFlashlight());
    }

    private void OnFlashlightSetting(InputAction.CallbackContext obj)
    {
        if (isPickedUp && !isRecharging)
        {
            lightSetting = !lightSetting;
        }
    }

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnObjectPickedUp);
            grabInteractable.selectExited.AddListener(OnObjectReleased);
        }
    }

    private void OnObjectPickedUp(SelectEnterEventArgs args)
    {
        isPickedUp = true;
    }

    private void OnObjectReleased(SelectExitEventArgs args)
    {
        isPickedUp = false;
    }

    private IEnumerator RechargeFlashlight()
    {
        if (!isRecharging)
        {
            isRecharging = true;
            yield return new WaitForSeconds(rechargeTime);
            isRecharging = false;
        }
    }
}
