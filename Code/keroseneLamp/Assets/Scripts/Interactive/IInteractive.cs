namespace Assets.Scripts.Interactive
{
    // 所有可交互的对象都得继承这个接口
    public interface IInteractive
    {
        /// <summary>
        /// 针对特殊互动：此情况比较特殊，可能后期获得了某个装备/到达了某个地方/解锁了某个技能都能触发特殊互动
        /// 特殊互动的先决条件
        /// </summary>
        /// <returns></returns>
        bool SpecialActiveCheck();
    }
}
