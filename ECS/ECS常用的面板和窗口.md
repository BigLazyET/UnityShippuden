# ECS常用的面板和窗口

`只针对ECS 1.0及其以上版本`

参考链接：[Performance and debugging](https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/performance-debugging.html)

## 一、Profile for Entity

打开Profile面板：Window -> Analysis -> Profiler，
使用 Profiler Modules 下拉框，选择如下模块

### 1. 针对Entity的性能分析模块
* Entities Structural Changes
* Entities Memory

## 二、Journaling for Entity

To enable Journaling, you can either:

* Window > Entities > Journaling
* Enable the option through the Preferences window (Preferences > Entities > Journaling).
* DISABLE_ENTITIES_JOURNALING

### 三、Other panels

* Window -> Entities -> Hirearchy/Components/Archetypes/System/Journaling
* Jobs -> Burst -> Open Inspector