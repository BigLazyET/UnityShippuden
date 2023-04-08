# Tilemap

2D地图生成：
a.TileMap生成 - 概念类比
精灵 - Sprite：色素粉
瓦片 - Tile：颜料泥（通过色素粉加水调和成的颜料泥）
笔刷 - Brush：画刷
调色板 - Palette：调色板（上面沾满了颜料，等待画刷蘸取然后在画布上刷）
瓦片地图 - TileMap：画布
b.2D SpriteShape - 在Unity的Package Manager中可以找到

## 碰撞器 - Tilemap Collider 2D
利用Tilemap Collider 2D组件可以直接针对Tilemap全体加上碰撞器
每个Tile都会加上一个碰撞器，按照Pixels per unit的最小尺寸