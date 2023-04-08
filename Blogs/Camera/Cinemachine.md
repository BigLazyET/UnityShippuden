# Cinemachine

**一个非常棒的相机插件**

> 其本质继承了主摄像头的配置

## 一、Cinemachine Confiner 2D

> Cinemachine -> Extensions -> Add Extensions -> Cinemachine Confiner 2D

限定镜头的可视范围，避免角色跳出场景之外，或者呈现场景之外的空间

- 给场景加一个能包含全部场景的Collider，一般是Polygon Collider 2D（根据相机需要的Collider类型而定）
- 勾选Is Trigger，因为不需要碰撞检测
- 设置Cinemachine Confiner 2D的Bounding Shape 2D为这个Collider，从而限定镜头的范围

1.镜头的远近拉伸
官方cinemachine插件
https://mp.weixin.qq.com/s/ak-zY3BfDebGC4saveH__Q