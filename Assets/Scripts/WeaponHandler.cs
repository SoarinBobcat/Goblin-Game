using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public float swayIntensityX;
    public float swayIntensityY;
    public float minSway;
    public float maxSway;

    public BobOverride[] bobOverrides;

    public Transform weapon;

    public float currSpd;

    private float currTimeX = 0;
    private float currTimeY = 0;

    private float xPos;
    private float yPos;

    private Vector3 smoothV;

    private void Update()
    {
        foreach (BobOverride bob in bobOverrides)
        {
            if (currSpd >= bob.minSpd && currSpd <= bob.maxSpd)
            {
                float bobMultiplier = (currSpd == 0) ? 1 : currSpd;

                currTimeX += bob.spdX / 10 * Time.deltaTime * bobMultiplier;
                currTimeY += bob.spdY / 10 * Time.deltaTime * bobMultiplier;

                xPos = (bob.bobX.Evaluate(currTimeX) * bob.intensityX);
                yPos = (bob.bobY.Evaluate(currTimeY) * bob.intensityY);
            }
        }

        float xSway = -Input.GetAxis("Mouse X") * swayIntensityX *2;
        float ySway = -Input.GetAxis("Mouse Y") * swayIntensityY *2;

        xSway = Mathf.Clamp(xSway, minSway, maxSway);
        ySway = Mathf.Clamp(ySway, minSway, maxSway);

        xPos += xSway;
        yPos += ySway;
    }

    private void FixedUpdate()
    {
        Vector3 target = new Vector3(xPos, yPos, 0);
        Vector3 desiredPos = Vector3.SmoothDamp(weapon.localPosition, target, ref smoothV, 0.1f);
        weapon.localPosition = desiredPos;
    }

    [System.Serializable]
    public struct BobOverride
    {
        public float minSpd;
        public float maxSpd;

        [Header("X Settings")]
        public float spdX;
        public float intensityX;
        public AnimationCurve bobX;

        [Header("Y Settings")]
        public float spdY;
        public float intensityY;
        public AnimationCurve bobY;
    }
}
