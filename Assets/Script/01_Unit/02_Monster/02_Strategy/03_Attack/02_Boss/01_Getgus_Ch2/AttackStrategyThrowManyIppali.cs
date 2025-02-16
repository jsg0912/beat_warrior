public class AttackStrategyThrowManyIppali : AttackStrategyThrowMany
{
    public AttackStrategyThrowManyIppali(float throwSpeed, float maxHeight, int throwCountMax, float throwInterval, PoolTag poolTag) : base(throwSpeed, maxHeight, throwCountMax, throwInterval, poolTag)
    {
        this.throwCountMax = throwCountMax;
        this.throwInterval = throwInterval;
        this.poolTag = poolTag;
    }
    protected override void SetTargetPosition()
    {
        /*
            TODO KMJ: 
            알은 두 방향으로 뱉는다. 

            위로 뱉을 때는 위로 포물선 1개 아래 직선으로 1개를 소환한다. 

            포물선은 왼/오른쪽 2층 타일 중 1곳 중앙으로 발사한다. 

            2층 타일의 방향은 랜덤으로 결정한다. 

            아래 직선은 보스 입 바로 직선 아래로 발사하여 0층에 소환한다. 
        */

        targetPosition = GetPlayerPos();
    }
}