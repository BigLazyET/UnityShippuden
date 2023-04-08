# Input

## 一、获取键盘按键
- Input.GetKey(KeyCode.A)：按住键盘A键
- Input.GetKeyUp：抬起键盘
- Input.GetKeyDown(KeyCode.X)：按下键盘X
- Input.GetAxisRaw("Horizontal")：按下得到结果：1/-1    // Project Settings -> Input Manager -> 查看键盘，鼠标等映射配置
- Input.GetAxis("Horizontal")：按下得到结果：有个递增递减，数据平滑连续过渡的过程，最终停留在1/-1
- Input.GetButton()：获取按钮信息，通常在Update()函数中调用，按钮状态只有在帧更新后重置
- Input.GetButtonDown("Jump")：按空格键跳跃，默认unity设置，可以Input Manager查看和修改
- Input.GetMouseButton(1)：0 -> 鼠标左键；1 -> 鼠标右键
- Input.touches：追踪移动触屏设备多点触控输入
- Input.acceleration：追踪加速信息
- Input.gyro：追踪地理位置
- input.touchCount：检测是否有输入