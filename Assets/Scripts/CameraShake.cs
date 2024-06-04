using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineFreeLook cameraFreeLook;
    [SerializeField]private CinemachineBasicMultiChannelPerlin[] shakeRigs = new CinemachineBasicMultiChannelPerlin[3];
    private const int shakeRigCount = 3;
    private float shakeTimer = 0f, startingIntensity = 0f, shakeTimerTotal = 0f;
    public static CameraShake Instance{get; private set;}
    // Start is called before the first frame update
    void Start()
    {
        cameraFreeLook = GetComponent<CinemachineFreeLook>();
        for(int i = 0; i < shakeRigCount; i++)
        {
            shakeRigs[i] = cameraFreeLook.GetRig(i).GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
        }
        Instance = this;
    }

    public void ShakeCamera(float intensity, float timer)
    {
        for(int i = 0; i < shakeRigCount; i++)
        {
            startingIntensity = intensity;
            shakeRigs[i].m_AmplitudeGain = startingIntensity;
            shakeTimer = timer;
            shakeTimerTotal = timer;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0f)
        {
            for (int i = 0; i < shakeRigCount; i++)
            {
                shakeRigs[i].m_AmplitudeGain = Mathf.Lerp(startingIntensity,0f,(1 - shakeTimer/shakeTimerTotal));
            }
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                shakeTimer = 0f;
                for (int i = 0; i < shakeRigCount; i++)
                {
                    shakeRigs[i].m_AmplitudeGain = 0f;
                }
            }
        }

        // if (Input.GetKeyUp("b"))
        // {
        //     ShakeCamera(3f,0.5f);
        // }
    }
}
