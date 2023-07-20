namespace Assets.Scripts.Interactive
{
    // 所有可交互的对象都得继承这个接口
    public interface IInteractive
    {
        void NormalActive();

        void SpecificActive();
    }
}
