# ECS InputSystem

参考链接：
* [DemoInputGatheringSystem](https://github.com/Unity-Technologies/EntityComponentSystemSamples/blob/f22bb949b3865c68d5fc588a6e8d032096dc788a/PhysicsSamples/Assets/Common/Scripts/DemoInputGatheringSystem.cs)

`我们可以根据不同角色类型来构建不同的Singleton Input Component Data`

## 一、InputSystem

正常配置

## 二、Input Gathering System

## 三、InputActions Interface

* System继承需要的InputActions Interface
* 实现InputActions中的各个Action

## 四、Create Singleton Input Component Data

* 根据实现InputActions中的各个Action，获取到Input
* 将Input存储到对应的Component Data中