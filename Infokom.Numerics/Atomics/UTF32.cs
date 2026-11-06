namespace Infokom.Numerics.Atomics
{








	public enum UTF32 : uint
	{
		BLACK_SPADE_SUIT = 0x00002660,
		BLACK_HEART_SUIT = 0x00002665,
		BLACK_DIAMOND_SUIT = 0x00002666,
		BLACK_CLUB_SUIT = 0x00002663,


		PLAYING_CARD_BACK = 0x0001F0A0,
		PLAYING_CARD_ACE_OF_SPADES = 0x0001F0A1,
		PLAYING_CARD_TWO_OF_SPADES = 0x0001F0A2,
		PLAYING_CARD_THREE_OF_SPADES = 0x0001F0A3,
		PLAYING_CARD_FOUR_OF_SPADES = 0x0001F0A4,
		PLAYING_CARD_FIVE_OF_SPADES = 0x0001F0A5,
		PLAYING_CARD_SIX_OF_SPADES = 0x0001F0A6,
		PLAYING_CARD_SEVEN_OF_SPADES = 0x0001F0A7,
		PLAYING_CARD_EIGHT_OF_SPADES = 0x0001F0A8,
		PLAYING_CARD_NINE_OF_SPADES = 0x0001F0A9,
		PLAYING_CARD_TEN_OF_SPADES = 0x0001F0AA,
		PLAYING_CARD_JACK_OF_SPADES = 0x0001F0AB,
		PLAYING_CARD_QUEEN_OF_SPADES = 0x0001F0AD,
		PLAYING_CARD_KING_OF_SPADES = 0x0001F0AE,

		PLAYING_CARD_ACE_OF_HEARTS = 0x0001F0B1,
		PLAYING_CARD_TWO_OF_HEARTS = 0x0001F0B2,
		PLAYING_CARD_THREE_OF_HEARTS = 0x0001F0B3,
		PLAYING_CARD_FOUR_OF_HEARTS = 0x0001F0B4,
		PLAYING_CARD_FIVE_OF_HEARTS = 0x0001F0B5,
		PLAYING_CARD_SIX_OF_HEARTS = 0x0001F0B6,
		PLAYING_CARD_SEVEN_OF_HEARTS = 0x0001F0B7,
		PLAYING_CARD_EIGHT_OF_HEARTS = 0x0001F0B8,
		PLAYING_CARD_NINE_OF_HEARTS = 0x0001F0B9,
		PLAYING_CARD_TEN_OF_HEARTS = 0x0001F0BA,
		PLAYING_CARD_JACK_OF_HEARTS = 0x0001F0BB,
		PLAYING_CARD_QUEEN_OF_HEARTS = 0x0001F0BD,
		PLAYING_CARD_KING_OF_HEARTS = 0x0001F0BE,

		PLAYING_CARD_ACE_OF_DIAMONDS = 0x0001F0C1,
		PLAYING_CARD_TWO_OF_DIAMONDS = 0x0001F0C2,
		PLAYING_CARD_THREE_OF_DIAMONDS = 0x0001F0C3,
		PLAYING_CARD_FOUR_OF_DIAMONDS = 0x0001F0C4,
		PLAYING_CARD_FIVE_OF_DIAMONDS = 0x0001F0C5,
		PLAYING_CARD_SIX_OF_DIAMONDS = 0x0001F0C6,
		PLAYING_CARD_SEVEN_OF_DIAMONDS = 0x0001F0C7,
		PLAYING_CARD_EIGHT_OF_DIAMONDS = 0x0001F0C8,
		PLAYING_CARD_NINE_OF_DIAMONDS = 0x0001F0C9,
		PLAYING_CARD_TEN_OF_DIAMONDS = 0x0001F0CA,
		PLAYING_CARD_JACK_OF_DIAMONDS = 0x0001F0CB,
		PLAYING_CARD_QUEEN_OF_DIAMONDS = 0x0001F0CD,
		PLAYING_CARD_KING_OF_DIAMONDS = 0x0001F0CE,

		PLAYING_CARD_ACE_OF_CLUBS = 0x0001F0D1,
		PLAYING_CARD_TWO_OF_CLUBS = 0x0001F0D2,
		PLAYING_CARD_THREE_OF_CLUBS = 0x0001F0D3,
		PLAYING_CARD_FOUR_OF_CLUBS = 0x0001F0D4,
		PLAYING_CARD_FIVE_OF_CLUBS = 0x0001F0D5,
		PLAYING_CARD_SIX_OF_CLUBS = 0x0001F0D6,
		PLAYING_CARD_SEVEN_OF_CLUBS = 0x0001F0D7,
		PLAYING_CARD_EIGHT_OF_CLUBS = 0x0001F0D8,
		PLAYING_CARD_NINE_OF_CLUBS = 0x0001F0D9,
		PLAYING_CARD_TEN_OF_CLUBS = 0x0001F0DA,
		PLAYING_CARD_JACK_OF_CLUBS = 0x0001F0DB,

		/// <summary>
		/// 🂛
		/// </summary>
		PLAYING_CARD_QUEEN_OF_CLUBS = 0x0001F0DD,

		/// <summary>
		/// 🂞
		/// </summary>
		PLAYING_CARD_KING_OF_CLUBS = 0x0001F0DE,








		/// <inheritdoc cref="UTF16.ElementOf"/>
		ElementOf = UTF16.ElementOf,

		/// <inheritdoc cref="UTF16.NotAnElementOf"/>
		NotAnElementOf = UTF16.NotAnElementOf,

		/// <inheritdoc cref="UTF16.SubsetOf"/>
		SubsetOf = UTF16.SubsetOf,

		/// <inheritdoc cref="UTF16.NotASubsetOf"/>
		NotASubsetOf = UTF16.NotASubsetOf,

		/// <inheritdoc cref="UTF16.SubsetOfOrEqualTo"/>
		SubsetOfOrEqualTo = UTF16.SubsetOfOrEqualTo,

		/// <inheritdoc cref="UTF16.NotASubsetOfOrEqualTo"/>
		NotASubsetOfOrEqualTo = UTF16.NotASubsetOfOrEqualTo,

		/// <inheritdoc cref="UTF16.SupersetOf"/>
		SupersetOf = UTF16.SupersetOf,

		/// <inheritdoc cref="UTF16.NotASupersetOf"/>
		NotASupersetOf = UTF16.NotASupersetOf,

		/// <inheritdoc cref="UTF16.SupersetOfOrEqualTo"/>
		SupersetOfOrEqualTo = UTF16.SupersetOfOrEqualTo,

		/// <inheritdoc cref="UTF16.NeitherASupersetOfNorASubsetOf"/>
		NeitherASupersetOfNorASubsetOf = UTF16.NeitherASupersetOfNorASubsetOf,

		/// <inheritdoc cref="UTF16.Union"/>
		Union = UTF16.Union,

		/// <inheritdoc cref="UTF16.Intersection"/>
		Intersection = UTF16.Intersection,

		/// <inheritdoc cref="UTF16.SetMinus"/>
		SetMinus = UTF16.SetMinus,

		/// <inheritdoc cref="UTF16.Xor"/>
		Xor = UTF16.Xor,

		/// <inheritdoc cref="UTF16.EmptySet"/>
		EmptySet = UTF16.EmptySet,

		/// <inheritdoc cref="UTF16.PlusSign"/>
		PlusSign = UTF16.PlusSign,

		/// <inheritdoc cref="UTF16.LessThanSign"/>
		LessThanSign = UTF16.LessThanSign,

		/// <inheritdoc cref="UTF16.EqualSign"/>
		EqualSign = UTF16.EqualSign,

		/// <inheritdoc cref="UTF16.GreaterThanSign"/>
		GreaterThanSign = UTF16.GreaterThanSign,

		/// <inheritdoc cref="UTF16.VerticalLine"/>
		VerticalLine = UTF16.VerticalLine,

		/// <inheritdoc cref="UTF16.Tilde"/>
		Tilde = UTF16.Tilde,

		/// <inheritdoc cref="UTF16.NotSign"/>
		NotSign = UTF16.NotSign,

		/// <inheritdoc cref="UTF16.PlusMinus"/>
		PlusMinus = UTF16.PlusMinus,

		/// <inheritdoc cref="UTF16.MultiplicationSign"/>
		MultiplicationSign = UTF16.MultiplicationSign,

		/// <inheritdoc cref="UTF16.DivisionSign"/>
		DivisionSign = UTF16.DivisionSign,

		/// <inheritdoc cref="UTF16.GreekReversedLunateEpsilonSymbol"/>
		GreekReversedLunateEpsilonSymbol = UTF16.GreekReversedLunateEpsilonSymbol,

		/// <inheritdoc cref="UTF16.FractionSlash"/>
		FractionSlash = UTF16.FractionSlash,

		/// <inheritdoc cref="UTF16.SuperscriptPlus"/>
		SuperscriptPlus = UTF16.SuperscriptPlus,

		/// <inheritdoc cref="UTF16.SuperscriptMinus"/>
		SuperscriptMinus = UTF16.SuperscriptMinus,

		/// <inheritdoc cref="UTF16.SuperscriptEquals"/>
		SuperscriptEquals = UTF16.SuperscriptEquals,

		/// <inheritdoc cref="UTF16.SubscriptPlus"/>
		SubscriptPlus = UTF16.SubscriptPlus,

		/// <inheritdoc cref="UTF16.SubscriptMinus"/>
		SubscriptMinus = UTF16.SubscriptMinus,

		/// <inheritdoc cref="UTF16.SubscriptEquals"/>
		SubscriptEquals = UTF16.SubscriptEquals,

		/// <inheritdoc cref="UTF16.ScriptCapitalP"/>
		ScriptCapitalP = UTF16.ScriptCapitalP,

		/// <inheritdoc cref="UTF16.SuperscriptTwo"/>
		SuperscriptTwo = UTF16.SuperscriptTwo,




	}
}
