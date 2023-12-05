using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ItemsMagnit : MonoBehaviour
{
    [SerializeField] private float _attractDuration = 1f;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _followOffsetDistance = 5f;
    [SerializeField] private float _shakeScalingDuration = 0.2f;
    [SerializeField] private float _shakeScalingValue = 3f;
    [SerializeField] private float _scaleReduceDuration = 0.5f;
    [SerializeField] private float _scaleReduceMoveSpeed = 5;

    private float _followRange => _followOffsetDistance * _followOffsetDistance;

    public void Attract(Item item)
    {
        item.DisableCollision();
        StartCoroutine(Animate(item));
    }

    public void AttractLinear(Item dollar)
    {
        dollar.transform.DOComplete(true);

        dollar.transform.DOLocalRotate(Vector3.zero, 0.05f);
        dollar.transform.DOLocalMove(transform.position, 0.05f).OnComplete(() =>
        {
            Destroy(dollar.gameObject);
        });
    }

    private IEnumerator Animate(Item item)
    {
        item.transform.DOComplete(true);
        item.transform.DOShakeScale(_shakeScalingDuration, _shakeScalingValue);

        yield return new WaitForSeconds(_shakeScalingDuration);

        // reset the rotate param and wait for attract
        item.transform.DOLocalRotate(Vector3.zero, _attractDuration);

        while (Vector3.SqrMagnitude(transform.position - item.transform.position) > _followRange)
        {
            float clampedSpeed = Mathf.Clamp(_speed * Time.deltaTime, 0, 1);
            item.transform.position = Vector3.Lerp(item.transform.position, transform.position, clampedSpeed);

            yield return null;
        }

        item.transform.DOScale(0, _scaleReduceDuration).OnComplete(() =>
        {
            //Attracted?.Invoke(item.Value);
            item.transform.DOComplete(true);
            Destroy(item.gameObject);
        });

        while (item)
        {
            item.transform.position = Vector3.MoveTowards(item.transform.position, transform.position,
                _scaleReduceMoveSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
