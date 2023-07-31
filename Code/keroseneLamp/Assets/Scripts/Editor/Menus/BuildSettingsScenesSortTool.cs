using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    // 参考链接：https://blog.csdn.net/avi9111/article/details/127838244
    public class BuildSettingsScenesSortTool
    {
        class PathComparer : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                if (x.Contains("/Level/") == false) return 0;//只调整/level/目录下的，不是这目录下的不参与排序
                short[] ix = new short[x.Length];
                for (int counter = 0; counter < x.Length; counter++)
                {
                    ix[counter] = (short)x[counter];//转成asc2码
                }

                short[] iy = new short[y.Length];
                for (int counter = 0; counter < y.Length; counter++)
                {
                    iy[counter] = (short)y[counter];//转成asc2码
                }

                for (int i = 0; i < ix.Length; i++)
                {
                    if (i >= iy.Length) return 1;//x比较长，则调整循序
                    if (ix[i] == iy[i])
                        continue;
                    if (ix[i] > iy[i])
                        return 1;

                    return -1;
                }

                return 0;
            }
        }

        // 这个Attribute的参数决定了在Unity编辑器菜单栏中加入了一个菜单工具，其路径为：游戏工具/资源打包/BuildSetting场景排序
        [MenuItem("游戏工具/资源打包/BuildSetting场景排序")]
        public void SortScenesInBuildSettings()
        {
            var scenes = EditorBuildSettings.scenes;
            Debug.Log("开始排序");
            EditorBuildSettings.scenes = scenes.OrderBy(x=>x.path, new PathComparer()).ToArray();
        }
    }
}
