using Cysharp.Threading.Tasks;
using Tools;

namespace SinglePlayer.GamePlay.Character;

public abstract class CharacterLogic : ICharacter, IChild<CharacterLogic>
{
	private readonly ReactiveProperty<int> ViewStar;

	private readonly ReactiveProperty<int> ViewHP;

	private readonly ReactiveProperty<int> ViewGold;

	private readonly ReactiveProperty<int> ViewATK;

	private readonly ReactiveProperty<int> ViewDEF;

	private readonly ReactiveProperty<bool> ViewInvincible;

	protected readonly UnitProperty _property;

	public Signal<int> HpChange { get; private set; }

	public Signal<int> GoldChange { get; private set; }

	public UnitProperty Property => _property;

	public abstract UniTask DoHit(int value);

	public abstract UniTask DoDamage(int value);

	public abstract UniTask DoDefend(int value);

	public abstract UniTask DoDodge(int value);

	public abstract UniTask DoDead();

	protected CharacterLogic(UnitProperty data)
	{
		_property = data;
		ViewStar = new ReactiveProperty<int>(data.Star);
		ViewHP = new ReactiveProperty<int>(data.HP);
		ViewGold = new ReactiveProperty<int>(data.Gold.Value);
		ViewATK = new ReactiveProperty<int>(data.ATK);
		ViewDEF = new ReactiveProperty<int>(data.DEF);
		ViewInvincible = new ReactiveProperty<bool>(data.Invincible);
		HpChange = new Signal<int>();
		GoldChange = new Signal<int>();
	}

	public ReactiveProperty<int> GetViewStar()
	{
		return ViewStar;
	}

	public ReactiveProperty<int> GetViewHP()
	{
		return ViewHP;
	}

	public ReactiveProperty<int> GetViewGold()
	{
		return ViewGold;
	}

	public ReactiveProperty<int> GetViewATK()
	{
		return ViewATK;
	}

	public ReactiveProperty<int> GetViewDEF()
	{
		return ViewDEF;
	}

	public ReactiveProperty<bool> GetShowInvincible()
	{
		return ViewInvincible;
	}

	public void UpdateViewProperty()
	{
		ViewStar.Value = _property.Star;
		ViewHP.Value = _property.HP;
		ViewGold.Value = _property.Gold.Value;
		ViewATK.Value = _property.ATK;
		ViewDEF.Value = _property.DEF;
		ViewInvincible.Value = _property.Invincible;
	}

	public bool IsDead()
	{
		return ViewHP.Value <= 0;
	}

	public T child<T>() where T : CharacterLogic
	{
		return this as T;
	}
}
