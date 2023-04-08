




2D模式：
1.导入的任何图像都假定为2D图像(sprites)，并设置为Sprite模式
2.Sprite Packer已启用
3.场景视图（Scene面板）设置为2D
4.默认的游戏对象没有实时的方向灯
5.相机模式位置 0，0 ，-10
6.相机设置为正交模式（3D模式下是透视-近大远小）
7.其他天空盒不能使用等等
8.2D模式最明显的特征时场景视图工具栏中2D视图模式。当启用2D模式时，将设置为正字视图；相机沿着z轴看，Y轴上增加。
9.2D图形对象称为Sprites，精灵本质上只是标准纹理。在开发过程中有特殊的技术来组合和管理纹理。
10.Unity提供了一个内置的Sprite编辑器，可以从较大的图像中提取精灵图形。这可以让你在图像编辑器中编辑单个纹理中的多个组件图像（例如可以使用它来将角色的胳膊，腿和身体作为单独的元素保存在一幅图像中）
11.2D中使用Sprite渲染器组件渲染精灵，；3D中使用网格渲染器渲染纹理
12.Component -> Rendering -> Sprite Renderer，添加到GameObject中；直接使用已经附加的Sprite Renderer创建一个GameObject:GameObject->2D Object -> Sprite
13.可以使用Sprite Creator工具制作占位符2D图像
14.Unity像3D那样已经帮我们预制了一些精灵，比如square,diamond等等，在项目面板右击->Create -> Sprites -> 可以选择你需要的精灵
15.改变场景视图中的精灵，点击场景中的精灵 -> 在Inspector面板里找到Sprite Renderering中的Sprite选项，点击右边的小圆点可以更换精灵


