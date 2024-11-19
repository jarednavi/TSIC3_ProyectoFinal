using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class PetControl : MonoBehaviour
{
    private float currScale;
    private float scaleMax = 5f;
    private float scaleMin = 1f;
    private Animator anim;
    private Rigidbody rb;
    private int speedHash = Animator.StringToHash("speed");
    public ARRaycastManager raycastManager;
    void Awake()
    {
        Joystick.JoystickMoved += UpdateMove;
    }
    void OnDestroy()
    {
        Joystick.JoystickMoved -= UpdateMove;
    }
    void Start()
    {
        currScale = Mathf.Clamp(transform.localScale.x, scaleMin, scaleMax);
        anim = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody>();
    }
    public void SetPosition()
    {
        var screenPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
        List<ARRaycastHit> hitResults = new List<ARRaycastHit>();
        if (raycastManager.Raycast(screenPosition, hitResults, TrackableType.PlaneWithinBounds))
        {
            var hitPose = hitResults[0].pose;
            transform.position = hitPose.position;
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
    public void Jump()
    {
        rb.AddForce(Vector3.up * 140f);
    }
    public void UpScale()
    {
        if (Mathf.Approximately(currScale, scaleMax)) return;
        currScale += 1f;
        transform.localScale = new Vector3(currScale, currScale, currScale);
    }
    public void DownScale()
    {
        if (Mathf.Approximately(currScale, scaleMin)) return;
        currScale -= 1f;
        transform.localScale = new Vector3(currScale, currScale, currScale);
    }
    private void UpdateMove(Vector2 input)
    {
        if (input.Equals(Vector2.zero))
        {
            anim.SetFloat(speedHash, 0f);
            return;
        }
        Vector3 inputAxes = new Vector3(input.x, 0, input.y);
        anim.SetFloat(speedHash, inputAxes.magnitude);
        SetLookDirection(inputAxes);
        transform.localPosition += (transform.forward * inputAxes.magnitude * Time.deltaTime);
    }
    void SetLookDirection(Vector3 inputAxes)
    {
        Quaternion yRotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        Vector3 lookDirection = (yRotation * inputAxes).normalized;
        transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}