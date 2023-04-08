# 2D Joint

## 一、相关链接
- [Physics Joints in Unity 2D](https://www.raywenderlich.com/1766-physics-joints-in-unity-2d)
- [2D Joints Starter](https://koenig-media.raywenderlich.com/uploads/2015/05/2D_Joints_Starter.zip)
- [2D_Joints_Complete](https://koenig-media.raywenderlich.com/uploads/2015/05/2D_Joints_Complete.zip)

## 二、Distance Joint 2D - 距离关节2D
> 两种物体之间保持相距额定的距离；但是勾选Max Distance Only会允许两个物体接近且相互通过，只不过限定最大距离

### 1. 挂载组件
* 选择物体，添加组件(Add Component) -> Distance Joint 2D Component
* 你会立马发现一根绿色的线从你选择的物体延展到屏幕中央点，这个点称为起始点，(0,0)

### 2. 组件属性
- Collide Connected：关节连接的两个物体之间是否可以碰撞；不勾选的话两个物体可以彼此穿过
- Connected Rigid Body：除挂载关节组件本身的物体之外，关节另一端连接的物体；不设置则默认连接到屏幕的某个点上
- Anchor：锚点，表示关节的一端连接到物体(就是挂载关节组件本身的物体)上的点，点的坐标相对于物体本身的坐标系，一般(0,0)即可
- Connected Anchor：连接锚点，与Anchor对应，表示关节的一端连接到另一个物体上(即Connected Rigid Body)的点（相对于**物体本身的坐标系**）；如果Connected Rigid Body为空，那么这个点就是**屏幕坐标系**上的一个点
- Distance：关节连接的两个物体之间的距离
- Max Distance Only：关节连接的两个物体之间的最大距离；注意：**勾选上之后，关节只强制执行最大距离，意味着关节连接的两个物体是可以相互靠近的，但是没办法相互远离超过Max Distance Only设定的值**

## 三、Spring Joint 2D - 弹簧关节2D
> 弹簧关节对连接施加张力，就像两个物体通过弹簧连接一样

> 挂载组件方式同Distance Joint 2D
### 1. 组件属性
**大部分属性与Distance Joint 2D保持一致**
- Distance：距离，关节连接的两个物体之间的距离，弹簧关节围绕这个距离来回反弹，最终停到这个距离值处
- Damping Ratio：阻尼比，它决定了由弹簧连接的物体多快静止；取值0-1之间，0代表最慢静止：换句话说就是弹簧最有弹性 -> 来回的幅度就越大，花费的时间就越长 -> 所以就越慢静止
- Frequency：频率，弹簧每秒弹跳的次数；较低的值表示更具有弹性的弹簧 -> 来回的幅度就越大，花费的时间越长，所以每秒弹跳的次数就不会很多

## 四、Hinge Joint 2D - 铰链关节2D
> 此关节让一个带有刚体的物体围绕一个固定点旋转
>
> 当此物体收到力的作用时，此关节可以计算一个正确的旋转角度

> 挂载组件方式同Distance Joint 2D
### 1. 组件属性
**大部分属性与Distance Joint 2D保持一致**
- Use Motor：设定物理引擎施加恒定的力来旋转来努力(可能有其他的力来加快或者减慢速度)达到一个额定的速度
- Motor
    - Motor Speed：旋转速度（角速度）
    - Maximum Motor Force：施加的最大扭矩力
- Use Limits：设置限制上角和下角的最大角度
- Angle Limits
    - Lower Angle：下角
    - Upper Angle：上角

## 五、Slider Joint 2D - 滑动关节2D
> 此关节限制了物体沿空间线的移动

> 挂载组件方式同Distance Joint 2D
### 1. 组件属性
**大部分属性与Distance Joint 2D保持一致**
- Angle：指定锚点与连接锚点之间的角度
- Use Motor：设定物理引擎施加恒定的力来让物体进行线性运动
- Motor
    - Motor Speed：线性运动速度
    - Maximum Motor Force：施加的最大线性力
- Use Limits：设置限制上角和下角的最大角度
- Translation Limits
    - Lower Translation：指定了运行的距离限制
    - Upper Translation：制定了运行的距离限制

## 六、Wheel Joint 2D - 车轮关节2D
虽然您可以使用铰链关节制作旋转物体，但您需要使用车轮关节来模拟汽车上的车轮等物体
> 它模拟一个可以连接到另一个对象的滚轮，配有一个悬挂弹簧，可在车轮与其其他锚点之间提供一些"缓冲"

模拟滚动的车轮，使对象可通过车轮而移动.车轮使用悬架“弹簧”来保持与车身主体的距离

## 七、其他
- Fixed Joint2D固定关节
- Friction Joint 2D摩擦关节
- Relative Joint 2D相对关节

## 八、实际操作！
