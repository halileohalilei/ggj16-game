using UnityEngine;

namespace Assets.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        private float _fadeOutSpeed = .5f;
        private bool _fadingOut;

        private float _remainingTime;
        private bool _songPlaying;

        [SerializeField] private AudioSource _metalAudioSource;
        [SerializeField] private AudioSource _jazzAudioSource;
        [SerializeField] private AudioSource _electronicAudioSource;
        [SerializeField] private AudioSource _classicalAudioSource;
        [SerializeField] private AudioSource _defaultAudioSource;

        private AudioSource _activeAudioSource;
        
        void Start()
        {
            _instance = this;

            _activeAudioSource = _defaultAudioSource;
        }

        void Update()
        {
            if (_fadingOut)
            {
                _activeAudioSource.volume -= _fadeOutSpeed*Time.deltaTime;
                if (_activeAudioSource.volume < 0.01f)
                {
                    _activeAudioSource.Stop();
                    _fadingOut = false;
                    _songPlaying = false;
                    if (TempoManager.GetInstance().IsGameActive())
                    {
                        TempoManager.GetInstance().EndGame();
                    }
                }
            }
            else if (_songPlaying)
            {
                if (_remainingTime < 0f)
                {
                    FadeOutSong();
                }
                else
                {
                    _remainingTime -= Time.deltaTime;
                }
            }

        }

        public static AudioManager GetInstance()
        {
            return _instance;
        }

        public void StartSong(GameLevel level)
        {
            switch (level)
            {
                case GameLevel.Jazz:
                    _activeAudioSource = _jazzAudioSource;
                    break;
                case GameLevel.Classic:
                    _activeAudioSource = _classicalAudioSource;
                    break;
                case GameLevel.Electronic:
                    _activeAudioSource = _electronicAudioSource;
                    break;
                case GameLevel.Metal:
                    _activeAudioSource = _metalAudioSource;
                    break;
            }
            _activeAudioSource.Play();
            _songPlaying = true;
            _remainingTime = 30f;
        }

        public void FadeOutSong()
        {
            _fadingOut = true;
        }
    }
}
