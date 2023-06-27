二、内置函数
1.OnGUI()函数
用来处理GUI事件，包括GUI的创建，外观变化和功能触发。是Unity内置的诸多回调函数的一部分。和Start()，Update()一样，由Unity自动调用
一帧当中可能会多次调用OnGUI函数，这取决于功能触发的频率，不过每一个GUI事件只会调用一次OnGUI函数
2.Start()函数
游戏第一帧运行时，Start函数会立刻调用，并且只被调用一次
通常游戏或程序会按照每秒播放固定的帧数来运行，在运行时，只有第一帧调用Start函数
初始化一些变量可以放在Start函数中，尽量不要放太过耗时的任务
3.Update()函数
游戏的每一帧都会调用Update函数，所以Update函数每秒会被调用多次。
当我们持续执行某个动作时，可以使用Update函数，例如敌人不停的来回移动
4.InvokeRepeating(...)函数
按照指定的频率重复调用某个函数
例子：InvokeRepeating("enemySpawn"，3，3) -- 在游戏开始3秒后，按照每次间隔3秒的频率，重复调用enemySpawn函数
以下的解释：https://blog.csdn.net/pixel_nplance/article/details/80759122
5.OnAwak()
6.OnEnable()
7.OnDisable()
8.OnDestroy()


2.创建按钮
使用Unity中内置的GUILayout类和它的Button()函数创建按钮，按钮文本和大小以参数形式。