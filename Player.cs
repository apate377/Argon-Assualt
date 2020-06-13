using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip ("In m/s")][SerializeField] float xSpeed = 4f;
    [Tooltip("In m/s")] [SerializeField] float ySpeed = 4f;
    [Tooltip("In m")] [SerializeField] float horizontalClamp = 10f;
    [Tooltip("In m")] [SerializeField] float verticalMax = 10f;
    [Tooltip("In m")] [SerializeField] float verticalMin = -10f;
    [SerializeField] GameObject[] guns;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -5f;

    [SerializeField] float positionYawFactor = -5f;

    [SerializeField] float controlRollFactor = -5f;


    float horizontalThrow, verticalThrow;
    bool isControlEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
        CalculateHorizontalMovement();
        CalculateVerticalMovement();
        CalculateRotation();
        ProcessFiring();
        }
    }

    void OnPlayerDeath() //called by string reference
    {
        print("freeze controls");
        isControlEnabled = false;
    }

    private void CalculateHorizontalMovement()
    {
        horizontalThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = horizontalThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        float NewXPos = Mathf.Clamp(rawNewXPos, -horizontalClamp, horizontalClamp);
        transform.localPosition = new Vector3(NewXPos, transform.localPosition.y, transform.localPosition.z);
    }
    private void CalculateVerticalMovement()
    {
        verticalThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = verticalThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        float NewYPos = Mathf.Clamp(rawNewYPos, verticalMin, verticalMax);
        transform.localPosition = new Vector3(transform.localPosition.x, NewYPos, transform.localPosition.z);
    }
    private void CalculateRotation() {
        float pitch = transform.localPosition.y * positionPitchFactor + verticalThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = horizontalThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }

    }
    private void SetGunsActive(bool isActive)
    {
        foreach(GameObject gun in guns)
        {
            var emmissionModule = gun.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = isActive;
        }
    }
  

}
