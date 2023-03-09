using Game.Managers;
using UnityEngine;

[ExecuteAlways]
public class CameraManager : MonoBehaviour
{
    public static CameraManager _cameraManager;
    [SerializeField] private Camera _mainCam;
  
    private Vector2Int _mapSize;
    private GridManager _gridManager;
    private float _screenAspect;
    private float _camOrtoSize;

    void Awake()
    {
        _cameraManager = this;
    }

    private void Start()
    {
        _camOrtoSize = _mainCam.orthographicSize;
        _screenAspect = (float) Screen.width / (float) Screen.height;
        
        _gridManager = GridManager.Instance; // TODO RACING CONDITIONS!
        var currentGridData = _gridManager.GetCurrentGridData();
        _mapSize = currentGridData.GridSize;
    } 

    public Camera GetMainCam()
    {
        return _mainCam;
    }

    public void SetCameraPosition(Vector2 cameraPos)
    {
        // _mainCam.transform.position = new Vector3(cameraPos.x, cameraPos.y, -15f);
        _mainCam.transform.position = ApplyCameraBounds(cameraPos);
    }

    private Vector3 ApplyCameraBounds(Vector3 rawPos) ///todo THIS ONLY WORKS WITH CAM SIZE 10
    {
        var halfOfOrto = _camOrtoSize * 0.5f;
        var multipler = halfOfOrto / _screenAspect;

        var maxX = _mapSize.x - (_mapSize.x / multipler) - 0.5f;
        var minX = _mapSize.x / multipler - 0.5f;
        
        var maxY = _mapSize.y - (_mapSize.y / halfOfOrto) - 0.5f;
        var minY = (_mapSize.y / halfOfOrto) - 0.5f;

        return new Vector3(Mathf.Clamp(rawPos.x, minX, maxX), Mathf.Clamp(rawPos.y, minY, maxY), -10);
    }
}
