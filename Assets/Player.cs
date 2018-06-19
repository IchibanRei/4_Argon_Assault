using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 12f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    [SerializeField] float positionPitchFactor = -5;
    [SerializeField] float controlPitchFactor = -20;
    [SerializeField] float positionYawFactor = 5;
    [SerializeField] float controlRollFactor = -20;

    float xThrow, yThrow;
    // Use this for initialization
    void Start () {
		
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
	
	// Update is called once per frame
	void Update () {
        #region Horizontal
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        var xOffset = xThrow * xSpeed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        #endregion

        #region Vertial
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        var yOffset = yThrow * xSpeed * Time.deltaTime;

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        #endregion
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        ProcessRotation();

    }
}
