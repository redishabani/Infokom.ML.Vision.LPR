using System.Collections.Immutable;

namespace Infokom.Numerics.Atomics
{
	public enum ASCII
	{
		/// <summary>
		/// Null char
		/// </summary>
		NUL = 0,

		/// <summary>
		/// Start of Heading
		/// </summary>
		SOH = 1,

		/// <summary>
		/// Start of Text
		/// </summary>
		STX = 2,

		/// <summary>
		/// End of Text
		/// </summary>
		ETX = 3,

		/// <summary>
		/// End of Transmission
		/// </summary>
		EOT = 4,

		/// <summary>
		/// Enquiry
		/// </summary>
		ENQ = 5,

		/// <summary>
		/// Acknowledgment
		/// </summary>
		ACK = 6,

		/// <summary>
		/// Bell
		/// </summary>
		BEL = 7,

		/// <summary>
		/// Backspace
		/// </summary>
		BS = 8,

		/// <summary>
		/// Horizontal Tab (\t)
		/// </summary>
		TAB = 9,

		/// <summary>
		/// Line Feed / New Line (\n)
		/// </summary>
		LF = 10,

		/// <summary>
		/// Vertical Tab
		/// </summary>
		VT = 11,

		/// <summary>
		/// Form Feed
		/// </summary>
		FF = 12,

		/// <summary>
		/// Carriage Return (\r)
		/// </summary>
		CR = 13,

		/// <summary>
		/// Shift Out
		/// </summary>
		SO = 14,

		/// <summary>
		/// Shift In
		/// </summary>
		SI = 15,

		/// <summary>
		/// Data Link Escape
		/// </summary>
		DLE = 16,

		/// <summary>
		/// Device Control 1 (XON)
		/// </summary>
		DC1 = 17,

		/// <summary>
		/// Device Control 2
		/// </summary>
		DC2 = 18,

		/// <summary>
		/// Device Control 3 (XOFF)
		/// </summary>
		DC3 = 19,

		/// <summary>
		/// Device Control 4
		/// </summary>
		DC4 = 20,

		/// <summary>
		/// Negative Acknowledgment
		/// </summary>
		NAK = 21,

		/// <summary>
		/// Synchronous Idle
		/// </summary>
		SYN = 22,

		/// <summary>
		/// End of Trans. Block
		/// </summary>
		ETB = 23,

		/// <summary>
		/// Cancel
		/// </summary>
		CAN = 24,

		/// <summary>
		/// End of Medium
		/// </summary>
		EM = 25,

		/// <summary>
		/// Substitute
		/// </summary>
		SUB = 26,

		/// <summary>
		/// Escape (\e)
		/// </summary>
		ESC = 27,

		/// <summary>
		/// File Separator
		/// </summary>
		FS = 28,

		/// <summary>
		/// Group Separator
		/// </summary>
		GS = 29,

		/// <summary>
		/// Record Separator
		/// </summary>
		RS = 30,

		/// <summary>
		/// Unit Separator
		/// </summary>
		US = 31,

		/// <summary>
		/// Space character ' '
		/// </summary>
		SPACE = 32,

		/// <summary>
		/// Exclamation point '!'
		/// </summary>
		EXCLAMATION = 33,

		/// <summary>
		/// Quotation mark '"'
		/// </summary>
		QUOTE = 34,

		/// <summary>
		/// Number sign '#'
		/// </summary>
		HASH = 35,

		/// <summary>
		/// Dollar sign '$'
		/// </summary>
		DOLLAR = 36,

		/// <summary>
		/// Percent sign '%'
		/// </summary>
		PERCENT = 37,

		/// <summary>
		/// Ampersand '&amp;'
		/// </summary>
		AMPERSAND = 38,

		/// <summary>
		/// Apostrophe '\''
		/// </summary>
		APOSTROPHE = 39,

		/// <summary>
		/// Left parenthesis '('
		/// </summary>
		LEFT_PARENTHESIS = 40,

		/// <summary>
		/// Right parenthesis ')'
		/// </summary>
		RIGHT_PARENTHESIS = 41,

		/// <summary>
		/// Asterisk '*'
		/// </summary>
		ASTERISK = 42,

		/// <summary>
		/// Plus sign '+'
		/// </summary>
		PLUS = 43,

		/// <summary>
		/// Comma ','
		/// </summary>
		COMMA = 44,

		/// <summary>
		/// Hyphen / Minus '-'
		/// </summary>
		HYPHEN = 45,

		/// <summary>
		/// Period / Dot '.'
		/// </summary>
		PERIOD = 46,

		/// <summary>
		/// Slash '/'
		/// </summary>
		SLASH = 47,

		/// <summary>
		/// Digit character '0'
		/// </summary>
		DIGIT_0 = 48,

		/// <summary>
		/// Digit character '1'
		/// </summary>
		DIGIT_1 = 49,

		/// <summary>
		/// Digit character '2'
		/// </summary>
		DIGIT_2 = 50,

		/// <summary>
		/// Digit character '3'
		/// </summary>
		DIGIT_3 = 51,

		/// <summary>
		/// Digit character '4'
		/// </summary>
		DIGIT_4 = 52,

		/// <summary>
		/// Digit character '5'
		/// </summary>
		DIGIT_5 = 53,

		/// <summary>
		/// Digit character '6'
		/// </summary>
		DIGIT_6 = 54,

		/// <summary>
		/// Digit character '7'
		/// </summary>
		DIGIT_7 = 55,

		/// <summary>
		/// Digit character '8'
		/// </summary>
		DIGIT_8 = 56,

		/// <summary>
		/// Digit character '9'
		/// </summary>
		DIGIT_9 = 57,

		/// <summary>
		/// Colon ':'
		/// </summary>
		COLON = 58,

		/// <summary>
		/// Semicolon ';'
		/// </summary>
		SEMICOLON = 59,

		/// <summary>
		/// Less-than sign '&lt;'
		/// </summary>
		LESS_THAN = 60,

		/// <summary>
		/// Equal sign '='
		/// </summary>
		EQUAL = 61,

		/// <summary>
		/// Greater-than sign '&gt;'
		/// </summary>
		GREATER_THAN = 62,

		/// <summary>
		/// Question mark '?'
		/// </summary>
		QUESTION_MARK = 63,

		/// <summary>
		/// At sign '@'
		/// </summary>
		AT_SIGN = 64,

		/// <summary>
		/// Uppercase letter 'A'
		/// </summary>
		UPPERCASE_A = 65,

		/// <summary>
		/// Uppercase letter 'B'
		/// </summary>
		UPPERCASE_B = 66,

		/// <summary>
		/// Uppercase letter 'C'
		/// </summary>
		UPPERCASE_C = 67,

		/// <summary>
		/// Uppercase letter 'D'
		/// </summary>
		UPPERCASE_D = 68,

		/// <summary>
		/// Uppercase letter 'E'
		/// </summary>
		UPPERCASE_E = 69,

		/// <summary>
		/// Uppercase letter 'F'
		/// </summary>
		UPPERCASE_F = 70,

		/// <summary>
		/// Uppercase letter 'G'
		/// </summary>
		UPPERCASE_G = 71,

		/// <summary>
		/// Uppercase letter 'H'
		/// </summary>
		UPPERCASE_H = 72,

		/// <summary>
		/// Uppercase letter 'I'
		/// </summary>
		UPPERCASE_I = 73,

		/// <summary>
		/// Uppercase letter 'J'
		/// </summary>
		UPPERCASE_J = 74,

		/// <summary>
		/// Uppercase letter 'K'
		/// </summary>
		UPPERCASE_K = 75,

		/// <summary>
		/// Uppercase letter 'L'
		/// </summary>
		UPPERCASE_L = 76,

		/// <summary>
		/// Uppercase letter 'M'
		/// </summary>
		UPPERCASE_M = 77,

		/// <summary>
		/// Uppercase letter 'N'
		/// </summary>
		UPPERCASE_N = 78,

		/// <summary>
		/// Uppercase letter 'O'
		/// </summary>
		UPPERCASE_O = 79,

		/// <summary>
		/// Uppercase letter 'P'
		/// </summary>
		UPPERCASE_P = 80,

		/// <summary>
		/// Uppercase letter 'Q'
		/// </summary>
		UPPERCASE_Q = 81,

		/// <summary>
		/// Uppercase letter 'R'
		/// </summary>
		UPPERCASE_R = 82,

		/// <summary>
		/// Uppercase letter 'S'
		/// </summary>
		UPPERCASE_S = 83,

		/// <summary>
		/// Uppercase letter 'T'
		/// </summary>
		UPPERCASE_T = 84,

		/// <summary>
		/// Uppercase letter 'U'
		/// </summary>
		UPPERCASE_U = 85,

		/// <summary>
		/// Uppercase letter 'V'
		/// </summary>
		UPPERCASE_V = 86,

		/// <summary>
		/// Uppercase letter 'W'
		/// </summary>
		UPPERCASE_W = 87,

		/// <summary>
		/// Uppercase letter 'X'
		/// </summary>
		UPPERCASE_X = 88,

		/// <summary>
		/// Uppercase letter 'Y'
		/// </summary>
		UPPERCASE_Y = 89,

		/// <summary>
		/// Uppercase letter 'Z'
		/// </summary>
		UPPERCASE_Z = 90,

		/// <summary>
		/// Left square bracket '['
		/// </summary>
		LEFT_BRACKET = 91,

		/// <summary>
		/// Backslash '\'
		/// </summary>
		BACKSLASH = 92,

		/// <summary>
		/// Right square bracket ']'
		/// </summary>
		RIGHT_BRACKET = 93,

		/// <summary>
		/// Caret / Circumflex '^'
		/// </summary>
		CARET = 94,

		/// <summary>
		/// Underscore '_'
		/// </summary>
		UNDERSCORE = 95,

		/// <summary>
		/// Grave accent / Backtick '`'
		/// </summary>
		BACKTICK = 96,

		/// <summary>
		/// Lowercase letter 'a'
		/// </summary>
		LOWERCASE_A = 97,

		/// <summary>
		/// Lowercase letter 'b'
		/// </summary>
		LOWERCASE_B = 98,

		/// <summary>
		/// Lowercase letter 'c'
		/// </summary>
		LOWERCASE_C = 99,

		/// <summary>
		/// Lowercase letter 'd'
		/// </summary>
		LOWERCASE_D = 100,

		/// <summary>
		/// Lowercase letter 'e'
		/// </summary>
		LOWERCASE_E = 101,

		/// <summary>
		/// Lowercase letter 'f'
		/// </summary>
		LOWERCASE_F = 102,

		/// <summary>
		/// Lowercase letter 'g'
		/// </summary>
		LOWERCASE_G = 103,

		/// <summary>
		/// Lowercase letter 'h'
		/// </summary>
		LOWERCASE_H = 104,

		/// <summary>
		/// Lowercase letter 'i'
		/// </summary>
		LOWERCASE_I = 105,

		/// <summary>
		/// Lowercase letter 'j'
		/// </summary>
		LOWERCASE_J = 106,

		/// <summary>
		/// Lowercase letter 'k'
		/// </summary>
		LOWERCASE_K = 107,

		/// <summary>
		/// Lowercase letter 'l'
		/// </summary>
		LOWERCASE_L = 108,

		/// <summary>
		/// Lowercase letter 'm'
		/// </summary>
		LOWERCASE_M = 109,

		/// <summary>
		/// Lowercase letter 'n'
		/// </summary>
		LOWERCASE_N = 110,

		/// <summary>
		/// Lowercase letter 'o'
		/// </summary>
		LOWERCASE_O = 111,

		/// <summary>
		/// Lowercase letter 'p'
		/// </summary>
		LOWERCASE_P = 112,

		/// <summary>
		/// Lowercase letter 'q'
		/// </summary>
		LOWERCASE_Q = 113,

		/// <summary>
		/// Lowercase letter 'r'
		/// </summary>
		LOWERCASE_R = 114,

		/// <summary>
		/// Lowercase letter 's'
		/// </summary>
		LOWERCASE_S = 115,

		/// <summary>
		/// Lowercase letter 't'
		/// </summary>
		LOWERCASE_T = 116,

		/// <summary>
		/// Lowercase letter 'u'
		/// </summary>
		LOWERCASE_U = 117,

		/// <summary>
		/// Lowercase letter 'v'
		/// </summary>
		LOWERCASE_V = 118,

		/// <summary>
		/// Lowercase letter 'w'
		/// </summary>
		LOWERCASE_W = 119,

		/// <summary>
		/// Lowercase letter 'x'
		/// </summary>
		LOWERCASE_X = 120,

		/// <summary>
		/// Lowercase letter 'y'
		/// </summary>
		LOWERCASE_Y = 121,

		/// <summary>
		/// Lowercase letter 'z'
		/// </summary>
		LOWERCASE_Z = 122,

		/// <summary>
		/// Left curly bracket '{'
		/// </summary>
		LEFT_BRACE = 123,

		/// <summary>
		/// Vertical bar / Pipe '|'
		/// </summary>
		PIPE = 124,

		/// <summary>
		/// Right curly bracket '}'
		/// </summary>
		RIGHT_BRACE = 125,

		/// <summary>
		/// Tilde '~'
		/// </summary>
		TILDE = 126,

		/// <summary>
		/// Delete char
		/// </summary>
		DEL = 127,






		/// <summary>
		/// ░
		/// </summary>
		LowDensityDotted = 176,

		/// <summary>
		/// ▒
		/// </summary>
		MediumDensityDotted = 177,

		/// <summary>
		/// ▓
		/// </summary>
		HighDensityDotted = 178,

		/// <summary>
		/// █
		/// </summary>
		Block = 219,

		/// <summary>
		/// ▄
		/// </summary>
		LowBlock = 220,

		/// <summary>
		/// ▀
		/// </summary>
		UppBlock = 223,

		/// <summary>
		/// ■
		/// </summary>
		MidBlock = 254,
	}

	public static class ASCIIExtensions
	{
		private static readonly ImmutableArray<ASCII> DIGITS = [ASCII.DIGIT_0, ASCII.DIGIT_1, ASCII.DIGIT_2, ASCII.DIGIT_3, ASCII.DIGIT_4, ASCII.DIGIT_5, ASCII.DIGIT_6, ASCII.DIGIT_7, ASCII.DIGIT_8, ASCII.DIGIT_9];
		private static readonly ImmutableArray<ASCII> LOWERCASE_LETTERS = [.. Enumerable.Range((int)ASCII.LOWERCASE_A, ASCII.LOWERCASE_Z - ASCII.LOWERCASE_A).Select(i => (ASCII)i)];
		private static readonly ImmutableArray<ASCII> UPPERCASE_LETTERS = [.. Enumerable.Range((int)ASCII.UPPERCASE_A, ASCII.UPPERCASE_Z - ASCII.UPPERCASE_A).Select(i => (ASCII)i)];

		extension(ASCII)
		{
			public static ImmutableArray<ASCII> Digits => DIGITS;

			public static ImmutableArray<ASCII> LowercaseLetters => LOWERCASE_LETTERS;

			public static ImmutableArray<ASCII> UppercaseLetters => UPPERCASE_LETTERS;
		}
	}

}
