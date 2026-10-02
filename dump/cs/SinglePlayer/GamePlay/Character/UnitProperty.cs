using Tools;
using UnityEngine;

namespace SinglePlayer.GamePlay.Character;

public class UnitProperty : IChild<UnitProperty>
{
	public int Id;

	public ICharacter Character { get; protected set; }

	public CharacterType CharacterType { get; protected set; }

	public int MaxHP { get; protected set; }

	public int Star { get; protected set; }

	public int HP { get; protected set; }

	public ReactiveEncryptorProperty<int, Int32Encryptor> Gold { get; protected set; } = new ReactiveEncryptorProperty<int, Int32Encryptor>();

	public int ATK { get; protected set; }

	public int DEF { get; protected set; }

	public bool Invincible { get; protected set; }

	public void SetCharacter(ICharacter character)
	{
		Character = character;
	}

	public void ChangeStar(int changeStar)
	{
		Star += changeStar;
	}

	public void ChangeMaxHP(int maxHp)
	{
		MaxHP = maxHp;
	}

	public void ChangeHP(int changeHp)
	{
		int value = HP + changeHp;
		value = Mathf.Clamp(value, 0, MaxHP);
		HP = value;
	}

	public void ChangeGold(int changeGold)
	{
		int value = Gold.Value + changeGold;
		value = Mathf.Clamp(value, 0, int.MaxValue);
		Gold.Value = value;
	}

	public void ChangeATK(int changeAtk)
	{
		ATK += changeAtk;
	}

	public void ChangeDEF(int changeDef)
	{
		DEF += changeDef;
	}

	public T child<T>() where T : UnitProperty
	{
		return this as T;
	}
}
