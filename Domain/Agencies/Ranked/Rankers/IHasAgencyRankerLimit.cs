namespace Fundalyzer.Domain.Agencies.Ranked.Rankers;

// Abstract static interface members, because why not?
public interface IHasAgencyRankerLimit
{
	static abstract int RankingLength { get;}
}