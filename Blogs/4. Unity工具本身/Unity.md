# Unity

## 一、Project Settings
- Edit -> Project Settings -> Editor -> Inspector面板 -> Default Behavior Mode -> 切换2D和3D模式
- Edit -> Preferences -> External Tools -> External Script Editor -> 切换脚本编辑器
- Edit -> Project Settings -> Input -> Unity默认输入管理配置
- Edit -> Project Settings -> Player -> Resolution -> 游戏运行分辨率设置
- Eidt -> Project Settings -> Time -> Fixed Timestep -> 修改FixUpdate的时间间隔
- Edit -> Project Settings -> Input Manager -> 查看键盘，鼠标等映射配置
- MonoDevelop两种调试方式：最好用Run -> Attach to Process -> Unity调试

## 二、Preferences
- Scripts脚本的默认打开设置：Edit -> Preferences

## 三、菜单栏功能自定义
1.类A：Editor
给内部方法比如Create()，添加Attribute：[MenuItem("CreateAsset/Asset")]
那么就可以在Unity界面的顶部菜单栏中找到CreateAsset -> Asset 
2.给可序列化类添加Attribute：[CreateAssetMenu (menuName = "ET/State")]
那么就可以在Unity界面的顶部菜单栏中的Asset中找到"ET"菜单及子菜单"State"
此处可举一反三

## 四、物体Inspector面板自定义
- 序列化字段Attribute：[SerializeField]可以让非public的字段显示在unity的inspector面板上 => 便于运行时变更数值调试
- 默认public的属性就可以显示在unity的inspector面板上 => 便于运行时变更数值调试

## 五、游戏数据
1.Unity自定义asset文件(.asset扩展名)：
相比2，它配置数据格式多样；
但是关联资源删除，相应配置数据丢失
2.其他格式文件(扩展名为.txt,.json,.xml等等)

可序列化类：
ScriptableObject可以将配置可序列化类，并保存成自定义配置资源.asset
1.它可以向其他资源管理它
2.它可以在不同项目中复用

## 六、其他面板
Project面板中项目结构参考：
1.Prefabs：预制体，可重用的游戏对象
2.Scenes：场景
3.Scripts：代码
4.Sounds：音频
5.Textures：纹理是游戏中的精灵和图像，当然2D中可以命名为Sprites
6.Resources：非常有用且独特的文件夹，允许在脚本中通过使用静态Resources类加载一个对象或者文件 - 可以用于菜单

Hierarchy面板中对象结构参考：
1.Scripts：记住Hierarchy面板中都是对象，所以这里的Scripts是一个对象，只不过附加在上面的是全局脚本
2.Render：放置摄像机及光线对象
3.Level：里面可以添加三个空的子对象：Background，Middleground，Foreground