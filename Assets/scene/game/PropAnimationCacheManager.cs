using cambrian.common;
using System.Collections;
using UnityEngine;

namespace scene.game
{
    /// <summary>
    /// 道具动画缓存列表
    /// </summary>
    public class PropAnimationCacheManager
    {
        private static PropAnimationCacheManager manager;

        private Hashtable table;

        public static PropAnimationCacheManager intance()
        {
            if (manager == null)
            {
                manager = new PropAnimationCacheManager();
            }
            return manager;
        }

        public static void reset()
        {
            manager = null;
        }

        /// <summary>
        /// 道具id（需要加载道具id）
        /// </summary>
        public int[] props = new int[] {13001, 13002, 13004, 13005};

        public PropAnimationCacheManager()
        {
            table = new Hashtable();
            init();
        }

        public void init()
        {
            GameObject widget = null;
            for (int i = 0; i < props.Length; i++)
            {
                widget = UnrealRoot.root.addABbundle("prefab/" + props[i]);
                if (widget != null)
                {
                    widget.name =props[i]+"";
                    propCache(props[i]);
                }
            }
        }

        public Sprite[] getPropCache(int id)
        {
            return (Sprite[]) this.table[id];
        }


        private void propCache(int id)
        {
            Transform tran = UnrealRoot.root.transform.Find(id+"");
           
            SpriteRenderer[] render = tran.GetComponentsInChildren<SpriteRenderer>();

            ArrayList<Sprite> sprites=new ArrayList<Sprite>();

            foreach (Transform child in tran)
            {
                sprites.add(child.GetComponent<SpriteRenderer>().sprite);
            }

            this.table.Add(id, sprites.toArray());
        }
    }
}
