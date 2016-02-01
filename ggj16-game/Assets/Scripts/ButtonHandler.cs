using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField] private Image _fader;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _clickSound;

        private readonly Color _fadeColor = new Color(169f / 255f, 169f / 255f, 169f / 255f, 1f);
        private const float FadeSpeed = 5f;

        void Update()
        {
            if (_fader.IsActive())
            {
                _fader.color = Color.Lerp(_fader.color, _fadeColor, Time.deltaTime*FadeSpeed);
                if (!_audioSource.clip) _audioSource.volume = Mathf.Lerp(_audioSource.volume, 0f, Time.deltaTime*FadeSpeed);

                if (_fader.color.a > 0.99f)
                {
                    SceneManager.LoadScene("game_scene");
                }
            }
        }

        public void StartGame()
        {
            Debug.Log("Starting...");
            transform.GetComponent<Button>().interactable = false;
            _fader.gameObject.SetActive(true);
        }

        public void PlayClickSound()
        {
            _audioSource.PlayOneShot(_clickSound);
        }
    }
}
