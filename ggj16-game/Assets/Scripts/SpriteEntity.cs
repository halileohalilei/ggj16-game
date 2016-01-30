using UnityEngine;

namespace Assets.Scripts
{
    public class SpriteEntity : MonoBehaviour {
        
        void Start ()
        {
            var spriteRenderer = transform.FindChild("Renderer");
            spriteRenderer.rotation = Camera.main.transform.rotation;
        }
    }
}
