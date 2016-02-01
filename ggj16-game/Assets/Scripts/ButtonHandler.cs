using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ButtonHandler : MonoBehaviour
    {
        [SerializeField] private Image _fader;

        private Color _fadeColor = new Color(169f / 255f, 169f / 255f, 169f / 255f, 1f);

        void Update()
        {
            if (_fader.IsActive())
            {
                _fader.color = Color.Lerp(_fader.color, _fadeColor, Time.deltaTime * 5f);

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
    }
}
