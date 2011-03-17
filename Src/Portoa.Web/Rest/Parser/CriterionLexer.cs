// $ANTLR 3.3 Nov 30, 2010 12:45:30 C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g 2011-03-17 00:04:57

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 219
// Unreachable code detected.
#pragma warning disable 162


using System.Collections.Generic;
using Antlr.Runtime;
using Stack = System.Collections.Generic.Stack<object>;
using List = System.Collections.IList;
using ArrayList = System.Collections.Generic.List<object>;

namespace  Portoa.Web.Rest.Parser 
{
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.3 Nov 30, 2010 12:45:30")]
[System.CLSCompliant(false)]
public partial class CriterionLexer : Antlr.Runtime.Lexer
{
	public const int EOF=-1;
	public const int AND=4;
	public const int OR=5;
	public const int NOT=6;
	public const int LIKE=7;
	public const int LESS_THAN=8;
	public const int LESS_THAN_OR_EQUAL_TO=9;
	public const int GREATER_THAN=10;
	public const int GREATER_THAN_OR_EQUAL_TO=11;
	public const int CRITERION_DELIMITER=12;
	public const int ID=13;

    // delegates
    // delegators

	public CriterionLexer()
	{
		OnCreated();
	}

	public CriterionLexer(ICharStream input )
		: this(input, new RecognizerSharedState())
	{
	}

	public CriterionLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state)
	{


		OnCreated();
	}
	public override string GrammarFileName { get { return "C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g"; } }

	private static readonly bool[] decisionCanBacktrack = new bool[0];


	partial void OnCreated();
	partial void EnterRule(string ruleName, int ruleIndex);
	partial void LeaveRule(string ruleName, int ruleIndex);

	partial void Enter_AND();
	partial void Leave_AND();

	// $ANTLR start "AND"
	[GrammarRule("AND")]
	private void mAND()
	{
		Enter_AND();
		EnterRule("AND", 1);
		TraceIn("AND", 1);
		try
		{
			int _type = AND;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:9:5: ( ',' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:9:7: ','
			{
			DebugLocation(9, 7);
			Match(','); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("AND", 1);
			LeaveRule("AND", 1);
			Leave_AND();
		}
	}
	// $ANTLR end "AND"

	partial void Enter_OR();
	partial void Leave_OR();

	// $ANTLR start "OR"
	[GrammarRule("OR")]
	private void mOR()
	{
		Enter_OR();
		EnterRule("OR", 2);
		TraceIn("OR", 2);
		try
		{
			int _type = OR;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:10:4: ( '|' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:10:6: '|'
			{
			DebugLocation(10, 6);
			Match('|'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("OR", 2);
			LeaveRule("OR", 2);
			Leave_OR();
		}
	}
	// $ANTLR end "OR"

	partial void Enter_NOT();
	partial void Leave_NOT();

	// $ANTLR start "NOT"
	[GrammarRule("NOT")]
	private void mNOT()
	{
		Enter_NOT();
		EnterRule("NOT", 3);
		TraceIn("NOT", 3);
		try
		{
			int _type = NOT;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:11:5: ( '!' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:11:7: '!'
			{
			DebugLocation(11, 7);
			Match('!'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("NOT", 3);
			LeaveRule("NOT", 3);
			Leave_NOT();
		}
	}
	// $ANTLR end "NOT"

	partial void Enter_LIKE();
	partial void Leave_LIKE();

	// $ANTLR start "LIKE"
	[GrammarRule("LIKE")]
	private void mLIKE()
	{
		Enter_LIKE();
		EnterRule("LIKE", 4);
		TraceIn("LIKE", 4);
		try
		{
			int _type = LIKE;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:12:6: ( '~' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:12:8: '~'
			{
			DebugLocation(12, 8);
			Match('~'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("LIKE", 4);
			LeaveRule("LIKE", 4);
			Leave_LIKE();
		}
	}
	// $ANTLR end "LIKE"

	partial void Enter_LESS_THAN();
	partial void Leave_LESS_THAN();

	// $ANTLR start "LESS_THAN"
	[GrammarRule("LESS_THAN")]
	private void mLESS_THAN()
	{
		Enter_LESS_THAN();
		EnterRule("LESS_THAN", 5);
		TraceIn("LESS_THAN", 5);
		try
		{
			int _type = LESS_THAN;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:13:11: ( '<' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:13:13: '<'
			{
			DebugLocation(13, 13);
			Match('<'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("LESS_THAN", 5);
			LeaveRule("LESS_THAN", 5);
			Leave_LESS_THAN();
		}
	}
	// $ANTLR end "LESS_THAN"

	partial void Enter_LESS_THAN_OR_EQUAL_TO();
	partial void Leave_LESS_THAN_OR_EQUAL_TO();

	// $ANTLR start "LESS_THAN_OR_EQUAL_TO"
	[GrammarRule("LESS_THAN_OR_EQUAL_TO")]
	private void mLESS_THAN_OR_EQUAL_TO()
	{
		Enter_LESS_THAN_OR_EQUAL_TO();
		EnterRule("LESS_THAN_OR_EQUAL_TO", 6);
		TraceIn("LESS_THAN_OR_EQUAL_TO", 6);
		try
		{
			int _type = LESS_THAN_OR_EQUAL_TO;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:14:23: ( '<=' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:14:25: '<='
			{
			DebugLocation(14, 25);
			Match("<="); 


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("LESS_THAN_OR_EQUAL_TO", 6);
			LeaveRule("LESS_THAN_OR_EQUAL_TO", 6);
			Leave_LESS_THAN_OR_EQUAL_TO();
		}
	}
	// $ANTLR end "LESS_THAN_OR_EQUAL_TO"

	partial void Enter_GREATER_THAN();
	partial void Leave_GREATER_THAN();

	// $ANTLR start "GREATER_THAN"
	[GrammarRule("GREATER_THAN")]
	private void mGREATER_THAN()
	{
		Enter_GREATER_THAN();
		EnterRule("GREATER_THAN", 7);
		TraceIn("GREATER_THAN", 7);
		try
		{
			int _type = GREATER_THAN;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:15:14: ( '>' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:15:16: '>'
			{
			DebugLocation(15, 16);
			Match('>'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("GREATER_THAN", 7);
			LeaveRule("GREATER_THAN", 7);
			Leave_GREATER_THAN();
		}
	}
	// $ANTLR end "GREATER_THAN"

	partial void Enter_GREATER_THAN_OR_EQUAL_TO();
	partial void Leave_GREATER_THAN_OR_EQUAL_TO();

	// $ANTLR start "GREATER_THAN_OR_EQUAL_TO"
	[GrammarRule("GREATER_THAN_OR_EQUAL_TO")]
	private void mGREATER_THAN_OR_EQUAL_TO()
	{
		Enter_GREATER_THAN_OR_EQUAL_TO();
		EnterRule("GREATER_THAN_OR_EQUAL_TO", 8);
		TraceIn("GREATER_THAN_OR_EQUAL_TO", 8);
		try
		{
			int _type = GREATER_THAN_OR_EQUAL_TO;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:16:26: ( '>=' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:16:28: '>='
			{
			DebugLocation(16, 28);
			Match(">="); 


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("GREATER_THAN_OR_EQUAL_TO", 8);
			LeaveRule("GREATER_THAN_OR_EQUAL_TO", 8);
			Leave_GREATER_THAN_OR_EQUAL_TO();
		}
	}
	// $ANTLR end "GREATER_THAN_OR_EQUAL_TO"

	partial void Enter_CRITERION_DELIMITER();
	partial void Leave_CRITERION_DELIMITER();

	// $ANTLR start "CRITERION_DELIMITER"
	[GrammarRule("CRITERION_DELIMITER")]
	private void mCRITERION_DELIMITER()
	{
		Enter_CRITERION_DELIMITER();
		EnterRule("CRITERION_DELIMITER", 9);
		TraceIn("CRITERION_DELIMITER", 9);
		try
		{
			int _type = CRITERION_DELIMITER;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:17:21: ( '/' )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:17:23: '/'
			{
			DebugLocation(17, 23);
			Match('/'); 

			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("CRITERION_DELIMITER", 9);
			LeaveRule("CRITERION_DELIMITER", 9);
			Leave_CRITERION_DELIMITER();
		}
	}
	// $ANTLR end "CRITERION_DELIMITER"

	partial void Enter_ID();
	partial void Leave_ID();

	// $ANTLR start "ID"
	[GrammarRule("ID")]
	private void mID()
	{
		Enter_ID();
		EnterRule("ID", 10);
		TraceIn("ID", 10);
		try
		{
			int _type = ID;
			int _channel = DefaultTokenChannel;
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:42:3: ( ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '-' | '_' | '=' | '+' | ';' | ':' )+ )
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:42:5: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '-' | '_' | '=' | '+' | ';' | ':' )+
			{
			DebugLocation(42, 5);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:42:5: ( 'a' .. 'z' | 'A' .. 'Z' | '0' .. '9' | '-' | '_' | '=' | '+' | ';' | ':' )+
			int cnt1=0;
			try { DebugEnterSubRule(1);
			while (true)
			{
				int alt1=2;
				try { DebugEnterDecision(1, decisionCanBacktrack[1]);
				int LA1_0 = input.LA(1);

				if ((LA1_0=='+'||LA1_0=='-'||(LA1_0>='0' && LA1_0<=';')||LA1_0=='='||(LA1_0>='A' && LA1_0<='Z')||LA1_0=='_'||(LA1_0>='a' && LA1_0<='z')))
				{
					alt1=1;
				}


				} finally { DebugExitDecision(1); }
				switch (alt1)
				{
				case 1:
					DebugEnterAlt(1);
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:
					{
					DebugLocation(42, 5);
					if (input.LA(1)=='+'||input.LA(1)=='-'||(input.LA(1)>='0' && input.LA(1)<=';')||input.LA(1)=='='||(input.LA(1)>='A' && input.LA(1)<='Z')||input.LA(1)=='_'||(input.LA(1)>='a' && input.LA(1)<='z'))
					{
						input.Consume();

					}
					else
					{
						MismatchedSetException mse = new MismatchedSetException(null,input);
						DebugRecognitionException(mse);
						Recover(mse);
						throw mse;}


					}
					break;

				default:
					if (cnt1 >= 1)
						goto loop1;

					EarlyExitException eee1 = new EarlyExitException( 1, input );
					DebugRecognitionException(eee1);
					throw eee1;
				}
				cnt1++;
			}
			loop1:
				;

			} finally { DebugExitSubRule(1); }


			}

			state.type = _type;
			state.channel = _channel;
		}
		finally
		{
			TraceOut("ID", 10);
			LeaveRule("ID", 10);
			Leave_ID();
		}
	}
	// $ANTLR end "ID"

	public override void mTokens()
	{
		// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:8: ( AND | OR | NOT | LIKE | LESS_THAN | LESS_THAN_OR_EQUAL_TO | GREATER_THAN | GREATER_THAN_OR_EQUAL_TO | CRITERION_DELIMITER | ID )
		int alt2=10;
		try { DebugEnterDecision(2, decisionCanBacktrack[2]);
		try
		{
			alt2 = dfa2.Predict(input);
		}
		catch (NoViableAltException nvae)
		{
			DebugRecognitionException(nvae);
			throw;
		}
		} finally { DebugExitDecision(2); }
		switch (alt2)
		{
		case 1:
			DebugEnterAlt(1);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:10: AND
			{
			DebugLocation(1, 10);
			mAND(); 

			}
			break;
		case 2:
			DebugEnterAlt(2);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:14: OR
			{
			DebugLocation(1, 14);
			mOR(); 

			}
			break;
		case 3:
			DebugEnterAlt(3);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:17: NOT
			{
			DebugLocation(1, 17);
			mNOT(); 

			}
			break;
		case 4:
			DebugEnterAlt(4);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:21: LIKE
			{
			DebugLocation(1, 21);
			mLIKE(); 

			}
			break;
		case 5:
			DebugEnterAlt(5);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:26: LESS_THAN
			{
			DebugLocation(1, 26);
			mLESS_THAN(); 

			}
			break;
		case 6:
			DebugEnterAlt(6);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:36: LESS_THAN_OR_EQUAL_TO
			{
			DebugLocation(1, 36);
			mLESS_THAN_OR_EQUAL_TO(); 

			}
			break;
		case 7:
			DebugEnterAlt(7);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:58: GREATER_THAN
			{
			DebugLocation(1, 58);
			mGREATER_THAN(); 

			}
			break;
		case 8:
			DebugEnterAlt(8);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:71: GREATER_THAN_OR_EQUAL_TO
			{
			DebugLocation(1, 71);
			mGREATER_THAN_OR_EQUAL_TO(); 

			}
			break;
		case 9:
			DebugEnterAlt(9);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:96: CRITERION_DELIMITER
			{
			DebugLocation(1, 96);
			mCRITERION_DELIMITER(); 

			}
			break;
		case 10:
			DebugEnterAlt(10);
			// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:1:116: ID
			{
			DebugLocation(1, 116);
			mID(); 

			}
			break;

		}

	}


	#region DFA
	DFA2 dfa2;

	protected override void InitDFAs()
	{
		base.InitDFAs();
		dfa2 = new DFA2(this);
	}

	private class DFA2 : DFA
	{
		private const string DFA2_eotS =
			"\x5\xFFFF\x1\xA\x1\xC\x6\xFFFF";
		private const string DFA2_eofS =
			"\xD\xFFFF";
		private const string DFA2_minS =
			"\x1\x21\x4\xFFFF\x2\x3D\x6\xFFFF";
		private const string DFA2_maxS =
			"\x1\x7E\x4\xFFFF\x2\x3D\x6\xFFFF";
		private const string DFA2_acceptS =
			"\x1\xFFFF\x1\x1\x1\x2\x1\x3\x1\x4\x2\xFFFF\x1\x9\x1\xA\x1\x6\x1\x5\x1"+
			"\x8\x1\x7";
		private const string DFA2_specialS =
			"\xD\xFFFF}>";
		private static readonly string[] DFA2_transitionS =
			{
				"\x1\x3\x9\xFFFF\x1\x8\x1\x1\x1\x8\x1\xFFFF\x1\x7\xC\x8\x1\x5\x1\x8"+
				"\x1\x6\x2\xFFFF\x1A\x8\x4\xFFFF\x1\x8\x1\xFFFF\x1A\x8\x1\xFFFF\x1\x2"+
				"\x1\xFFFF\x1\x4",
				"",
				"",
				"",
				"",
				"\x1\x9",
				"\x1\xB",
				"",
				"",
				"",
				"",
				"",
				""
			};

		private static readonly short[] DFA2_eot = DFA.UnpackEncodedString(DFA2_eotS);
		private static readonly short[] DFA2_eof = DFA.UnpackEncodedString(DFA2_eofS);
		private static readonly char[] DFA2_min = DFA.UnpackEncodedStringToUnsignedChars(DFA2_minS);
		private static readonly char[] DFA2_max = DFA.UnpackEncodedStringToUnsignedChars(DFA2_maxS);
		private static readonly short[] DFA2_accept = DFA.UnpackEncodedString(DFA2_acceptS);
		private static readonly short[] DFA2_special = DFA.UnpackEncodedString(DFA2_specialS);
		private static readonly short[][] DFA2_transition;

		static DFA2()
		{
			int numStates = DFA2_transitionS.Length;
			DFA2_transition = new short[numStates][];
			for ( int i=0; i < numStates; i++ )
			{
				DFA2_transition[i] = DFA.UnpackEncodedString(DFA2_transitionS[i]);
			}
		}

		public DFA2( BaseRecognizer recognizer )
		{
			this.recognizer = recognizer;
			this.decisionNumber = 2;
			this.eot = DFA2_eot;
			this.eof = DFA2_eof;
			this.min = DFA2_min;
			this.max = DFA2_max;
			this.accept = DFA2_accept;
			this.special = DFA2_special;
			this.transition = DFA2_transition;
		}

		public override string Description { get { return "1:1: Tokens : ( AND | OR | NOT | LIKE | LESS_THAN | LESS_THAN_OR_EQUAL_TO | GREATER_THAN | GREATER_THAN_OR_EQUAL_TO | CRITERION_DELIMITER | ID );"; } }

		public override void Error(NoViableAltException nvae)
		{
			DebugRecognitionException(nvae);
		}
	}

 
	#endregion

}

} // namespace  Portoa.Web.Rest.Parser 
