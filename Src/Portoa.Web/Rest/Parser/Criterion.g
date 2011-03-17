grammar Criterion;

options {
	language=CSharp3;
}

tokens {
	AND=',';
	OR='|';
	NOT='!';
	LIKE='~';
	LESS_THAN='<';
	LESS_THAN_OR_EQUAL_TO='<=';
	GREATER_THAN='>';
	GREATER_THAN_OR_EQUAL_TO='>=';
	CRITERION_DELIMITER	= '/';
}

@members {
	partial void SetCriteria(ref CriterionSet set);
	partial void SetIdent(string identValue);
}

@parser::namespace { Portoa.Web.Rest.Parser }
@lexer::namespace { Portoa.Web.Rest.Parser }

public getCriteria returns [CriterionSet set]:
	criterion* { SetCriteria(ref $set); }
;

criterion: fieldName CRITERION_DELIMITER fieldValue (booleanModifier fieldValue)* CRITERION_DELIMITER?;
fieldName:	ident;
fieldValue:  fieldValueModifier? ident;
ident:	ID { SetIdent($ID.text); };
booleanModifier: 	AND	| OR;
fieldValueModifier: NOT | LESS_THAN_OR_EQUAL_TO | LESS_THAN | GREATER_THAN | GREATER_THAN_OR_EQUAL_TO | LIKE;

ID: ('a'..'z'|'A'..'Z'|'0'..'9'|'-' |'_' | '=' | '+' | ';' | ':')+;