using cambrian.game;
using UnityEngine;
using UnityEngine.EventSystems;

namespace platform.poker
{
    public class MouseClickSlideProcess : MonoBehaviour,
         IPointerEnterHandler, IPointerUpHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
    {
        public T getRoot<T>() where T : UnrealLuaPanel { return (T)root; }

        public enum Sounds
        {
            None,
            Button
        }

        public Sounds sound = Sounds.None;
        private UnrealLuaPanel _root;

        public UnrealLuaPanel root
        {
            get
            {
                if (_root == null)
                {
                    Transform tran = transform;
                    _root = tran.GetComponent<UnrealLuaPanel>();
                    while (_root == null)
                    {
                        tran = tran.parent;
                        _root = tran.GetComponent<UnrealLuaPanel>();
                    }
                }
                return _root;
            }
            set { _root = value; }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (sound == Sounds.Button) { SoundManager.playButton(); }
            if (enabled) mouseClick();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (enabled) mouseEnter();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (enabled) mouseDown();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (enabled) mouseExit();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.dragging) return;
            if (enabled) mouseUp();
        }

        public virtual void mouseExit() { }

        public virtual void mouseClick() { }

        public virtual void mouseDown() { }

        public virtual void mouseUp() { }

        public virtual void mouseEnter() { }

        public virtual void setVisible(bool b) { gameObject.SetActive(b); }
    }
}

