using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class cinemachineLens : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField]private int lensSize = 5;
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.m_Lens.OrthographicSize = lensSize;
    }

}
