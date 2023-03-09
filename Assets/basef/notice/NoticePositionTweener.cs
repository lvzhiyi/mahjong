using UnityEngine;

namespace basef.notice
{
    public class NoticePositionTweener: MonoBehaviour
    {
        UnrealTableContainer container;

        float minX;

        float width;

        public void setMinX()
        {
            if (this.container == null)
                this.container = this.transform.Find("container").GetComponent<UnrealTableContainer>();
            float w = this.container.getWidth();
            float w1 = transform.GetComponent<RectTransform>().rect.width;
            this.minX = - w-w1/2;
            this.width = w1/2;
            if (this.container == null) return;
            this.container.setLocalX(width);
        }


        void Update()
        {
            if (this.container == null) return;
            float x = this.container.transform.localPosition.x - 60 * Time.deltaTime;
            if (x < minX)
            {
                x = width;
            }
            this.container.setLocalX(x);
        }
    }
}
