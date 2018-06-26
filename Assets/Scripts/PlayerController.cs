using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {
    // todo workout speed on start.

    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float controlSpeed = 12f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [Header("Screen-Position Based")]
    [SerializeField] float positionPitchFactor = -5;
    [SerializeField] float controlPitchFactor = -20;

    [Header("Control-Throw Based")]
    [SerializeField] float positionYawFactor = 5;
    [SerializeField] float controlRollFactor = -20;

    float xThrow, yThrow;
    bool isControlEnabled = true;
    // Use this for initialization
    void Start() {

    }

    void ProcessTransform()
    {
        #region Horizontal
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        var xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        #endregion

        #region Vertial
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        var yOffset = yThrow * controlSpeed * Time.deltaTime;

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        #endregion
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void OnPlayerDeath() //called by string reference
    {
        isControlEnabled = false;
        print("Controls Frozen");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isControlEnabled)
        {
            ProcessTransform();
            ProcessRotation();
        }
    }
}
