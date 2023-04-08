# Movement

*相关链接：*
- 

## 一、Transform.Translate

让Transform在给定某个坐标系下以某个方向移动某些距离 => 参数属于一种移动或者说是位置上的增量，而非一个特定的需要移动的某个点position

### 1. position和localPosition
- position是世界坐标中的位置，可以理解为绝对坐标
- localPosition是相对于父对象的位置，是相对坐标，我们在transform栏看到的是相对坐标
- 如果对象是一级对象，position和localPosition是相同的

### 2. 举例
```csharp
speed.X = inputX * moveSpeed    // 存储速度值，speed：vector2
translation = speed * Time.deltaTime    // 位移的增量，translation：Vector3
transform.Translate(translation, Space.World)   // 世界坐标系下，transform对象移动translation
```

## 二、插值

## 三、Force + Velocity

## 四、DoTween

## 五、配合其他组件

### 1. 刚体组件
- 使用重力：**不推荐unity提供的物理，太物理了反而不能自己实现一切**，还是手写物理：手写下落，碰撞检测等
- 刚体 可以配合 碰撞检测
- 刚体body type使用Kinematic，使用自己的脚本完成跳跃