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

namespace Portoa.Web.Rest.Parser {
	[System.CodeDom.Compiler.GeneratedCode("ANTLR", "3.3 Nov 30, 2010 12:45:30")]
	[System.CLSCompliant(false)]
	public partial class CriterionParser : Antlr.Runtime.Parser {


		internal static readonly string[] tokenNames = new string[] {
		"<invalid>", "<EOR>", "<DOWN>", "<UP>", "AND", "OR", "NOT", "LIKE", "LESS_THAN", "LESS_THAN_OR_EQUAL_TO", "GREATER_THAN", "GREATER_THAN_OR_EQUAL_TO", "CRITERION_DELIMITER", "ID"
	};
		public const int EOF = -1;
		public const int AND = 4;
		public const int OR = 5;
		public const int NOT = 6;
		public const int LIKE = 7;
		public const int LESS_THAN = 8;
		public const int LESS_THAN_OR_EQUAL_TO = 9;
		public const int GREATER_THAN = 10;
		public const int GREATER_THAN_OR_EQUAL_TO = 11;
		public const int CRITERION_DELIMITER = 12;
		public const int ID = 13;

		// delegates
		// delegators

#if ANTLR_DEBUG
		private static readonly bool[] decisionCanBacktrack =
			new bool[]
			{
				false, // invalid decision
				false, false, false, false
			};
#else
		private static readonly bool[] decisionCanBacktrack = new bool[0];
#endif
		public CriterionParser(ITokenStream input)
			: this(input, new RecognizerSharedState()) {
		}
		public CriterionParser(ITokenStream input, RecognizerSharedState state)
			: base(input, state) {

			OnCreated();
		}



		public override string[] TokenNames { get { return CriterionParser.tokenNames; } }

		public override string GrammarFileName { get { return "C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g"; } }


		partial void SetCriteria(ref CriterionSet set);
		partial void SetIdent(string identValue);


		partial void OnCreated();
		partial void EnterRule(string ruleName, int ruleIndex);
		partial void LeaveRule(string ruleName, int ruleIndex);

		#region Rules

		partial void Enter_getCriteria();
		partial void Leave_getCriteria();

		// $ANTLR start "getCriteria"
		// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:31:8: public getCriteria returns [CriterionSet set] : ( criterion )* ;
		[GrammarRule("getCriteria")]
		public CriterionSet getCriteria() {
			Enter_getCriteria();
			EnterRule("getCriteria", 1);
			TraceIn("getCriteria", 1);
			CriterionSet set = default(CriterionSet);

			try {
				DebugEnterRule(GrammarFileName, "getCriteria");
				DebugLocation(31, 0);
				try {
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:31:46: ( ( criterion )* )
					DebugEnterAlt(1);
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:32:2: ( criterion )*
					{
						DebugLocation(32, 2);
						// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:32:2: ( criterion )*
						try {
							DebugEnterSubRule(1);
							while (true) {
								int alt1 = 2;
								try {
									DebugEnterDecision(1, decisionCanBacktrack[1]);
									int LA1_0 = input.LA(1);

									if ((LA1_0 == ID)) {
										alt1 = 1;
									}


								} finally { DebugExitDecision(1); }
								switch (alt1) {
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:32:2: criterion
					{
											DebugLocation(32, 2);
											PushFollow(Follow._criterion_in_getCriteria117);
											criterion();
											PopFollow();


										}
					break;

									default:
					goto loop1;
								}
							}

						loop1:
							;

						} finally { DebugExitSubRule(1); }

						DebugLocation(32, 13);
						SetCriteria(ref set);

					}

				} catch (RecognitionException re) {
					ReportError(re);
					Recover(input, re);
				} finally {
					TraceOut("getCriteria", 1);
					LeaveRule("getCriteria", 1);
					Leave_getCriteria();
				}
				DebugLocation(33, 0);
			} finally { DebugExitRule(GrammarFileName, "getCriteria"); }
			return set;

		}
		// $ANTLR end "getCriteria"


		partial void Enter_criterion();
		partial void Leave_criterion();

		// $ANTLR start "criterion"
		// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:35:1: criterion : fieldName CRITERION_DELIMITER fieldValue ( booleanModifier fieldValue )* ( CRITERION_DELIMITER )? ;
		[GrammarRule("criterion")]
		private void criterion() {
			Enter_criterion();
			EnterRule("criterion", 2);
			TraceIn("criterion", 2);
			try {
				DebugEnterRule(GrammarFileName, "criterion");
				DebugLocation(35, 102);
				try {
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:35:10: ( fieldName CRITERION_DELIMITER fieldValue ( booleanModifier fieldValue )* ( CRITERION_DELIMITER )? )
					DebugEnterAlt(1);
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:35:12: fieldName CRITERION_DELIMITER fieldValue ( booleanModifier fieldValue )* ( CRITERION_DELIMITER )?
					{
						DebugLocation(35, 12);
						PushFollow(Follow._fieldName_in_criterion128);
						fieldName();
						PopFollow();

						DebugLocation(35, 22);
						Match(input, CRITERION_DELIMITER, Follow._CRITERION_DELIMITER_in_criterion130);
						DebugLocation(35, 42);
						PushFollow(Follow._fieldValue_in_criterion132);
						fieldValue();
						PopFollow();

						DebugLocation(35, 53);
						// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:35:53: ( booleanModifier fieldValue )*
						try {
							DebugEnterSubRule(2);
							while (true) {
								int alt2 = 2;
								try {
									DebugEnterDecision(2, decisionCanBacktrack[2]);
									int LA2_0 = input.LA(1);

									if (((LA2_0 >= AND && LA2_0 <= OR))) {
										alt2 = 1;
									}


								} finally { DebugExitDecision(2); }
								switch (alt2) {
									case 1:
										DebugEnterAlt(1);
										// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:35:54: booleanModifier fieldValue
					{
											DebugLocation(35, 54);
											PushFollow(Follow._booleanModifier_in_criterion135);
											booleanModifier();
											PopFollow();

											DebugLocation(35, 70);
											PushFollow(Follow._fieldValue_in_criterion137);
											fieldValue();
											PopFollow();


										}
					break;

									default:
					goto loop2;
								}
							}

						loop2:
							;

						} finally { DebugExitSubRule(2); }

						DebugLocation(35, 83);
						// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:35:83: ( CRITERION_DELIMITER )?
						int alt3 = 2;
						try {
							DebugEnterSubRule(3);
							try {
								DebugEnterDecision(3, decisionCanBacktrack[3]);
								int LA3_0 = input.LA(1);

								if ((LA3_0 == CRITERION_DELIMITER)) {
									alt3 = 1;
								}
							} finally { DebugExitDecision(3); }
							switch (alt3) {
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:35:83: CRITERION_DELIMITER
				{
										DebugLocation(35, 83);
										Match(input, CRITERION_DELIMITER, Follow._CRITERION_DELIMITER_in_criterion141);

									}
				break;

							}
						} finally { DebugExitSubRule(3); }


					}

				} catch (RecognitionException re) {
					ReportError(re);
					Recover(input, re);
				} finally {
					TraceOut("criterion", 2);
					LeaveRule("criterion", 2);
					Leave_criterion();
				}
				DebugLocation(35, 102);
			} finally { DebugExitRule(GrammarFileName, "criterion"); }
			return;

		}
		// $ANTLR end "criterion"


		partial void Enter_fieldName();
		partial void Leave_fieldName();

		// $ANTLR start "fieldName"
		// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:36:1: fieldName : ident ;
		[GrammarRule("fieldName")]
		private void fieldName() {
			Enter_fieldName();
			EnterRule("fieldName", 3);
			TraceIn("fieldName", 3);
			try {
				DebugEnterRule(GrammarFileName, "fieldName");
				DebugLocation(36, 16);
				try {
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:36:10: ( ident )
					DebugEnterAlt(1);
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:36:12: ident
					{
						DebugLocation(36, 12);
						PushFollow(Follow._ident_in_fieldName148);
						ident();
						PopFollow();


					}

				} catch (RecognitionException re) {
					ReportError(re);
					Recover(input, re);
				} finally {
					TraceOut("fieldName", 3);
					LeaveRule("fieldName", 3);
					Leave_fieldName();
				}
				DebugLocation(36, 16);
			} finally { DebugExitRule(GrammarFileName, "fieldName"); }
			return;

		}
		// $ANTLR end "fieldName"


		partial void Enter_fieldValue();
		partial void Leave_fieldValue();

		// $ANTLR start "fieldValue"
		// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:37:1: fieldValue : ( fieldValueModifier )? ident ;
		[GrammarRule("fieldValue")]
		private void fieldValue() {
			Enter_fieldValue();
			EnterRule("fieldValue", 4);
			TraceIn("fieldValue", 4);
			try {
				DebugEnterRule(GrammarFileName, "fieldValue");
				DebugLocation(37, 38);
				try {
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:37:11: ( ( fieldValueModifier )? ident )
					DebugEnterAlt(1);
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:37:14: ( fieldValueModifier )? ident
					{
						DebugLocation(37, 14);
						// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:37:14: ( fieldValueModifier )?
						int alt4 = 2;
						try {
							DebugEnterSubRule(4);
							try {
								DebugEnterDecision(4, decisionCanBacktrack[4]);
								int LA4_0 = input.LA(1);

								if (((LA4_0 >= NOT && LA4_0 <= GREATER_THAN_OR_EQUAL_TO))) {
									alt4 = 1;
								}
							} finally { DebugExitDecision(4); }
							switch (alt4) {
								case 1:
									DebugEnterAlt(1);
									// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:37:14: fieldValueModifier
				{
										DebugLocation(37, 14);
										PushFollow(Follow._fieldValueModifier_in_fieldValue155);
										fieldValueModifier();
										PopFollow();


									}
				break;

							}
						} finally { DebugExitSubRule(4); }

						DebugLocation(37, 34);
						PushFollow(Follow._ident_in_fieldValue158);
						ident();
						PopFollow();


					}

				} catch (RecognitionException re) {
					ReportError(re);
					Recover(input, re);
				} finally {
					TraceOut("fieldValue", 4);
					LeaveRule("fieldValue", 4);
					Leave_fieldValue();
				}
				DebugLocation(37, 38);
			} finally { DebugExitRule(GrammarFileName, "fieldValue"); }
			return;

		}
		// $ANTLR end "fieldValue"


		partial void Enter_ident();
		partial void Leave_ident();

		// $ANTLR start "ident"
		// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:38:1: ident : ID ;
		[GrammarRule("ident")]
		private void ident() {
			Enter_ident();
			EnterRule("ident", 5);
			TraceIn("ident", 5);

			IToken ID1 = null;

			try {
				DebugEnterRule(GrammarFileName, "ident");
				DebugLocation(38, 33);
				try {
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:38:6: ( ID )
					DebugEnterAlt(1);
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:38:8: ID
					{
						DebugLocation(38, 8);
						ID1 = (IToken)Match(input, ID, Follow._ID_in_ident164);
						DebugLocation(38, 11);
						SetIdent((ID1 != null ? ID1.Text : null));

					}

				} catch (RecognitionException re) {
					ReportError(re);
					Recover(input, re);
				} finally {
					TraceOut("ident", 5);
					LeaveRule("ident", 5);
					Leave_ident();
				}
				DebugLocation(38, 33);
			} finally { DebugExitRule(GrammarFileName, "ident"); }
			return;

		}
		// $ANTLR end "ident"


		partial void Enter_booleanModifier();
		partial void Leave_booleanModifier();

		// $ANTLR start "booleanModifier"
		// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:39:1: booleanModifier : ( AND | OR );
		[GrammarRule("booleanModifier")]
		private void booleanModifier() {
			Enter_booleanModifier();
			EnterRule("booleanModifier", 6);
			TraceIn("booleanModifier", 6);
			try {
				DebugEnterRule(GrammarFileName, "booleanModifier");
				DebugLocation(39, 26);
				try {
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:39:16: ( AND | OR )
					DebugEnterAlt(1);
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:
					{
						DebugLocation(39, 16);
						if ((input.LA(1) >= AND && input.LA(1) <= OR)) {
							input.Consume();
							state.errorRecovery = false;
						} else {

							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

				} catch (RecognitionException re) {
					ReportError(re);
					Recover(input, re);
				} finally {
					TraceOut("booleanModifier", 6);
					LeaveRule("booleanModifier", 6);
					Leave_booleanModifier();
				}
				DebugLocation(39, 26);
			} finally { DebugExitRule(GrammarFileName, "booleanModifier"); }
			return;

		}
		// $ANTLR end "booleanModifier"



		partial void Enter_fieldValueModifier();
		partial void Leave_fieldValueModifier();

		// $ANTLR start "fieldValueModifier"
		// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:40:1: fieldValueModifier : ( NOT | LESS_THAN_OR_EQUAL_TO | LESS_THAN | GREATER_THAN | GREATER_THAN_OR_EQUAL_TO | LIKE );
		[GrammarRule("fieldValueModifier")]
		private void fieldValueModifier() {
			Enter_fieldValueModifier();
			EnterRule("fieldValueModifier", 7);
			TraceIn("fieldValueModifier", 7);
			try {
				DebugEnterRule(GrammarFileName, "fieldValueModifier");
				DebugLocation(40, 108);
				try {
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:40:19: ( NOT | LESS_THAN_OR_EQUAL_TO | LESS_THAN | GREATER_THAN | GREATER_THAN_OR_EQUAL_TO | LIKE )
					DebugEnterAlt(1);
					// C:\\Users\\tmont\\code\\Portoa\\Src\\Portoa.Web\\Rest\\Criterion.g:
					{
						DebugLocation(40, 19);
						if ((input.LA(1) >= NOT && input.LA(1) <= GREATER_THAN_OR_EQUAL_TO)) {
							input.Consume();
							state.errorRecovery = false;
						} else {

							MismatchedSetException mse = new MismatchedSetException(null, input);
							DebugRecognitionException(mse);
							throw mse;
						}


					}

				} catch (RecognitionException re) {
					ReportError(re);
					Recover(input, re);
				} finally {
					TraceOut("fieldValueModifier", 7);
					LeaveRule("fieldValueModifier", 7);
					Leave_fieldValueModifier();
				}
				DebugLocation(40, 108);
			} finally { DebugExitRule(GrammarFileName, "fieldValueModifier"); }
			return;

		}
		// $ANTLR end "fieldValueModifier"
		#endregion Rules


		#region Follow sets
		private static class Follow {
			public static readonly BitSet _criterion_in_getCriteria117 = new BitSet(new ulong[] { 0x2002UL });
			public static readonly BitSet _fieldName_in_criterion128 = new BitSet(new ulong[] { 0x1000UL });
			public static readonly BitSet _CRITERION_DELIMITER_in_criterion130 = new BitSet(new ulong[] { 0x2FC0UL });
			public static readonly BitSet _fieldValue_in_criterion132 = new BitSet(new ulong[] { 0x1032UL });
			public static readonly BitSet _booleanModifier_in_criterion135 = new BitSet(new ulong[] { 0x2FC0UL });
			public static readonly BitSet _fieldValue_in_criterion137 = new BitSet(new ulong[] { 0x1032UL });
			public static readonly BitSet _CRITERION_DELIMITER_in_criterion141 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ident_in_fieldName148 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _fieldValueModifier_in_fieldValue155 = new BitSet(new ulong[] { 0x2000UL });
			public static readonly BitSet _ident_in_fieldValue158 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _ID_in_ident164 = new BitSet(new ulong[] { 0x2UL });

			public static readonly BitSet _set_in_booleanModifier0 = new BitSet(new ulong[] { 0x2UL });
			public static readonly BitSet _set_in_fieldValueModifier0 = new BitSet(new ulong[] { 0x2UL });

		}
		#endregion Follow sets
	}

} // namespace  Portoa.Web.Rest.Parser 
