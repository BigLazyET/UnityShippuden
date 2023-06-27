[Unity目录结构](https://www.cnblogs.com/liudq/p/5540051.html)

3.判断当前平台
Application.platform
(RuntimePlatform.Androd | RuntimePlatform.IPhonePlayer)

控制人物运动的几种方法

## 旋转的三种方式
* 设定欧拉旋转角：transform.localEulerAngles
```
// new Vector(x,y,z)为游戏物体最终旋转到的目标角度
transform.localEulerAngles = new Vector3(x, y,z);
```
* 设定四元数：transform.rotation
```
// new Vector(x,y,z)为游戏物体最终旋转到的目标角度
Quaternion rotation= Quaternion.Euler(new Vector3(x, y,z))；
Transform.rotation=rotation;
```
* transform.Rotate
```
// 以自身坐标系为参考，而不是世界坐标系，分别以x度y度z度绕X轴、Y轴、Z轴匀速旋转
transform.Rotate(x,y,z)
```