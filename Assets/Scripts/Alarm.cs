using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarm : MonoBehaviour
{
    [SerializeField] private Home _home;

    private AudioSource _audioSource;

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
        _home.ThieveDetected += ChangeVolume;
        _home.ThieveGone += ChangeVolume;
    }

    private void OnDisable()
    {
        _home.ThieveDetected -= ChangeVolume;
        _home.ThieveGone -= ChangeVolume;
    }

    private void Init()
    {
        _audioSource.volume = 0;
    }

    private void ChangeVolume(int step)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeVolumeRoutine(step));
    }

    private IEnumerator ChangeVolumeRoutine(int step)
    {
        var wait = new WaitForEndOfFrame();

        _audioSource.Play();

        while (_audioSource.volume <= 1 && _audioSource.volume >= 0)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, 1, step * Time.deltaTime);

            yield return wait;
        }

        if (_audioSource.volume == 0)
        {
            _audioSource.Stop();
        }
    }
}
