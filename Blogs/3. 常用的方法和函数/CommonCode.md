[Unity目录结构](https://www.cnblogs.com/liudq/p/5540051.html)

3.判断当前平台
Application.platform
(RuntimePlatform.Androd | RuntimePlatform.IPhonePlayer)

控制人物运动的几种方法

## 旋转的三种方式

参考链接：
* [Unity中的旋转与方向](https://docs.unity.cn/cn/2021.1/Manual/QuaternionAndEulerRotationsInUnity.html)
* [利用四元数实现钟表指针旋转](https://blog.csdn.net/thinbug/article/details/121168810)

### 1. 四元数 - Quaternion

* `处理脚本中的旋转时，应使用 Quaternion 类及其函数来创建和修改旋转值`
* 通过四元数来实现旋转：Transform.rotation=rotation;
* [Quaternion手册](https://docs.unity.cn/cn/2021.1/ScriptReference/Quaternion.html)


### 2. 欧拉角 - EulerAngles

* 通过设定欧拉角来旋转：transform.localEulerAngles = new Vector3(x, y,z);
* 可以使用欧拉角，最终仍应该存储为 Quaternion
```
// 正确方法
float x;    // 将欧拉角缓存起来，再应用；而不是依赖从四元数读取回欧拉角！
void Update () 
{
    x += Time.deltaTime * 10;
    transform.rotation = Quaternion.Euler(x,0,0);
}
```
* 避免四元数中获取、修改和重新应用欧拉角
```
// 错误用法
void Update () 
{
    var angles = transform.rotation.eulerAngles;
    angles.x += Time.deltaTime * 10;
    transform.rotation = Quaternion.Euler(angles);
}
```

### 3. Transform提供的方法，其最终还是落实到四元数Quaternion
* [Transform.Rotate](https://docs.unity.cn/cn/2021.1/ScriptReference/Transform.Rotate.html)
* [Transform.RotateAround](https://docs.unity.cn/cn/2021.1/ScriptReference/Transform.RotateAround.html)