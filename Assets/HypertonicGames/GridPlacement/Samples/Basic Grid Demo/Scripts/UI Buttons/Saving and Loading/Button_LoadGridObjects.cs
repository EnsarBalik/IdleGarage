using UnityEngine;
using UnityEngine.UI;

namespace Hypertonic.GridPlacement.Example.BasicDemo
{
    public class Button_LoadGridObjects : MonoBehaviour
    {
        public static event System.Action OnLoadGridObjectsEvent;

        public void Load()
        {
            OnLoadGridObjectsEvent.Invoke();
        }

        private void Start()
        {
            Invoke("Load", 0.5f);
        }
    }
}