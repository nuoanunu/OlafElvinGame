using System;
using System.Collections.Generic;

namespace GameConst
{
	public class Consts
	{
		public static Dictionary<string, float> damageFactor = new Dictionary<string, float> ()
		{
			{ "CC", 1F },		{ "CS", 0.25F },	{ "CI", 4F },		{ "CH", 3F },		{ "CG", 0.8F },		{ "CA", 2F },		{ "CF", 0.5F },
			{ "SC", 4F },		{ "SS", 0.25F },	{ "SI", 0.25F }, 	{ "SH", 0.8F }, 	{ "SG", 0.5F }, 	{ "SA", 0.8F }, 	{ "SF", 0.25F },
			{ "IC", 0.25F }, 	{ "IS", 3F }, 		{ "II", 1F }, 		{ "IH", 1.25F }, 	{ "IG", 0.8F }, 	{ "IA", 1.5F },		{ "IF", 0.5F },
			{ "HC", 0.5F }, 	{ "HS", 0.8F }, 	{ "HI", 1F }, 		{ "HH", 1F }, 		{ "HG", 4F }, 		{ "HA", 1.5F }, 	{ "HF", 1F },
			{ "GC", 1.25F }, 	{ "GS", 1.25F }, 	{ "GI", 1.25F }, 	{ "GH", 0.25F }, 	{ "GG", 1F }, 		{ "GA", 1.5F },		{ "GF", 1.25F },
			{ "AC", 3F }, 		{ "AS", 0.25F }, 	{ "AI", 1.5F }, 	{ "AH", 1.5F }, 	{ "AG", 0.8F },		{ "AA", 2F },		{ "AF", 3F },
			{ "FC", 2F },	 	{ "FS", 0.8F }, 	{ "FI", 2F }, 		{ "FH", 1F }, 		{ "FG", 1F }, 		{ "FA", 2F }, 		{ "FF", 1F },
		};

		public static Dictionary<string, ClassInfo> classes = new Dictionary<string, ClassInfo> (){
			{"Swordman", new ClassInfo('I', 20, 16, 6)},
			{"Horseman", new ClassInfo('C', 22, 14, 8)},
			{"Spearman", new ClassInfo('S', 18, 18, 5)}
		};
	}

	public class ClassInfo
	{
		public int baseAtk;
		public int baseDef;
		public char unitType;
		public int moveRange;

		public ClassInfo(char unitType, int baseAtk, int baseDef, int moveRange)
		{
			this.unitType = unitType;
			this.baseAtk = baseAtk;
			this.baseDef = baseDef;
			this.moveRange = moveRange;
		}
	}
}

