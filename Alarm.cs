using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collision))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private Collision _collision;
    private float _startVolume = 0.1f;
    private float _maxVolume = 1f;

    private void Start()
    {
        _collision = GetComponent<Collision>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Fraud fraud))
            _audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Fraud fraud))
            StartCoroutine(SmoothOff());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Fraud fraud))
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _startVolume * Time.deltaTime);
    }

    private IEnumerator SmoothOff()
    {
        WaitForSeconds wait = new WaitForSeconds(_startVolume);

        while (_audioSource.volume > _startVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _startVolume, _startVolume);

            yield return wait;
        }

        if (_audioSource.volume <= _maxVolume)
            _audioSource.Stop();

        yield return null;
    }
}
