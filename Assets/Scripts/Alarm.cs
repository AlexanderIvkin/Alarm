using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    private const float MinVolume = 0f;
    private const float MaxVolume = 1f;

    [SerializeField] private Home _home;
    [SerializeField] private float _speed;

    private AudioSource _audioSource;
    private Coroutine _coroutine;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        _home.ThieveDetected += () => ChangeVolume(MaxVolume);
        _home.ThieveGone += () => ChangeVolume(MinVolume);
    }

    private void OnDisable()
    {
        _home.ThieveDetected -= () => ChangeVolume(MaxVolume);
        _home.ThieveGone -= () => ChangeVolume(MinVolume);
    }

    private void Init()
    {
        _audioSource.volume = MinVolume;
    }

    private void ChangeVolume(float targetVolume)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(ChangeVolumeRoutine(targetVolume));
    }

    private IEnumerator ChangeVolumeRoutine(float targetVolume)
    {
        var wait = new WaitForEndOfFrame();

        _audioSource.Play();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speed * Time.deltaTime);

            yield return wait;
        }

        if (_audioSource.volume == 0)
        {
            _audioSource.Stop();
        }
    }
}
