# Raycast

1.Raycasting（透射法） - 第101页
检测用户手指或者鼠标是否与屏幕的GameObject发生触碰，返回一个3D向量ray：
Camera.main.ScreenToWorldPoint(pos)
pos：用户手指在触屏上的坐标或者鼠标当前点击的位置坐标
2.拿到1中的ray结果，取出ray.x|ray.y获取用户手指或者鼠标当前位置的x,y的坐标，并创建2D向量touchPos，然后传给下面的方法，检测碰撞其是否覆盖这个点(touchPos)，此结果返回bool：
Physics2D.OverlapPoint(touchPos);