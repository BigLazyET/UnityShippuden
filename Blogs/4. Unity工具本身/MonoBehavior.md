# MonoBehavior

> 一般创建C#脚本都会继承MonoBehavior

## 一、Update方法
* 游戏世界每执行一帧，即机器每渲染一次都会执行一次Update方法
* Time.deltaTime：两次Upddate方法执行的时间间隔
* **渲染帧**，每次调用时间不固定

## 二、FixedUpdate方法
* 两次FixedUpdate方法执行的时间间隔都是固定的
* 固定的值是可以设定的：Edit -> Project Settings -> Time -> Fixed Timestep
* 物理相关的移动在此方法中去实现
* **逻辑帧**

## 三、LateUpdate方法
* 在Update之后执行
* 每一帧执行一次，Update执行多少次，LateUpdate也执行多少次