using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crouch : MonoBehaviour
{
    public CharacterController theCC;
    public GameObject camera;
    public bool isCrouched = false;

    [SerializeField]
    private InputActionReference crouchActionReference;

    // Start is called before the first frame update
    void Start()
    {
        theCC = GetComponent<CharacterController>();
        crouchActionReference.action.performed += OnCrouch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCrouch(InputAction.CallbackContext obj)
    {
        isCrouched = !isCrouched;
    }

    public void Crouched()
    {
        if (isCrouched)
        {
            theCC.height = 1;
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y - 0.5f, camera.transform.position.z);
        }
        else
        {
            theCC.height = 2;
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + 0.5f, camera.transform.position.z);
        }
    }
}
