using System;
using cambrian.common;
using System.Collections;
using System.IO;
using Logger = cambrian.common.Logger;

namespace scene.game
{
    /// <summary>
    /// 资源配置表模块
    /// </summary>
    public class ABCfgModule : Module
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected static Logger log = LogFactory.getLogger<ABCfgModule>(true);

        /// <summary>
        /// 启动配置内容
        /// </summary>
        private Hashtable aBundlecfg;

        public ABCfgModule(string cfgpath)
        {
            load(cfgpath);
        }

        public void load(string cfgpath)
        {
            if (cfgpath == null || "".Equals(cfgpath)) return;
            if (VersionsCfg.cfg != null)
            {
                ConfigLoader loader = new ConfigLoader();
                string path = CacheLocalPath.AB_RESROUCE_PATH +cfgpath;
                if (File.Exists(path))
                    aBundlecfg = loader.load(path);
            }
        }

        /// <summary>
        /// 获取配置内容
        /// </summary>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public object getPropertyValue(string propertyname)
        {
            return aBundlecfg[propertyname];
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public void getPropertyValue(string propertyname, Action<string> value)
        {
            if (aBundlecfg == null)
            {
                value(null);
            }
            else
            {
                if (aBundlecfg[propertyname] == null || value == null)
                {
                    if (log.isDebug)
                    {
                        log.getMessage("aBundlecfg[" + propertyname + "]=" + aBundlecfg[propertyname] + ",values=" + value);
                    }
                    
                }
                   
                value((string)aBundlecfg[propertyname]);
            }
        }

        /// <summary>
        /// 获取多个值
        /// </summary>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public void getPropertyValues(string propertyname, Action<string[]> values)
        {
            if (aBundlecfg == null)
            {
                values(null);
            }
            else
            {
                if (aBundlecfg[propertyname] == null || values == null)
                {
                    if (log.isDebug)
                    {
                        log.getMessage("aBundlecfg[" + propertyname + "]=" + aBundlecfg[propertyname] + ",values=" + values);
                    }
                }
                values((string[]) aBundlecfg[propertyname]);
            }
        }
    }
}
