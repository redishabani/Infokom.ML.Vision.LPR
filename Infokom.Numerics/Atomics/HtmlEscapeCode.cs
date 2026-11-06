namespace Infokom.Numerics.Atomics
{
	

	public enum HtmlEscapeCode : uint
	{
		// ==========================================
		// 1. CORE & STRUCTURAL ESCAPES
		// ==========================================

		/// <summary> Horizontal Tab: &#x09; </summary>
		Tab = 0x09,

		/// <summary> New Line / Line Feed: &#x0A; </summary>
		NewLine = 0x0A,

		/// <summary> Exclamation Mark: &#x21; </summary>
		Exclamation = 0x21,

		/// <summary> Double Quotation Mark: &#x22; </summary>
		DoubleQuote = 0x22,

		/// <summary> Number Sign / Hash: &#x23; </summary>
		Hash = 0x23,

		/// <summary> Dollar Sign: &#x24; </summary>
		Dollar = 0x24,

		/// <summary> Percent Sign: &#x25; </summary>
		Percent = 0x25,

		/// <summary> Ampersand: &#x26; </summary>
		Ampersand = 0x26,

		/// <summary> Apostrophe / Single Quote: &#x27; </summary>
		Apostrophe = 0x27,

		/// <summary> Left Parenthesis: &#x28; </summary>
		LeftParenthesis = 0x28,

		/// <summary> Right Parenthesis: &#x29; </summary>
		RightParenthesis = 0x29,

		/// <summary> Asterisk: &#x2A; </summary>
		Asterisk = 0x2A,

		/// <summary> Plus Sign: &#x2B; </summary>
		Plus = 0x2B,

		/// <summary> Comma: &#x2C; </summary>
		Comma = 0x2C,

		/// <summary> Solidus / Forward Slash: &#x2F; </summary>
		ForwardSlash = 0x2F,

		/// <summary> Colon: &#x3A; </summary>
		Colon = 0x3A,

		/// <summary> Semicolon: &#x3B; </summary>
		Semicolon = 0x3B,

		/// <summary> Less Than Sign: &#x3C; </summary>
		LessThan = 0x3C,

		/// <summary> Equals Sign: &#x3D; </summary>
		Equals = 0x3D,

		/// <summary> Greater Than Sign: &#x3E; </summary>
		GreaterThan = 0x3E,

		/// <summary> Question Mark: &#x3F; </summary>
		QuestionMark = 0x3F,

		/// <summary> Commercial At / At Sign: &#x40; </summary>
		AtSign = 0x40,

		// ==========================================
		// 2. ISO 8859-1 / HTML4 STANDARD SYMBOLS
		// ==========================================

		/// <summary> Non-Breaking Space: &#xA0; </summary>
		NonBreakingSpace = 0xA0,

		/// <summary> Inverted Exclamation Mark: &#xA1; </summary>
		InvertedExclamation = 0xA1,

		/// <summary> Cent Sign: &#xA2; </summary>
		Cent = 0xA2,

		/// <summary> Pound Sign: &#xA3; </summary>
		Pound = 0xA3,

		/// <summary> Currency Sign: &#xA4; </summary>
		Currency = 0xA4,

		/// <summary> Yen Sign: &#xA5; </summary>
		Yen = 0xA5,

		/// <summary> Broken Bar: &#xA6; </summary>
		BrokenBar = 0xA6,

		/// <summary> Section Sign: &#xA7; </summary>
		Section = 0xA7,

		/// <summary> Diaeresis / Umlaut Accent: &#xA8; </summary>
		Umlaut = 0xA8,

		/// <summary> Copyright Sign: &#xA9; </summary>
		Copyright = 0xA9,

		/// <summary> Feminine Ordinal Indicator: &#xAA; </summary>
		FeminineOrdinal = 0xAA,

		/// <summary> Left-Pointing Double Angle Quotation: &#xAB; </summary>
		LeftGuillemet = 0xAB,

		/// <summary> Not Sign / Logical Negation: &#xAC; </summary>
		NotSign = 0xAC,

		/// <summary> Soft Hyphen: &#xAD; </summary>
		SoftHyphen = 0xAD,

		/// <summary> Registered Sign: &#xAE; </summary>
		Registered = 0xAE,

		/// <summary> Macron Accent: &#xAF; </summary>
		Macron = 0xAF,

		/// <summary> Degree Sign: &#xB0; </summary>
		Degree = 0xB0,

		/// <summary> Plus-Minus Sign: &#xB1; </summary>
		PlusMinus = 0xB1,

		/// <summary> Superscript Two / Squared: &#xB2; </summary>
		SuperscriptTwo = 0xB2,

		/// <summary> Superscript Three / Cubed: &#xB3; </summary>
		SuperscriptThree = 0xB3,

		/// <summary> Acute Accent: &#xB4; </summary>
		AcuteAccent = 0xB4,

		/// <summary> Micro Sign / Mu: &#xB5; </summary>
		Micro = 0xB5,

		/// <summary> Pilcrow Sign / Paragraph Sign: &#xB6; </summary>
		Paragraph = 0xB6,

		/// <summary> Middle Dot / Interpunct: &#xB7; </summary>
		MiddleDot = 0xB7,

		/// <summary> Cedilla Accent: &#xB8; </summary>
		Cedilla = 0xB8,

		/// <summary> Superscript One: &#xB9; </summary>
		SuperscriptOne = 0xB9,

		/// <summary> Masculine Ordinal Indicator: &#xBA; </summary>
		MasculineOrdinal = 0xBA,

		/// <summary> Right-Pointing Double Angle Quotation: &#xBB; </summary>
		RightGuillemet = 0xBB,

		/// <summary> Vulgar Fraction One Quarter: &#xBC; </summary>
		FractionQuarter = 0xBC,

		/// <summary> Vulgar Fraction One Half: &#xBD; </summary>
		FractionHalf = 0xBD,

		/// <summary> Vulgar Fraction Three Quarters: &#xBE; </summary>
		FractionThreeQuarters = 0xBE,

		/// <summary> Inverted Question Mark: &#xBF; </summary>
		InvertedQuestion = 0xBF,

		/// <summary> Multiplication Sign: &#xD7; </summary>
		Multiply = 0xD7,

		/// <summary> Division Sign: &#xF7; </summary>
		Divide = 0xF7,

		/// <summary>
		/// Greek Capital Letter Alpha: &#x391;
		/// </summary>
		Αλφα = 0x391,

		
		/// <summary>
		/// Greek Capital Letter Beta: &#x392;
		/// </summary>
		Βήτα = 0x392,
		Γάμμα = 0x393,
		Δέλτα = 0x394,
		Εψιλον = 0x395,
		Ζήτα = 0x396,
		Ητα = 0x397,
		Θήτα = 0x398,
		Ιώτα = 0x399,
		Κάππα = 0x39A,
		Λάμβδα = 0x39B,
		Μυ = 0x39C,
		Νυ = 0x39D,
		Ξι = 0x39E,
		Ομικρον = 0x39F,
		Ρο = 0x3A0,
		Σίγμα = 0x3A3,
		Ταυ = 0x3A4,
		Υψιλον = 0x3A5,
		Φι = 0x3A6,
		Χι = 0x3A7,
		Ψι = 0x3A8,
		Ωμέγα = 0x3A9,












		GreekCapitalLetterBeta = Βήτα,
		GreekCapitalLetterGamma = Γάμμα,
		GreekCapitalLetterDelta = Δέλτα,
		GreelCapitalLetterEpsilon = Εψιλον,
		GreekCapitalLetterZeta = Ζήτα,
		GreekCapitalLetterEta = Ητα,
		GreekCapitalLetterTheta = Θήτα,
		GreekCapitalLetterIota = Ιώτα,
		GreekCapitalLetterKappa = Κάππα,
		GreekCapitalLetterLambda = Λάμβδα,


		/*
		 913	Α	&Alpha;	&#913;	Alpha
914	Β	&Beta;	&#914;	Beta
915	Γ	&Gamma;	&#915;	Gamma
916	Δ	&Delta;	&#916;	Delta
917	Ε	&Epsilon;	&#917;	Epsilon
918	Ζ	&Zeta;	&#918;	Zeta
919	Η	&Eta;	&#919;	Eta
920	Θ	&Theta;	&#920;	Theta
921	Ι	&Iota;	&#921;	Iota
922	Κ	&Kappa;	&#922;	Kappa
923	Λ	&Lambda;	&#923;	Lambda
924	Μ	&Mu;	&#924;	Mu
925	Ν	&Nu;	&#925;	Nu
926	Ξ	&Xi;	&#926;	Xi
927	Ο	&Omicron;	&#927;	Omicron
928	Π	&Pi;	&#928;	Pi
929	Ρ	&Rho;	&#929;	Rho
931	Σ	&Sigma;	&#931;	Sigma
932	Τ	&Tau;	&#932;	Tau
933	Υ	&Upsilon;	&#933;	Upsilon
934	Φ	&Phi;	&#934;	Phi
935	Χ	&Chi;	&#935;	Chi
936	Ψ	&Psi;	&#936;	Psi
937	Ω	&Omega;	&#937;	Omega
945	α	&alpha;	&#945;	alpha
946	β	&beta;	&#946;	beta
947	γ	&gamma;	&#947;	gamma
948	δ	&delta;	&#948;	delta
949	ε	&epsilon;	&#949;	epsilon
950	ζ	&zeta;	&#950;	zeta
951	η	&eta;	&#951;	eta
952	θ	&theta;	&#952;	theta
953	ι	&iota;	&#953;	iota
954	κ	&kappa;	&#954;	kappa
955	λ	&lambda;	&#955;	lambda
956	μ	&mu;	&#956;	mu
957	ν	&nu;	&#957;	nu
958	ξ	&xi;	&#958;	xi
959	ο	&omicron;	&#959;	omicron
960	π	&pi;	&#960;	pi
961	ρ	&rho;	&#961;	rho
962	ς	&sigmaf;	&#962;	sigmaf
963	σ	&sigma;	&#963;	sigma
964	τ	&tau;	&#964;	tau
965	υ	&upsilon;	&#965;	upsilon
966	φ	&phi;	&#966;	phi
967	χ	&chi;	&#967;	chi
968	ψ	&psi;	&#968;	psi
969	ω	&omega;	&#969;	omega
977	ϑ	&thetasym;	&#977;	Theta symbol
978	ϒ	&upsih;	&#978;	Upsilon symbol
982	ϖ	&piv;	&#982;	Pi symbol
		 */
	}
}
