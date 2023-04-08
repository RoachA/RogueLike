using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Light2D))]
public class PointLightView : MonoBehaviour
{
    [SerializeField] private Light2D _lightObj;
    [SerializeField] private PointLightData _lightData;

    private Sequence _seq;

    private void Start()
    {
        _lightObj.color = _lightData.LightColor;
        _lightObj.pointLightOuterRadius = _lightData.Range;
        _lightObj.intensity = _lightData.Intensity;
        _lightObj.enabled = _lightData.ActiveOnAwake;

        if (_lightData.HasBlinks)
            StartBlinks();
    }

    public void SetLightParameters(PointLightData lightData)
    {
        _lightObj.color = lightData.LightColor;
        _lightObj.pointLightOuterRadius = lightData.Range;
        _lightObj.intensity = lightData.Intensity;
    }

    public void SetLightState(bool state)
    {
        _lightObj.enabled = state;
    }

    public void StartBlinks()
    {
        float a = 0;
        _seq?.Kill(true);
        _seq = DOTween.Sequence().SetLink(gameObject).SetLoops(-1, LoopType.Yoyo);

        _seq.Append(DOTween.To(x => a = x, 0f, 1f, 0.25f)
            .OnStepComplete(() => _lightObj.intensity = Random.Range(0.25f, 2f)));
    }

    public void StopBlinks()
    {
        _seq?.Kill(true);
    }

    [Serializable]
    public struct PointLightData
    {
        public Color LightColor;
        public float Range;
        public float Intensity;
        public bool ActiveOnAwake;
        public bool HasBlinks;

        public PointLightData(Color lightColor, float range, float intensity, bool hasBlinks, bool activeOnAwake)
        {
            LightColor = lightColor;
            Range = range;
            Intensity = intensity;
            HasBlinks = hasBlinks;
            ActiveOnAwake = activeOnAwake;
        }
    }
}
