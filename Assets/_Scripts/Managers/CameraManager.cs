using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager _cameraManager;
    [SerializeField] private Camera _mainCam;

    void Awake()
    {
        _cameraManager = this;
    }

    public Camera GetMainCam()
    {
        return _mainCam;
    }

    public void SetCameraPosition(Vector2 cameraPos)
    {
        _mainCam.transform.position = new Vector3(cameraPos.x, cameraPos.y, -15f);
    }
}
