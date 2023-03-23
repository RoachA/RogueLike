using System.Numerics;
using UnityEngine;
using DG.Tweening;
using Vector3 = UnityEngine.Vector3;

public class DynamicEntityFeedbacks : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _entityTransform;

    [Header("ParticleSystems")]
    [SerializeField] private ParticleSystem _bloodSplatter_normal;
    [SerializeField] private ParticleSystem _bloodSplatter_critical;

    private Sequence _seq;

    public void AttackActionAnim(Vector3 direction)
    {
        _seq?.Kill(true);
        _seq = DOTween.Sequence().SetLink(gameObject);
        _seq.Append(_entityTransform.DOMove(direction, 0.1f).SetEase(Ease.InBack));
        _seq.Append(_entityTransform.DOLocalMove(Vector3.zero, 0.1f).SetEase(Ease.OutBack));
    }

    public void OnReceiveDamage(bool isCritical = false)
    {
        _bloodSplatter_normal.Play(true);
        
        _seq?.Kill(true);
        _seq = DOTween.Sequence().SetLink(gameObject);

        _seq.Append(_entityTransform.DOPunchScale(Vector3.one * -0.25f, 0.4f, 3, 1));
        
        if (isCritical)
            _bloodSplatter_critical.Play(true);
    }
}
